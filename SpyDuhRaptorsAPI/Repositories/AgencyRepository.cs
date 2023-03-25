using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly string _connectionString;
        public AgencyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Agency> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM AgencyLookup";
                    var reader = cmd.ExecuteReader();
                    var varieties = new List<Agency>();
                    while (reader.Read())
                    {
                        var variety = new Agency()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                        varieties.Add(variety);
                    }

                    reader.Close();

                    return varieties;
                }
            }
        }

        public Agency Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM AgencyLookup WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Agency variety = null;

                    if (reader.Read())
                    {
                        variety = new Agency()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    }

                    reader.Close();

                    return variety;
                }
            }
        }

        public void Add(Agency variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO AgencyLookup (Name) OUTPUT INSERTED.Id VALUES (@Name)";


                    cmd.Parameters.AddWithValue("@Name", variety.Name);

                    variety.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Agency variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE AgencyLookup 
                           SET Name = @Name
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", variety.Id);
                    cmd.Parameters.AddWithValue("@Name", variety.Name);

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
                    cmd.CommandText = "DELETE FROM AgencyLookup WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
