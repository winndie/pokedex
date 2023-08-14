using System;
using Pokedex.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Pokedex.Services
{
	public class PokedexService: IPokedexService
    {
		public PokedexService()
		{
		}

		public Variation GetEvolutionChain(string pokemonName)
		{
            var id = pokemonName == "pikachu" ? 1 :0;
            var uri = $"https://pokeapi.co/api/v2/evolution-chain/{id}";
            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var evolutions = JsonConvert.DeserializeObject<Variation>(json);
                return new Variation("caterpie", new List<Variation> {
                    new Variation("metapod", new List<Variation> {
                    new Variation("butterfree", new List<Variation> {})
                    })
                    });
            }
            return null;
        }
    }
}

