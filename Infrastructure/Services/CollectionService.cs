using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using IdentityService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IdentityService.Infrastructure.Services;

public class CollectionService : ICollectionService
{
    private readonly IOptions<MongoDbConfig> databaseSettings;
    public CollectionService(IOptions<MongoDbConfig> databaseSettings)
    {
        this.databaseSettings = databaseSettings;
    }
    public IMongoCollection<TDocument> GetCollectionName<TDocument>()
    {
        var type = typeof(TDocument);
        string typeName = typeof(TDocument).Name;
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.Name);

        return mongoDatabase.GetCollection<TDocument>(
              databaseSettings.Value.CollectionNames
                  .Where(c => c.Key == typeName)
                  .Select(c => c.Value)
                  .FirstOrDefault());
    }
}