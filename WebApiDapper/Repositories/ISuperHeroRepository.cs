using WebApiDapper.Models;

namespace WebApiDapper.Repositories
{
    public interface ISuperHeroRepository
    {
        Task<IEnumerable<SuperHero>> GetSuperHeroes();
        Task<SuperHero> GetSuperHero(int id);
        Task<SuperHero> Add(SuperHero hero);
        Task<SuperHero> Update(SuperHero hero);
        Task<bool> Delete(int id);
    }
}
