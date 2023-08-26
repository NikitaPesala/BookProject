namespace ConceptArchitect.Utils
{
    public interface IBookRepository<Entity, Entity2, Id>
    {
        Task<Entity> Add(Entity entity);
        Task<List<Entity>> GetAll();

        Task<List<Entity>> GetAll(Func<Entity, bool> predicate);

        Task<Entity> GetById(Id id);

        Task<Entity> Update(Entity entity, Action<Entity,Entity> mergeOldNew);

        Task Delete(Id id);

        Task<Entity> Fav(Entity entity, string userId);

        Task<List<Entity>> GetAllFav(string userId);

        Task DeleteFav(string bookId, string userId);
    }
}