using Pokedex.Models;
using Newtonsoft.Json.Linq;

namespace Pokedex.Services
{
	public class PokedexService: IPokedexService
    {
		public PokedexService()
		{
		}

		public ServiceResult<Variation> GetEvolutionChain(string pokemonName)
		{
            var name = pokemonName.ToLower();
            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
            var apiResult = GetJson(uri);

            if (apiResult.IsSuccess)
            {
                var jsobject = apiResult.Data;
                int? id = Convert.ToInt32(jsobject?["id"]?.ToString());

                if (id != null)
                {
                    uri = $"https://pokeapi.co/api/v2/evolution-chain/{id}";
                    apiResult = GetJson(uri);

                    if (apiResult.IsSuccess)
                    {
                        jsobject = apiResult.Data;
                        return new ServiceResult<Variation>(new Variation(jsobject?["chain"]?["species"]?["name"]?.ToString(), new List<Variation> { }));
                    }
                }
            }
            return new ServiceResult<Variation>();
        }

        private ServiceResult<JObject> GetJson(string uri)
        {
            var result = new ServiceResult<JObject>();
            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                result = new ServiceResult< JObject >(JObject.Parse(json));
            }

            if(client != null)
                client.Dispose();

            return result;
        }
    }
}

