using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repositorys;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            this.clubRepository = clubRepository;
            this.photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await clubRepository.GetAll();
            return View(clubs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var club = await clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if(!ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(clubViewModel.Image);
                var club = new Club
                {
                    Title = clubViewModel.Title,
                    Description = clubViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = clubViewModel.Address.City,
                        State = clubViewModel.Address.State,
                        Street = clubViewModel.Address.Street,
                    }

                };
                clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload to failed");
            }
            return View(clubViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await clubRepository.GetByIdAsync(id);
            if(club == null)
                return View("Error");
            var clubViewModel = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId   = club.AddressId,   
                URL = club.Image,
                Address = club.Address,
                ClubCategory = club.ClubCategory
            };
            return View(clubViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel editClubViewModel)
        {
            if(!ModelState.IsValid) // Kiểm tra dữ liệu nhập có hợp lệ không?
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", editClubViewModel);
            }
            
            var userClub = await clubRepository.GetByIdAsyncNoTracking(id); // Lấy club theo id
            
            if(userClub != null) // Kiểm tra hình ảnh upload
            {
                try
                {
                    await photoService.DeletePhotoAsync(userClub.Image); // Xóa hình ảnh trên cloud
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editClubViewModel);
                }
                var photoResult = await photoService.AddPhotoAsync(editClubViewModel.Image); // Thêm hình ảnh mới
                var club = new Club
                {
                    Id = id,
                    Title = editClubViewModel.Title,
                    Description = editClubViewModel.Description,
                    Address = editClubViewModel.Address,
                    Image = photoResult.Url.ToString(),
                    AddressId = id,
                };
                clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editClubViewModel);
            }
        }
    }
}
