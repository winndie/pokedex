using Moq;
using Newtonsoft.Json;
using Pokedex.Controllers;
using Pokedex.Services;
using System.Net;
using System.Text;

namespace Pokedex.Tests
{
	public class PokedexControllerTest
	{
        private readonly Mock<IPokedexService> _service = new Mock<IPokedexService>();
        private readonly Mock<ILogger<PokedexController>> _logger = new Mock<ILogger<PokedexController>>();
        private readonly PokedexController _controller;

		public PokedexControllerTest()
		{
            _controller = new PokedexController(_service.Object, _logger.Object);
        }

        //[Fact]
        //public void GetEvolutionChain_ReturnsEvolutionChain()
        //{
        //    var pokemonName = "Pikachu";
        //    var result = _controller.Get(pokemonName);

        //    Assert.Equal(evolutionChain, result);
        //}
    }
}

