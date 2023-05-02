using FastSteamCopy.Data;
using FastSteamCopy.Models;
using FastSteamCopy.Services.Interfaces;
using FastSteamCopy.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastSteamCopy.Services
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbContext context;

        public GameServices(ApplicationDbContext post)
        {
            context = post;
        }
        public List<GameViewModel> GetAll()
        {
            return context.Games.Select(game => new GameViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                Genre = game.Genre,
                Platform = game.Platform,
                Achievements = game.Achievements,
            }).ToList();
        }
        public async Task CreateAsync(GameViewModel model)
        {
            Game game = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Developer = model.Developer,
                Publisher = model.Publisher,
                ReleaseDate = model.ReleaseDate,
                Genre = model.Genre,
                Platform = model.Platform,
                Achievements = model.Achievements,
            };

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();
        }        
        public GameViewModel GetDetailsById(string id)
        {
            GameViewModel? model = context.Games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                Genre = game.Genre,
                Platform = game.Platform,
                Achievements = game.Achievements,
             }).SingleOrDefault(game => game.Id == id);

            return model;
        }
        public async Task UpdateAsync(GameViewModel model)
        {
            Game? game = await context.Games.FindAsync(model.Id);

            if (game != null)
            {
                game.Id = model.Id;
                game.Title = model.Title;
                game.Developer = model.Developer;
                game.Publisher = model.Publisher;
                game.ReleaseDate = model.ReleaseDate;
                game.Genre = model.Genre;
                game.Platform = model.Platform;
                game.Achievements = model.Achievements;

                context.Games.Update(game);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(GameViewModel model)
        {
            Game? game = await context.Games.FindAsync(model.Id);

            if (game != null)
            {                
                context.Games.Remove(game);
                await context.SaveChangesAsync();
            }
        }
    }
}
