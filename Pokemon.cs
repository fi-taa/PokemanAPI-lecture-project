using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonAPI.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = "Pikachu";
        public string Power { get; set; } = "Electric";
        public string Type { get; set; } = "Shock";
        public int Level { get; set; } = 2;
    }
}