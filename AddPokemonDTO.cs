using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonAPI.Models
{
    public class AddPokemonDTO
    {
        public string Name { get; set; } = "Pikachu";
        public string Type { get; set; } = "Electric";
        public string Power { get; set; } = "Shock";
        public int Level { get; set; } = 2;

        public AddPokemonDTO(Pokemon pokemon)
        {
            Name = pokemon.Name;
            Type = pokemon.Type;
            Power = pokemon.Power;
            Level = pokemon.Level;
        }

        public AddPokemonDTO()
        {

        }

        public Pokemon GetPokemonObject()
        {
            return new Pokemon() { Name = this.Name, Type = this.Type, Power = this.Power, Level = this.Level };
        }
    }
}