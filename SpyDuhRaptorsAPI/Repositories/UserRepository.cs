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

        public List<UserDto> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select U.ID, U.Name, C.CountryName as 'Country', U.UserName, AL.Name as 'Agency', U.Type, A.Name as 'Assignment', A.Location as 'Assignment Location', A.StartTS as 'Start Time', A.EndTS as 'End Time', A.IsUnderCover as 'Undercover' from Users U JOIN Assignment A on U.AssignmentId = A.ID JOIN AgencyLookup AL on U.AgencyId = AL.ID JOIN Countries C on U.CountryId = c.Id ";
                    var reader = cmd.ExecuteReader();
                    var varieties = new List<UserDto>();
                    while (reader.Read())
                    {
                        var variety = new UserDto()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Country = reader.GetString(reader.GetOrdinal("Country")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Agency = reader.GetString(reader.GetOrdinal("Agency")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Assignment = reader.GetString(reader.GetOrdinal("Assignment")),
                            AssignmentLocation = reader.GetString(reader.GetOrdinal("Assignment Location")),
                            StartTS = reader.GetDateTime(reader.GetOrdinal("Start Time")),
                            EndTS = reader.GetDateTime(reader.GetOrdinal("End Time")),
                            IsUnderCover = reader.GetBoolean(reader.GetOrdinal("Undercover")),
                        };
                        varieties.Add(variety);
                    }


                    reader.Close();

                    return varieties;
                }
            }
        }

        public UserDto Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select U.ID, U.Name, C.CountryName as 'Country', U.UserName, AL.Name as 'Agency', U.Type, A.Name as 'Assignment', A.Location as 'Assignment Location', A.StartTS as 'Start Time', A.EndTS as 'End Time', A.IsUnderCover as 'Undercover' from Users U JOIN Assignment A on U.AssignmentId = A.ID JOIN AgencyLookup AL on U.AgencyId = AL.ID JOIN Countries C on U.CountryId = c.Id WHERE U.Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    UserDto variety = null;

                    if (reader.Read())
                    {
                        variety = new UserDto()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Country = reader.GetString(reader.GetOrdinal("Country")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Agency = reader.GetString(reader.GetOrdinal("Agency")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Assignment = reader.GetString(reader.GetOrdinal("Assignment")),
                            AssignmentLocation = reader.GetString(reader.GetOrdinal("Assignment Location")),
                            StartTS = reader.GetDateTime(reader.GetOrdinal("Start Time")),
                            EndTS = reader.GetDateTime(reader.GetOrdinal("End Time")),
                            IsUnderCover = reader.GetBoolean(reader.GetOrdinal("Undercover")),
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

                    cmd.CommandText = @"INSERT INTO USERS (Name, CountryID, Username, Password, AgencyId, Type, HandlerId, AssignmentId, Email) OUTPUT INSERTED.Id VALUES (@Name,@CountryID,@Username,@Password,@AgencyID,@Type,@HandlerID,@AssignmentID,@Email)";


                    cmd.Parameters.AddWithValue("@Name", variety.Name);
                    cmd.Parameters.AddWithValue("@CountryID", variety.CountryId);
                    cmd.Parameters.AddWithValue("@Username", variety.UserName);
                    cmd.Parameters.AddWithValue("@Password", variety.Password);
                    cmd.Parameters.AddWithValue("@AgencyId", variety.AgencyId);
                    cmd.Parameters.AddWithValue("@Type", variety.Type);
                    cmd.Parameters.AddWithValue("@HandlerId", variety.HandlerId);
                    cmd.Parameters.AddWithValue("@AssignmentId", variety.AssignmentId);
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
                               Email = @Email
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
