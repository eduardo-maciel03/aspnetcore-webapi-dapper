using Dapper;
using System.Data.SqlClient;
using WebApiDapper.Models;

namespace WebApiDapper.Repositories
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly string? _connectionString;
        public SuperHeroRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SqlServer");
        }
        public async Task<IEnumerable<SuperHero>> GetSuperHeroes()
        {
            var query = "select * from superheroes";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryAsync<SuperHero>(query);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SuperHero> GetSuperHero(int id)
        {
            var query = "select * from superheroes where id = @Id";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryFirstAsync<SuperHero>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SuperHero> Add(SuperHero hero)
        {
            var query = "insert into superheroes (name, firstname, lastname, place) values (@Name, @FirstName, @LastName, @Place)";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(query, hero);
                    return hero;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SuperHero> Update(SuperHero hero)
        {
            var query = "update superheroes set name = @Name, firstname = @FirstName, lastname = @LastName, place = @Place where id = @Id";
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(query, hero);
                    return hero;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = "delete from superheroes where id = @Id";
            var parameters = new { Id = id };

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
