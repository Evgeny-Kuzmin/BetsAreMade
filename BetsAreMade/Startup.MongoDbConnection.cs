using BetsAreMade.DataContracts.Dbo.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BetsAreMade
{
    public partial class Startup
    {
        public static void ConfigureMongoDb(IServiceCollection services, IConfiguration config)
        {
            var connectionString =
                config.GetValue<string>("MongoDB:ConnectionStrings")
                .Replace("{user}", config.GetValue<string>("MongoDb:User"))
                .Replace("{password}", config.GetValue<string>("MongoDb:Password"));

            services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
            services.AddSingleton<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(config.GetValue<string>("MongoDb:DbName")));
            services.AddSingleton<IMongoCollection<UserDbo>>(sp => sp.GetRequiredService<IMongoDatabase>().GetCollection<UserDbo>(config.GetValue<string>("MongoDb:CollectionName")));
        }

        public static void RegisterClassMap()
        {
            BsonClassMap.RegisterClassMap<UserDbo>(
                (map) =>
                {
                    map.AutoMap();
                    map.SetIdMember(map.GetMemberMap(c => c.Id));
                    map.SetIgnoreExtraElements(true);
                });

            BsonClassMap.RegisterClassMap<BetDbo>(
               (map) =>
               {
                   map.SetIdMember(map.GetMemberMap(c => c.Id));
                   map.AutoMap();
                   map.SetIgnoreExtraElements(true);
               });
        }
    }
}
