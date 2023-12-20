using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Models.DTOs;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonServcie;
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonServcie = pokemonService;
        }

        [HttpGet()]
        public ActionResult<List<Pokemon>> GetPokemons()
        {
            return Ok(_pokemonServcie.GetPokemons());
        }

        [HttpGet("{Id}")]
        public ActionResult<Pokemon> GetPokemon(string Id)
        {
            return Ok(_pokemonServcie.GetPokemonById(Id));
        }

        [HttpPost]
        public ActionResult<Pokemon> AddPokemon([FromBody] AddPokemonDTO pokemon)
        {
            Pokemon newPokemon = _pokemonServcie.AddPokemon(pokemon);
            return Ok(newPokemon);

        }


        [HttpPost("database")]
        public async Task<ActionResult<bool>> CreatePokemon([FromBody] Pokemon pokemon)
        {
            return Ok(await _pokemonServcie.AddPokemonDB(pokemon));
        }

        [HttpPatch()]
        public ActionResult<UpdatePokemonDTO> UpdatePokemon([FromQuery] string pokemonId, [FromBody] UpdatePokemonDTO updatedPokemon)
        {
            UpdatePokemonDTO updateResult = _pokemonServcie.UpdatePokemon(pokemonId, updatedPokemon);

            var resourceUri = Url.Action("GetPokemonById", updateResult) ?? "";
            return Created(resourceUri, updateResult);

        }

        [HttpDelete]
        public ActionResult<bool> DeletePokemon([FromQuery] string pokemonId)
        {
            var result = _pokemonServcie.DeletePokemonById(pokemonId);
            return Ok(result);
        }
    }
}