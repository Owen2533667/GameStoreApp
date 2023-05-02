using GameStoreApp.Data.Base;
using GameStoreApp.Data.ViewModel;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public class GameService : EntityBaseRepository<Game>, IGameService
    {
        private readonly GameStoreAppDbContext _context;

        public GameService(GameStoreAppDbContext context) : base(context)
        {
            _context = context;
        }

        //AddNewGameAsync is an asynchronos method that returns a task and accepts a newGameVM as a parameter. The purpose of this method is to add a new game into the related game table and other relationship data between game and voice actor/platform.
        public async Task AddNewGameAsync(NewGameVM data)
        {
            var newGame = new Game()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                GameRatingId = data.GameRatingId,
                GamePublisherId = data.GamePublisherId,
                GameDeveloperId = data.GameDeveloperId,
                ReleaseDate = data.ReleaseDate,
                GameGenre = data.GameGenre,
            };

            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();

            foreach (var VoiceActorId in data.VoiceActorIds)
            {
                var newVoiceActorGame = new VoiceActor_Game()
                {
                    GameId = newGame.Id,
                    VoiceActorId = VoiceActorId,
                };
                await _context.VoiceActors_Games.AddAsync(newVoiceActorGame);
            }

            foreach (var PlatformId in data.PlatformIds)
            {
                var newPlatformGame = new Platform_Game()
                {
                    GameId = newGame.Id,
                    PlatformId = PlatformId,
                };
                await _context.Platforms_Games.AddAsync(newPlatformGame);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            var data = await _context.Games
                .Include(p => p.GamePublisher)
                .Include(d => d.GameDeveloper)
                .Include(r => r.GameRating)
                .Include(vag => vag.VoiceActors_Games).ThenInclude(va => va.VoiceActor)
                .Include(pg => pg.Platforms_Games).ThenInclude(p => p.Platform)
                .FirstOrDefaultAsync(x => x.Id == id);

            return data;
        }

        public async Task<NewGameDropdownVM> GetNewGameDropDownValues()
        {
            var response = new NewGameDropdownVM();
            response.VoiceActor = await _context.VoiceActors.OrderBy(x => x.FullName).ToListAsync();
            response.Publisher = await _context.GamePublishers.OrderBy(x => x.Name).ToListAsync();
            response.Developer = await _context.GameDevelopers.OrderBy(x => x.Name).ToListAsync();
            response.Platform = await _context.Platforms.OrderBy(x => x.Name).ToListAsync();
            response.Rating = await _context.GameRatings.OrderBy(x => x.Name).ToListAsync();

            return response;

        }

        public async Task UpdateGameAsync(NewGameVM data)
        {
            var dbGame = await _context.Games.FirstOrDefaultAsync(x => x.Id == data.Id);

            if (dbGame != null)
            {
                dbGame.Name = data.Name;
                dbGame.Description = data.Description;
                dbGame.Price = data.Price;
                dbGame.ImageURL = data.ImageURL;
                dbGame.GamePublisherId = data.GamePublisherId;
                dbGame.GameDeveloperId = data.GameDeveloperId;
                dbGame.GameRatingId = data.GameRatingId;
                dbGame.ReleaseDate = data.ReleaseDate;
                dbGame.GameGenre = data.GameGenre;
                await _context.SaveChangesAsync();
            }

            //Remove existing voice actors
            var existingVoiceActorDb = _context.VoiceActors_Games.Where(x => x.GameId == data.Id).ToList();
            _context.VoiceActors_Games.RemoveRange(existingVoiceActorDb);
            await _context.SaveChangesAsync();

            //Remove existing platforms
            var existingPlatformsDb = _context.Platforms_Games.Where(x => x.GameId == data.Id).ToList();
            _context.Platforms_Games.RemoveRange(existingPlatformsDb);
            await _context.SaveChangesAsync();


            //
            foreach (var VoiceActorId in data.VoiceActorIds)
            {
                var newVoiceActorGame = new VoiceActor_Game()
                {
                    GameId = data.Id,
                    VoiceActorId = VoiceActorId,
                };
                await _context.VoiceActors_Games.AddAsync(newVoiceActorGame);
            }
            await _context.SaveChangesAsync();

            foreach (var PlatformIds in data.PlatformIds)
            {
                var newPlatformGame = new Platform_Game()
                {
                    GameId = data.Id,
                    PlatformId = PlatformIds,
                };
                await _context.Platforms_Games.AddAsync(newPlatformGame);
            }
            await _context.SaveChangesAsync();
        }

    }
}
