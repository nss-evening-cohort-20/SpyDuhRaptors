using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly string _connectionString;
        public AssignmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Assignment> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Assignment";
                    var reader = cmd.ExecuteReader();
                    var varieties = new List<Assignment>();
                    while (reader.Read())
                    {
                        var variety = new Assignment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Location = reader.GetString(reader.GetOrdinal("Location")),
                            StartTS = reader.GetDateTime(reader.GetOrdinal("StartTS")),
                            EndTS = reader.GetDateTime(reader.GetOrdinal("EndTS")),
                            IsUnderCover = reader.GetBoolean(reader.GetOrdinal("IsUnderCover")),
                        };
                        varieties.Add(variety);
                    }

                    reader.Close();

                    return varieties;
                }
            }
        }

        public Assignment Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Assignment WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    Assignment variety = null;

                    if (reader.Read())
                    {
                        variety = new Assignment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Location = reader.GetString(reader.GetOrdinal("Location")),
                            StartTS = reader.GetDateTime(reader.GetOrdinal("StartTS")),
                            EndTS = reader.GetDateTime(reader.GetOrdinal("EndTS")),
                            IsUnderCover = reader.GetBoolean(reader.GetOrdinal("IsUnderCover")),
                        };
                    }

                    reader.Close();

                    return variety;
                }
            }
        }

        public void Add(Assignment variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO Assignment (Name, Location, StartTS, EndTS, IsUnderCover) OUTPUT INSERTED.Id VALUES (@Name,@Location,@StartTS,@EndTS,@IsUnderCover)";


                    cmd.Parameters.AddWithValue("@Name", variety.Name);
                    cmd.Parameters.AddWithValue("@Location", variety.Location);
                    cmd.Parameters.AddWithValue("@StartTS", variety.StartTS);
                    cmd.Parameters.AddWithValue("@EndTS", variety.EndTS);
                    cmd.Parameters.AddWithValue("@IsUnderCover", variety.IsUnderCover);

                    variety.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Assignment variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Users 
                           SET Name = @Name,
                               Location = @Location,
                               StartTS = @StartTS,
                               EndTS = @EndTS,
                               IsUnderCover = @IsUnderCover
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", variety.Id);
                    cmd.Parameters.AddWithValue("@Name", variety.Name);
                    cmd.Parameters.AddWithValue("@Location", variety.Location);
                    cmd.Parameters.AddWithValue("@StartTS", variety.StartTS);
                    cmd.Parameters.AddWithValue("@EndTS", variety.EndTS);
                    cmd.Parameters.AddWithValue("@IsUnderCover", variety.IsUnderCover);


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
                    cmd.CommandText = "DELETE FROM Assignment WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
