using FastSteamCopy.Data;
using FastSteamCopy.Models;
using FastSteamCopy.Services.Interfaces;
using FastSteamCopy.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastSteamCopy.Services
{
    public class AchievementServices : IAchievementServices
    {
        private readonly ApplicationDbContext context;
        public readonly GameServices gameServices;

        public AchievementServices(ApplicationDbContext post)
        {
            context = post;
            gameServices = new GameServices(context);
        }
        public List<AchievementViewModel> GetAll()
        {
            return context.Achievements.Include(achievement => achievement.Games)
            .Select(achievement => new AchievementViewModel()
            {
                Id = achievement.Id,
                Name = achievement.Name,
                Description = achievement.Description,
                GameId = achievement.GameId,
                Games = achievement.Games,
            }).ToList();
        }
        public List<SelectListItem> GetGames()
        {
            var list = new List<SelectListItem>();

            List<GameViewModel> games = gameServices.GetAll();
            list = games.Select(game => new SelectListItem()
            {
                Value = game.Id,
                Text = game.Title
            }).ToList();

            var defaultItem = new SelectListItem()
            {
                Value = null,
                Text = "---Select a Game---"
            };

            list.Insert(0, defaultItem);

            return list;
        }
        public async Task CreateAsync(AchievementViewModel model)
        {
            if(model.GameId == "")
            {
                //
            }
            else
            {
                Achievement achievement = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Description = model.Description,
                    GameId = model.GameId
                };

                //GameViewModel? game = gameServices.GetDetailsById(achievement.GameId);
                //game.Achievements.Add(achievement);

                //await gameServices.UpdateAsync(game);
                await context.Achievements.AddAsync(achievement);
                await context.SaveChangesAsync();
            }
        }
            
        public AchievementViewModel GetDetailsById(string id)
        {
            AchievementViewModel? model = context.Achievements.Include(achievement => achievement.Games)
            .Select(achievement => new AchievementViewModel
            {
                Id = achievement.Id,
                Name = achievement.Name,
                Description = achievement.Description,
                GameId = achievement.GameId,
                Games = achievement.Games
            }).SingleOrDefault(achievement => achievement.Id == id);

            return model;
        }
        public async Task UpdateAsync(AchievementViewModel model)
        {
            Achievement? achievement = await context.Achievements.FindAsync(model.Id);

            if (achievement != null)
            {
                achievement.Id = model.Id;
                achievement.Name = model.Name;
                achievement.Description = model.Description;
                achievement.GameId = model.GameId;

                context.Achievements.Update(achievement);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(AchievementViewModel model)
        {
            Achievement? achievement = await context.Achievements.FindAsync(model.Id);

            if (achievement != null)
            {
                context.Achievements.Remove(achievement);
                await context.SaveChangesAsync();
            }
        }
    }
}
