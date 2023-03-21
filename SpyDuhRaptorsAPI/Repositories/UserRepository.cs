using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;
using System.Collections.Generic;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<User> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users";
                    var reader = cmd.ExecuteReader();
                    var varieties = new List<User>();
                    while (reader.Read())
                    {
                        var variety = new User()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Title")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            HandlerId = reader.GetInt32(reader.GetOrdinal("HandlerId")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                            AgencyId = reader.GetInt32(reader.GetOrdinal("AgencyId")),
                            AssignmentId = reader.GetInt32(reader.GetOrdinal("AssignmentId")),
                        };
                        varieties.Add(variety);
                    }

                    reader.Close();

                    return varieties;
                }
            }
        }

        public User Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Users WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    User variety = null;

                    if (reader.Read())
                    {
                        variety = new User()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Title")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            HandlerId = reader.GetInt32(reader.GetOrdinal("HandlerId")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            CountryId = reader.GetInt32(reader.GetOrdinal("CountryId")),
                            AgencyId = reader.GetInt32(reader.GetOrdinal("AgencyId")),
                            AssignmentId = reader.GetInt32(reader.GetOrdinal("AssignmentId")),
                        };
                    }

                    reader.Close();

                    return variety;
                }
            }
        }

        public void Add(User variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO USERS VALES (@Name,@CountryID,@Username,@Password,@AgencyID,@Type,@HandlerID,@AssignmentID,@Email)";


                    cmd.Parameters.AddWithValue("@Name", variety.Name);
                    cmd.Parameters.AddWithValue("@CountryID", variety.CountryId);
                    cmd.Parameters.AddWithValue("@Username", variety.UserName);
                    cmd.Parameters.AddWithValue("@Password", variety.Password);
                    cmd.Parameters.AddWithValue("@AgencyID", variety.AgencyId);
                    cmd.Parameters.AddWithValue("@Type", variety.Type);
                    cmd.Parameters.AddWithValue("@HandlerID", variety.HandlerId);
                    cmd.Parameters.AddWithValue("@AssignmentID", variety.AssignmentId);
                    cmd.Parameters.AddWithValue("@Email", variety.Email);



                    variety.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(User variety)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Users 
                           SET Name = @Name,
                               CountryId = @CountryId,
                               UserName = @UserName,
                               Password = @Password,
                               AgencyId = @AgencyId,
                               Type = @Type,
                               HandlerId = @HandlerId,
                               AssignmentId = @AssignmentId,
                               Email = @Email,
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", variety.Id);
                    cmd.Parameters.AddWithValue("@Name", variety.Name);
                    cmd.Parameters.AddWithValue("@CountryId", variety.CountryId);
                    cmd.Parameters.AddWithValue("@UserName", variety.UserName);
                    cmd.Parameters.AddWithValue("@Password", variety.Password);
                    cmd.Parameters.AddWithValue("@AgencyId", variety.AgencyId);
                    cmd.Parameters.AddWithValue("@Type", variety.Type);
                    cmd.Parameters.AddWithValue("@HandlerId", variety.HandlerId);
                    cmd.Parameters.AddWithValue("@AssignmentId", variety.AssignmentId);
                    cmd.Parameters.AddWithValue("@Email", variety.Email);


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
                    cmd.CommandText = "DELETE FROM Users WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
