using Microsoft.AspNetCore.SignalR;
using SpyDuhRaptorsAPI.Models;
using System.Collections.Generic;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class ServicesRepository
    {
        public static IRelationshipsRepository Getrelationships(string args)
        {
            IConfiguration config = null;
            IRelationshipsRepository temp = new RelationshipsRepository(config);
            return temp;
        }

        public static IRelationshipsRepository GetServices(string args)
        {
            IConfiguration config = null;
            IRelationshipsRepository temp = new RelationshipsRepository(config);
            return temp;
        }


        //private readonly IDbContext _context;

        //public ServicesRepository(IDbContext context)
        //{
        //    _context = context;
        //}

        //public List<Services> GetAll()
        //{
        //    using var conn = _context.GetConnection();
        //    conn.Open();
        //    using var cmd = conn.CreateCommand();
        //    cmd.CommandText = "SELECT Id, UserId, ServiceId, AvailableToHire FROM Services;";
        //    using var reader = cmd.ExecuteReader();
        //    var services = new List<Services>();
        //    while (reader.Read())
        //    {
        //        var service = new Services()
        //        {
        //            Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //            UserId = reader.GetString(reader.GetOrdinal("UserId")),
        //            ServiceId = reader.GetInt32(reader.GetOrdinal("ServiceId")),
        //            AvailableToHire = reader.GetBoolean(reader.GetOrdinal("AvailableToHire"))
        //        };
        //        services.Add(service);
        //    }

        //    return services;
        //}
    }
}
