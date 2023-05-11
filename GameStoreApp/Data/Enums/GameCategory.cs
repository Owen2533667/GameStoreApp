using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Data.Enums
{
    /// <summary>
    /// A enum of the different game genres. If there is a genre that require more than one word, use Display tag.
    /// </summary>
    public enum GameGenre
    {
        Sandbox = 1,
        [Display(Name = "Real-Time Strategy (RTS)")]
        RTS,
        Shooter,
        [Display(Name = "Multiplayer oneline battle arena (MOBA)")]
        MOBA,
        [Display(Name = "Role-Playing Game (RPG)")]
        RPG,
        [Display(Name = "Sport Simulation")]
        SimulationSport,
        [Display(Name = "Action-Adventure")]
        ActionAdventure,
        Survival,
        Horror,
        Platformer,
        [Display(Name = "Turn-Based Stratagy")]
        TurnStrategy,
        [Display(Name = "Party Game")]
        Party,
        [Display(Name = "Grand Strategy")]
        GrandStrategy,
        [Display(Name = "Social Simulation")]
        SocialSimulation,
        Action,
        Racing,
        Simulation,
        Puzzle,
        Fighting

    }
}
