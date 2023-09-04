using Pokedex.Services;
using Xunit;

namespace Pokedex.Tests
{
    public class PokedexTests
    {
        [Fact]
        public void GetEvolutionChain_For_NotAPokemon_Returns_Fail()
        {
            var pokedexService = new PokedexService();
            var result = pokedexService.GetEvolutionChain("Not a Pokemon");

            Assert.False(result.IsSuccess, "Not a Pokemon");
        }

        [Fact]
        public void GetEvolutionChain_For_Charmeleon_Returns_Success()
        {
            var pokedexService = new PokedexService();
            var result = pokedexService.GetEvolutionChain("Charmeleon");

            Assert.True(result.IsSuccess, "Charmeleon");
            Assert.Equal(1, result.Data.Variations.Count());
        }
    }
}
