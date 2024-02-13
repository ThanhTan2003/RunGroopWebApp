using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
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
        public async Task<IActionResult> Edit(int id)
        {
            var race = await raceRepository.GetByIdAsync(id);
            if(race == null)
            {
                return View("Error");
            }
            var editViewModel = new EditRaceViewModel
            {
                Id = race.Id,
                Title = race.Title,
                Description = race.Description,
                URL = race.Image,
                Address = race.Address,
                RaceCategory = race.RaceCategory
            };
            return View(editViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel editRaceViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View(editRaceViewModel);
            }
            var userRace = await raceRepository.GetByIdAsyncNoTracking(id);
            if(userRace != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(userRace.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editRaceViewModel);
                }
                var newPhoto = await photoService.AddPhotoAsync(editRaceViewModel.Image);
                var race = new Race
                {
                    Id = editRaceViewModel.Id,
                    Title = editRaceViewModel.Title,
                    Description = editRaceViewModel.Description,
                    Image = newPhoto.Url.ToString(),
                    Address = editRaceViewModel.Address
                };
                raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editRaceViewModel);
            }
        }
    }
}
