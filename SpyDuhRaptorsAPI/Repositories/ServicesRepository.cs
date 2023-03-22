using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using SpyDuhRaptorsAPI.Models;
using System.Collections.Generic;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class ServicesRepository : BaseRepository, IServicesRepository
    {
        private const string _servicesSelect = @"SELECT s.Id
                                                 ,s.UserId
                                                 ,s.ServiceId
                                                 ,s.AvalibleToHire
                                                 ,sl.Id
                                                 ,sl.Name
                                                FROM Services s
                                                JOIN ServicesLookUp sl on sl.Id = s.ServiceId ";

        private const string _servicesInsert = @"INSERT INTO Services
                                               (Id, ServiceId, AvailibleToHire)
                                                OUTPUT INSERTED.Id
                                                VALUES
                                               (@Id, @ServiceId, @AvailibleToHire)";

        private const string _servicesUpdate = @"UPDATE Services
                                                SET Id = @Id
                                                ,Name = @Name
                                                ,ServiceId = @ServiceId
                                                ,AvailibleToHire = @AvailibleToHire
                                                WHERE Id = @id";

        private const string _servicesDelete = @"DELETE FROM Services
                                                WHERE Id = @id";

        public ServicesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Services> GetAll()
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _servicesSelect;

            using var reader = cmd.ExecuteReader();
            List<Services> results = new();

            while (reader.Read())
            {
                results.Add(ServicesFromReader(reader));
            }

            return results;
        }

        public Services? GetById(int id)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"{_servicesSelect} WHERE s.Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            Services? result = null;

            if (reader.Read())
            {
                result = ServicesFromReader(reader);
            }

            return result;
        }

        public bool Insert(Services service)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _servicesInsert;
            cmd.Parameters.AddWithValue("@Id", service.Id);
            cmd.Parameters.AddWithValue("@ServiceId", service.ServiceId);

            service.Id = (int)cmd.ExecuteScalar();
            return service.Id != 0;
        }

        public bool Update(Services service)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _servicesUpdate;
            cmd.Parameters.AddWithValue("@id", service.Id);
            cmd.Parameters.AddWithValue("@UserId", service.UserId);
            cmd.Parameters.AddWithValue("@ServiceId", service.ServiceId);
            cmd.Parameters.AddWithValue("@AvailibleToHire", service.AvailibleToHire);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Delete(int id)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _servicesDelete;

            return cmd.ExecuteNonQuery() > 0;
        }

        private Services ServicesFromReader(SqlDataReader reader)
        {
            return new Services()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                ServiceId = reader.GetInt32(reader.GetOrdinal("ServicesId")),
                AvailibleToHire = reader.GetBoolean(reader.GetOrdinal("AvailibleToHire")),
                Service = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ServicesId")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
                    AvailibleToHire = !reader.IsDBNull(reader.GetOrdinal("AvailibleToHire"))
                            ? reader.GetBoolean(reader.GetOrdinal("AvailibleToHire"))
                    : false
                            
                }
            };
        }
    }
}
