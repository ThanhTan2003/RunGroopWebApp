using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repositorys;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository raceRepository;
        private readonly IPhotoService photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            this.raceRepository = raceRepository;
            this.photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
           IEnumerable<Race> races = await raceRepository.GetAll();
            return View(races);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Race club = await raceRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel createRaceViewModel)
        {
            if (!ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(createRaceViewModel.Image);
                var race = new Race
                {
                    Title = createRaceViewModel.Title,
                    Description = createRaceViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = createRaceViewModel.Address.City,
                        State = createRaceViewModel.Address.State,
                        Street = createRaceViewModel.Address.Street,
                    }

                };
                raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload to failed");
            }
            return View(createRaceViewModel);
        }
    }
}
