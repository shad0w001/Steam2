using FastSteamCopy.Data;
using FastSteamCopy.Models;
using FastSteamCopy.Services;
using FastSteamCopy.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastSteamCopy.Controllers
{
    public class AchievementController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AchievementServices AchievementServices { get; set; }

        public AchievementController(AchievementServices services, ApplicationDbContext context)
        {
            AchievementServices = services;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<AchievementViewModel> achievements = AchievementServices.GetAll();
            return View(achievements);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Games = AchievementServices.GetGames();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AchievementViewModel achievementViewModel)
        {
            await AchievementServices.CreateAsync(achievementViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AchievementViewModel model = AchievementServices.GetDetailsById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            AchievementViewModel achievement = AchievementServices.GetDetailsById(id);

            if (achievement == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Games = AchievementServices.GetGames();
            return View(achievement);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(AchievementViewModel model)
        {
            await AchievementServices.UpdateAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AchievementViewModel model = AchievementServices.GetDetailsById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost] //Can't change to delete
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(AchievementViewModel model)
        {
            await AchievementServices.DeleteAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

    }
}
