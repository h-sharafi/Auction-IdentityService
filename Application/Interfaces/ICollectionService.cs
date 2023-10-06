using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IdentityService.Application.Interfaces;

public interface ICollectionService
{

    IMongoCollection<TDocument> GetCollectionName<TDocument>();
}