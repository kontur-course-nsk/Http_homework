using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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
            return JsonSerializer.Deserialize<Pokemon>(await GetContentAsync($"pokemon/{name}"));
        }

        public async Task<Pokemon> GetPokemonAsync(int id)
        {
            return JsonSerializer.Deserialize<Pokemon>(await GetContentAsync($"pokemon/{id}"));
        }

        public async Task<PokemonList> GetPokemonsAsync(int? offset = null, int? limit = null)
        {
            return JsonSerializer.Deserialize<PokemonList>(
                await GetContentAsync($"pokemon?offset={offset}&limit={limit}"));
        }

        private async Task<string> GetContentAsync(string requestUri)
        {
            var response = await this.httpClient.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}