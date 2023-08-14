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
            var jobject = GetJson(uri);

            if (jobject["id"] != null)
            {
                int? id = Convert.ToInt32(jobject["id"]?.ToString());

                if (id != null)
                {
                    uri = $"https://pokeapi.co/api/v2/evolution-chain/{id}";
                    jobject = GetJson(uri);

                    if (jobject["chain"]?["species"]?["name"] != null)
                    {
                        return new ServiceResult<Variation>(new Variation(jobject["chain"]?["species"]?["name"]?.ToString(), new List<Variation> { }));
                    }
                }
            }
            return new ServiceResult<Variation>();
        }

        private JObject GetJson(string uri)
        {
            var result = new JObject();
            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                result = JObject.Parse(json);
            }

            if(client != null)
                client.Dispose();

            return result;
        }
    }
}

