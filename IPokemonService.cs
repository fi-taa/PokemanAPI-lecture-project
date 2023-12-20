using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonAPI.Models;
using PokemonAPI.Models.DTOs;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        List<Pokemon> GetPokemons();
        Pokemon? GetPokemonById(string id);

        Pokemon AddPokemon(AddPokemonDTO pokemon);
        public Task<bool> AddPokemonDB(Pokemon newPokemon);

        public UpdatePokemonDTO UpdatePokemon(string pokemonId, UpdatePokemonDTO updatedPokemon);
        public bool DeletePokemonById(string pokemonId);

    }
}