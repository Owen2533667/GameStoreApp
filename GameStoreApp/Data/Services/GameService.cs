using GameStoreApp.Data.Base;
using GameStoreApp.Data.ViewModel;
using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services

    /// <summary>
    /// Represents a service for managing games.
    /// Implements the <see cref="EntityBaseRepository{T}"/> class and the <see cref="IGameService"/> interface.
    /// </summary>
    public class GameService : EntityBaseRepository<Game>, IGameService
{
    private readonly GameStoreAppDbContext _context;

    /// <summary>
    /// Initialises a new instance of the <see cref="GameService"/> class.
    /// </summary>
    /// <param name="context">The <see cref="GameStoreAppDbContext"/> database context.</param>
    public GameService(GameStoreAppDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new game asynchronously.
    /// </summary>
    /// <param name="data">The data of the new game.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddNewGameAsync(NewGameVM data)
    {
        // Create a new Game instance with the provided data
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

        // Add the new game to the context
        await _context.Games.AddAsync(newGame);
        await _context.SaveChangesAsync();

        // If there are voice actors for this game, add them to the VoiceActors_Games table
        if (data.VoiceActorIds?.Count() > 0) // Checks VoiceActorIds count is greater than zero.
        {
            // For each of the elements in the VoiceActorId list
            foreach (var VoiceActorId in data.VoiceActorIds)
            {
                // Create a new Voiceactor_Game instance with the element data.
                var newVoiceActorGame = new VoiceActor_Game()
                {
                    GameId = newGame.Id, // Set the GameId property of the new VoiceActor_Game instance to the Id of the newly created game
                    VoiceActorId = VoiceActorId, // Set the VoiceActorId property of the new VoiceActor_Game instance to the current voice actor id in the loop
                };
                await _context.VoiceActors_Games.AddAsync(newVoiceActorGame); // Add the new VoiceActor_Game instance to the context
            }
        }

        // If there are platforms for this game, add them to the Platforms_Games table
        if (data.PlatformIds?.Count() > 0)
        {
            // For each of the elements in the PlatformIds list
            foreach (var PlatformId in data.PlatformIds)
            {
                // Create a new Voiceactor_Game instance with the element data.
                var newPlatformGame = new Platform_Game()
                {
                    GameId = newGame.Id, // Set the GameId property of the new Platform_Game instance to the Id of the newly created game
                    PlatformId = PlatformId, // Set the PlatformId property of the new Platform_Game instance to the current platform id in the loop
                };
                await _context.Platforms_Games.AddAsync(newPlatformGame); // Add the new Platform_Game instance to the context
            }
        }

        // Save all changes made to the context
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves a game by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the game to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The retrieved game.</returns>
    public async Task<Game> GetGameByIdAsync(int id)
    {
        // Retrieve the game from the context including related entities
        var data = await _context.Games
                .Include(p => p.GamePublisher)
                .Include(d => d.GameDeveloper)
                .Include(r => r.GameRating)
                .Include(vag => vag.VoiceActors_Games)!.ThenInclude(va => va.VoiceActor)
                .Include(pg => pg.Platforms_Games)!.ThenInclude(p => p.Platform)
                .FirstOrDefaultAsync(x => x.Id == id);

        // Return the game with the included properties
        return data!;
    }

    /// <summary>
    /// Retrieves dropdown values for creating a new game asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The dropdown values for a new game.</returns>
    public async Task<NewGameDropdownVM> GetNewGameDropDownValues()
    {
        // Create a new instance of the NewGameDropdownVM class to store the dropdown values
        var response = new NewGameDropdownVM();

        // Retrieve voice actors from the context and order them by FullName
        response.VoiceActor = await _context.VoiceActors.OrderBy(x => x.FullName).ToListAsync();
        // Retrieve publishers from the context and order them by Name
        response.Publisher = await _context.GamePublishers.OrderBy(x => x.Name).ToListAsync();
        // Retrieve developers from the context and order them by Name
        response.Developer = await _context.GameDevelopers.OrderBy(x => x.Name).ToListAsync();
        // Retrieve platforms from the context and order them by Name
        response.Platform = await _context.Platforms.OrderBy(x => x.Name).ToListAsync();
        // Retrieve ratings from the context and order them by Name
        response.Rating = await _context.GameRatings.OrderBy(x => x.Name).ToListAsync();

        return response; // Return the response
    }

    /// <summary>
    /// Updates a game asynchronously with the provided data.
    /// </summary>
    /// <param name="data">The data to update the game.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateGameAsync(NewGameVM data)
    {
        // Retrieve the game from the context based on the provided ID
        var dbGame = await _context.Games.FirstOrDefaultAsync(x => x.Id == data.Id);

        // If the dbGame result is not null, meaning it matched a game from the db
        if (dbGame != null)
        {
            // Update the game properties with the provided data
            dbGame.Name = data.Name;
            dbGame.Description = data.Description;
            dbGame.Price = data.Price;
            dbGame.ImageURL = data.ImageURL;
            dbGame.GamePublisherId = data.GamePublisherId;
            dbGame.GameDeveloperId = data.GameDeveloperId;
            dbGame.GameRatingId = data.GameRatingId;
            dbGame.ReleaseDate = data.ReleaseDate;
            dbGame.GameGenre = data.GameGenre;

            // Save the changes to the context
            await _context.SaveChangesAsync();
        }

        // Remove existing voice actors associated with the game
        var existingVoiceActorDb = _context.VoiceActors_Games.Where(x => x.GameId == data.Id).ToList();
        _context.VoiceActors_Games.RemoveRange(existingVoiceActorDb);
        await _context.SaveChangesAsync();

        // Remove existing platforms associated with the game
        var existingPlatformsDb = _context.Platforms_Games.Where(x => x.GameId == data.Id).ToList();
        _context.Platforms_Games.RemoveRange(existingPlatformsDb);
        await _context.SaveChangesAsync();

        // Associate the game with the specified voice actors
        if (data.VoiceActorIds?.Count() > 0)
        {
            foreach (var VoiceActorId in data.VoiceActorIds)
            {
                var newVoiceActorGame = new VoiceActor_Game()
                {
                    GameId = data.Id,
                    VoiceActorId = VoiceActorId,
                };
                await _context.VoiceActors_Games.AddAsync(newVoiceActorGame);
            }

            // Save the changes to the context
            await _context.SaveChangesAsync();
        }

        // Associate the game with the specified platforms
        if (data.PlatformIds?.Count() > 0)
        {
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

        // Save the changes to the context
        await _context.SaveChangesAsync();
    }

}
}
