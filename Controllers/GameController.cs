using FastSteamCopy.Data;
using FastSteamCopy.Models;
using FastSteamCopy.Services;
using FastSteamCopy.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastSteamCopy.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameServices GameServices { get; set; }

        public GameController(GameServices services, ApplicationDbContext context)
        {
            GameServices = services;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<GameViewModel> games = GameServices.GetAll();

            return View(games);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Developer, Publisher, ReleaseDate, Genre, Platform")] GameViewModel gameViewModel)
        {
            await GameServices.CreateAsync(gameViewModel);
            await _context.SaveChangesAsync();
            //TempData["SuccessMessage"] = $"{gameViewModel.Title} created successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GameViewModel gameModel = GameServices.GetDetailsById(id);
            if (gameModel == null)
            {
                return NotFound();
            }
            return View(gameModel);
        }

        [HttpGet]
        public IActionResult Update(string? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            GameViewModel game = GameServices.GetDetailsById(id);

            if (game == null)
            {
                return RedirectToAction("Index");
            }
            return View(game);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(GameViewModel model)
        {
            await GameServices.UpdateAsync(model);
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
            GameViewModel game = GameServices.GetDetailsById(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost] //Can't change to delete
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(GameViewModel model)
        {
            await GameServices.DeleteAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

    }
}
