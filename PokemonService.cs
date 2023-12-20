using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonAPI.Exceptions;
using PokemonAPI.Models;
using PokemonAPI.Models.DTOs;
using PokemonAPI.utils;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly List<Pokemon> pokemons = new(){
        new Pokemon(){Id = "002", Name="Balbashur", Type="Fire", Power="Elemental", Level=3},
        new Pokemon(){Id = "001"}
    };
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IOptions<MongoDBSettings> settings)
        {
            //The following can be moved to another class
            var mongoClient = new MongoClient(settings.Value.ConnectionUrl);
            var db = mongoClient.GetDatabase(settings.Value.Name);
            _pokemonCollection = db.GetCollection<Pokemon>("Pokemon");
        }

        public async Task<bool> AddPokemonDB(Pokemon newPokemon)
        {
            await _pokemonCollection.InsertOneAsync(newPokemon);
            return true;
        }

        // public async Task UpdatePokemonDB(Pokemon updatedPokemon){
        //     var update = Builders<BsonDocument>.Update.Set()    
        //     _pokemonCollection.UpdateOneAsync(pokemon => pokemon.Name == updatedPokemon.Name, );
        // }
        public Pokemon AddPokemon(AddPokemonDTO pokemonDTO)
        {
            Pokemon pokemon = pokemonDTO.GetPokemonObject();

            pokemons.Add(pokemon);
            return pokemon;
        }

        public bool DeletePokemonById(string pokemonId)
        {
            Pokemon? pokemon = pokemons.Find(pokemon => pokemon.Id == pokemonId);
            Console.WriteLine(pokemon);
            if (pokemon == null)
            {
                return false;
            }
            pokemons.Remove(pokemon);
            pokemon = pokemons.Find(pokemon => pokemon.Id == pokemonId);
            return pokemon == null;
        }


        public UpdatePokemonDTO UpdatePokemon(string pokemonId, UpdatePokemonDTO updatedPokemon)
        {
            //Summary:
            //       This method updates the Pokemon if there are only update fields set and also the values of those update fields
            //       are different from the current value of the Pokemon.
            //Parameters: 
            //          pokemonId -      the id of the pokemon to be Updated
            //          updatedPokemon - the fields and their corresponding values of the pokemon to be updated. 
            //Behaviour: 
            //          This method throws an exception if there is no given update fields or the pokemon to be updated doesn't exist.


            bool updated = false;
            var pokemonType = typeof(Pokemon);

            //If there is no update fields given.
            if (updatedPokemon == null) throw new InvalidUpdateDataException("No update data provided");

            //If the pokemon with the id isn't found.
            Pokemon? pokemon = pokemons.Find(pokemon => pokemon.Id == pokemonId) ?? throw new PokemonNotFoundException(pokemonId);

            pokemons.Remove(pokemon);

            //This for loop is used to dynamically make the checks involving both the UpdatePokemonDTO and the Pokemon itself. 
            //If the checks are passed it will update the object's fields.
            foreach (var property in pokemonType.GetProperties())
            {
                string propName = property.Name;
                object? propValue = property.GetValue(pokemon);
                var propertyToBeSet = pokemonType.GetProperty(propName);
                var newValue = typeof(UpdatePokemonDTO).GetProperty(propName)?.GetValue(updatedPokemon) ?? propValue; //Check if null and set it to the same value to prevent unintentional update

                bool fieldUpdateSet = propValue != null;
                bool fieldUpdated = propValue != newValue;

                if (fieldUpdateSet & fieldUpdated)
                {
                    propertyToBeSet?.SetValue(pokemon, newValue);
                    updated = true;
                }
            }

            pokemons.Add(pokemon);
            return new UpdatePokemonDTO(pokemon) { Updated = updated };
        }

        public Pokemon? GetPokemonById(string id)
        {
            // Another way of handling this would be to use the following code
            // var p = pokemons.Find(p => p.Id == id);
            // if (p != null)
            // {
            //     return p;
            // }
            // else
            // {
            //     throw new Exception("No pokemon found");
            // }
            return pokemons.Find(p => p.Id == id);
        }

        public List<Pokemon> GetPokemons()
        {
            return pokemons;
        }
    }
}