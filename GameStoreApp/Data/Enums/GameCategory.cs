using System.ComponentModel.DataAnnotations;

namespace GameStoreApp.Data.Enums
{
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
        Party

    }
}
