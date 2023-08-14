using Pokedex.Models;

namespace Pokedex.Services
{
	public interface IPokedexService
    {
        Variation GetEvolutionChain(string pokemonName);
    }
}

