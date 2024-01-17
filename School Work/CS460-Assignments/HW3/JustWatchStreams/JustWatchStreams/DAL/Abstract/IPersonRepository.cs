using JustWatchStreams.Models;

namespace JustWatchStreams.DAL.Abstract;

public interface IPersonRepository : IRepository<Person>
{
    IQueryable<Person> GetAllActors();
    Person FindActorById(int id);
    Person FindByJustWatchId(int id);

}