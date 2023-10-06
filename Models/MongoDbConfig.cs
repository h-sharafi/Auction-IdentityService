using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Models;

public class MongoDbConfig
{
    public string Name { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string ConnectionString { get; set; }

    public Dictionary<string, string>? CollectionNames { get; set; }
}