using Pokedex.Models;
using Newtonsoft.Json.Linq;

namespace Pokedex.Services
{
	public class PokedexService: IPokedexService
    {
		public PokedexService()
		{
		}

        /// <summary>
        /// This is a method to get evolution chain by pokemon name
        /// </summary>
        /// <param name="pokemonName">The pokemon name</param>
        /// <returns>ServiceResult<Variation></returns>
		public ServiceResult<Variation> GetEvolutionChain(string pokemonName)
		{
            ServiceResult<Variation> result = new ServiceResult<Variation>();
            var name = pokemonName.ToLower();
            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
            var apiResult = GetJson(uri);

            if (apiResult.IsSuccess)
            {
                int? id = Convert.ToInt32(apiResult.Data?["id"]?.ToString());

                if (id != null)
                {
                    uri = $"https://pokeapi.co/api/v2/evolution-chain/{id}";
                    apiResult = GetJson(uri);

                    if (apiResult.IsSuccess)
                    {
                        if (apiResult.Data?["chain"] != null)
                        {
                            var chain = GetChainLink(apiResult.Data?["chain"], null);
                            if (chain.IsSuccess)
                            {
                                result = new ServiceResult<Variation>(chain.Data);
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// This is a recursive method to get parent and child node of evolves_to
        /// </summary>
        /// <param name="token">The JToken to be conveted</param>
        /// <param name="variation">The variation to be updated</param>
        /// <returns>ServiceResult<Variation></returns>
        private ServiceResult<Variation> GetChainLink(JToken token, Variation variation)
        {
            ServiceResult<Variation> result = new ServiceResult<Variation>();

            if (token != null && token["evolves_to"] != null)
            {
                JArray array = JArray.FromObject(token["evolves_to"]);
                if (array != null)
                {
                    foreach (JToken a in array)
                    {
                        var v = GetVariation(a);
                        if (v.IsSuccess)
                        {
                            if (variation == null)
                            {
                                variation = v.Data;
                            }
                            else
                            {
                                variation.Variations.Add(v.Data);
                            }
                            result = new ServiceResult<Variation>(variation);
                            GetChainLink(a, result.Data);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This is a method to convert JToken to Variation
        /// </summary>
        /// <param name="token">The JToken to be conveted</param>
        /// <returns>ServiceResult<Variation></returns>
        private ServiceResult<Variation> GetVariation(JToken token)
        {
            ServiceResult<Variation> result = new ServiceResult<Variation>();

            var speciesName = token?["species"]?["name"];
            if (speciesName != null)
            {
                result = new ServiceResult<Variation>(new Variation(speciesName.ToString(), new List<Variation> { }));
            }

            return result;
        }

        /// <summary>
        /// This is a method to fetch uri and return JObject
        /// </summary>
        /// <param name="uri">The JToken to be conveted</param>
        /// <returns>ServiceResult<JObject></returns>
        private ServiceResult<JObject> GetJson(string uri)
        {
            ServiceResult<JObject> result = new ServiceResult<JObject>();

            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                result = new ServiceResult<JObject>(JObject.Parse(json));
            }

            if (client != null)
                client.Dispose();

            return result;
        }

    }
}

