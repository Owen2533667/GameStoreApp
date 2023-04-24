using GameStoreApp.Data.Base;
using GameStoreApp.Data.ViewModel;
using GameStoreApp.Models;

namespace GameStoreApp.Data.Services
{
    public interface IGameService : IEntityBaseRepository<Game>
    {
        Task<Game> GetGameByIdAsync(int id);
        Task<NewGameDropdownVM> GetNewGameDropDownValues();
        Task AddNewGameAsync(NewGameVM data);
        Task UpdateGameAsync(NewGameVM data);
    }
}
