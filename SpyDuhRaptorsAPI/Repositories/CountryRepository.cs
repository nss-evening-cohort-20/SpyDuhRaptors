using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _connectionString;
        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Country> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Countries";
                    var reader = cmd.ExecuteReader();
                    var varieties = new List<Country>();
                    while (reader.Read())
                    {
                        var variety = new Country()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                            Region = reader.GetString(reader.GetOrdinal("Region")),
                        };
                        varieties.Add(variety);
                    }

                    reader.Close();

                    return varieties;
                }
            }
        }

        public Country Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Countries WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Country variety = null;

                    if (reader.Read())
                    {
                        variety = new Country()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                            Region = reader.GetString(reader.GetOrdinal("Region")),
                        };
                    }

                    reader.Close();

                    return variety;
                }
            }
        }

        public void Add(Country variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO Countries VALES (@CountryName, @Region)";


                    cmd.Parameters.AddWithValue("@CountryName", variety.CountryName);
                    cmd.Parameters.AddWithValue("@Region", variety.Region);

                    variety.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Country variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Countries 
                           SET CountryName = @CountryName,
                               Region = @Region,
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", variety.Id);
                    cmd.Parameters.AddWithValue("@CountryName", variety.CountryName);
                    cmd.Parameters.AddWithValue("@Region", variety.Region);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Countries WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
