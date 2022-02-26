using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using PokeApi.Models;

namespace PokeApi.Client
{
    public sealed class PokeClient : IPokeClient
    {
        private readonly HttpClient httpClient;

        public PokeClient()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/")
            };
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            var content = await GetContentAsync($"pokemon/{name}");
            return JsonSerializer.Deserialize<Pokemon>(content);
        }

        public async Task<Pokemon> GetPokemonAsync(int id)
        {
            var content = await GetContentAsync($"pokemon/{id}");
            return JsonSerializer.Deserialize<Pokemon>(content);
        }

        public async Task<PokemonList> GetPokemonsAsync(int? offset = null, int? limit = null)
        {
            var content = await GetContentAsync($"pokemon?limit={limit}&offset={offset}");
            return JsonSerializer.Deserialize<PokemonList>(content);
        }

        public async Task<string> GetContentAsync(string request)
        {
            var response = await this.httpClient.GetAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}