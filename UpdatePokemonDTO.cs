using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonAPI.Models.DTOs
{
    public class UpdatePokemonDTO
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Power { get; set; }
        public int? Level { get; set; }
        public bool? Updated { get; set; }

        public UpdatePokemonDTO(Pokemon pokemon)
        {
            Name = pokemon.Name;
            Type = pokemon.Type;
            Power = pokemon.Power;
            Level = pokemon.Level;
        }
        public UpdatePokemonDTO(){}
    }
}