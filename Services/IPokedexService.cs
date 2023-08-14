using Pokedex.Models;

namespace Pokedex.Services
{
	public interface IPokedexService
    {
        ServiceResult<Variation> GetEvolutionChain(string pokemonName);
    }
}

