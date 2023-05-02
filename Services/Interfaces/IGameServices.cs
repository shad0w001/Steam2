using FastSteamCopy.Services.ViewModels;

namespace FastSteamCopy.Services.Interfaces
{
    public interface IGameServices
    {
        List<GameViewModel> GetAll();
        Task CreateAsync(GameViewModel model);
        GameViewModel GetDetailsById(string id);       
        Task UpdateAsync(GameViewModel model);
        Task DeleteAsync(GameViewModel model);
    }
}
