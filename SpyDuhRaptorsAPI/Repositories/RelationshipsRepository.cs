using Microsoft.Extensions.Hosting;
using SpyDuhRaptorsAPI.Models;
using SpyDuhRaptorsAPI.Utilities;

namespace SpyDuhRaptorsAPI.Repositories
{
    public class RelationshipsRepository : BaseRepository, IRelationshipsRepository
    {
        public RelationshipsRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IList<RelationshipsDto> GetAllEnemies(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      select u1.Id as Id, u1.name as Name, u1.UserName, u2.Name as HandlerName, u1.Email,
                      c1.CountryName,
                      a1.Name as AgencyName, a2.Name as CurrentAssignmentName
                      from Relationships r1
                      inner join users u1 on u1.Id = r1.RelatedUserId
                      inner join users u2 on u2.Id = u1.HandlerId
                      inner join [dbo].[AgencyLookup] a1 on a1.Id = u1.AgencyId
                      inner join [dbo].[Assignment] a2 on a2.Id = u1.AssignmentId
                      inner join [dbo].[Countries] c1 on c1.id = u1.CountryId
                      where r1.UserId = @UserId
                      and a2.StartTS = (select max(StartTS) from [dbo].[Assignment] s1 where s1.id = u1.AssignmentId)
                      and r1.isFriend = 0

                      union

                      select u1.Id as Id, u1.name as Name, u1.UserName, u2.Name as HandlerName, u1.Email,
                      c1.CountryName,
                      a1.Name as AgencyName, a2.Name as CurrentAssignmentName
                      from Relationships r1
                      inner join users u1 on u1.Id = r1.UserId
                      inner join users u2 on u2.Id = u1.HandlerId
                      inner join [dbo].[AgencyLookup] a1 on a1.Id = u1.AgencyId
                      inner join [dbo].[Assignment] a2 on a2.Id = u1.AssignmentId
                      inner join [dbo].[Countries] c1 on c1.id = u1.CountryId
                      where r1.RelatedUserId =  @UserId
                      and a2.StartTS = (select max(StartTS) from [dbo].[Assignment] s1 where s1.id = u1.AssignmentId)
                      and r1.isFriend = 0";

                    DbUtils.AddParameter(cmd, "@UserId", userId);

                    var reader = cmd.ExecuteReader();

                    var enemiesDtoList = new List<RelationshipsDto>();
                    while (reader.Read())
                    {
                        enemiesDtoList.Add(new RelationshipsDto(

                            DbUtils.GetInt(reader, "Id"),
                            DbUtils.GetString(reader, "Name"),
                            DbUtils.GetString(reader, "UserName"),
                            DbUtils.GetString(reader, "HandlerName"),
                            DbUtils.GetString(reader, "Email"),
                            DbUtils.GetString(reader, "CountryName"),
                            DbUtils.GetString(reader, "AgencyName"),
                            DbUtils.GetString(reader, "CurrentAssignmentName")
                        ));
                    }

                    reader.Close();

                    return enemiesDtoList;
                }
            }
        }

        public IList<RelationshipsDto> GetAllFriends(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      select u1.Id as Id, u1.name as Name, u1.UserName, u2.Name as HandlerName, u1.Email,
                      c1.CountryName,
                      a1.Name as AgencyName, a2.Name as CurrentAssignmentName
                      from Relationships r1
                      inner join users u1 on u1.Id = r1.RelatedUserId
                      inner join users u2 on u2.Id = u1.HandlerId
                      inner join [dbo].[AgencyLookup] a1 on a1.Id = u1.AgencyId
                      inner join [dbo].[Assignment] a2 on a2.Id = u1.AssignmentId
                      inner join [dbo].[Countries] c1 on c1.id = u1.CountryId
                      where r1.UserId = @UserId
                      and a2.StartTS = (select max(StartTS) from [dbo].[Assignment] s1 where s1.id = u1.AssignmentId)
                      and r1.isFriend = 1

                      union

                      select u1.Id as Id, u1.name as Name, u1.UserName, u2.Name as HandlerName, u1.Email,
                      c1.CountryName,
                      a1.Name as AgencyName, a2.Name as CurrentAssignmentName
                      from Relationships r1
                      inner join users u1 on u1.Id = r1.UserId
                      inner join users u2 on u2.Id = u1.HandlerId
                      inner join [dbo].[AgencyLookup] a1 on a1.Id = u1.AgencyId
                      inner join [dbo].[Assignment] a2 on a2.Id = u1.AssignmentId
                      inner join [dbo].[Countries] c1 on c1.id = u1.CountryId
                      where r1.RelatedUserId =  @UserId
                      and a2.StartTS = (select max(StartTS) from [dbo].[Assignment] s1 where s1.id = u1.AssignmentId)
                      and r1.isFriend = 1";

                    DbUtils.AddParameter(cmd, "@UserId", userId);

                    var reader = cmd.ExecuteReader();

                    var friendsDtoList = new List<RelationshipsDto>();
                    while (reader.Read())
                    {
                        friendsDtoList.Add(new RelationshipsDto(

                            DbUtils.GetInt(reader, "Id"),
                            DbUtils.GetString(reader, "Name"),
                            DbUtils.GetString(reader, "UserName"),
                            DbUtils.GetString(reader, "HandlerName"),
                            DbUtils.GetString(reader, "Email"),
                            DbUtils.GetString(reader, "CountryName"),
                            DbUtils.GetString(reader, "AgencyName"),
                            DbUtils.GetString(reader, "CurrentAssignmentName")
                        ));
                    }

                    reader.Close();

                    return friendsDtoList;
                }
            }
        }
    }
}
