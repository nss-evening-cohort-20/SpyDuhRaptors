using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;
using System.Collections.Generic;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class ServicesLookUpRepository : BaseRepository, IServicesLookUpRepository
    {
        private const string _servicesLookUpSelect = @"SELECT sl.Id
                                                        ,sl.Name
                                                      FROM ServicesLookUp sl";

        public ServicesLookUpRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<ServicesLookUp> GetAll()
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"Select Id,Name
                                   From ServicesLookUp";

            using var reader = cmd.ExecuteReader();
            List<ServicesLookUp> results = new();

            while (reader.Read())
            {
                results.Add(ServicesLookUpFromReader(reader));
            }

            return results;
        }

        public ServicesLookUp? GetById(int id)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"{_servicesLookUpSelect} WHERE s.Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            ServicesLookUp? result = null;

            if (reader.Read())
            {
                result = ServicesLookUpFromReader(reader);
            }

            return result;
        }

        private ServicesLookUp ServicesLookUpFromReader(SqlDataReader reader)
        {
            return new ServicesLookUp()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Services = new ServicesLookUp()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                }
            };
        }
    }
}
