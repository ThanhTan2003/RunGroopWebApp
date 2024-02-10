using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            this.raceRepository = raceRepository;
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
        public async Task<IActionResult> Create(Race race)
        {
            if(ModelState.IsValid)
            {
                return View(race);
            }
            raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
