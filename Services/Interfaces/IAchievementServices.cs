using FastSteamCopy.Services.ViewModels;

namespace FastSteamCopy.Services.Interfaces
{
    public interface IAchievementServices
    {
        List<AchievementViewModel> GetAll();
        Task CreateAsync(AchievementViewModel model);
        AchievementViewModel GetDetailsById(string id);
        Task UpdateAsync(AchievementViewModel model);
        Task DeleteAsync(AchievementViewModel model);
    }
}
