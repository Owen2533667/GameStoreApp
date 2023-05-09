namespace GameStoreApp.Data.Base
{
    /// <summary>
    /// Represents an entity that has a unique identifier.
    /// </summary>
    public interface IEntityBase 
    {
        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        int Id { get; set; }
    }
}
