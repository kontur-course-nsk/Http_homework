using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PokeApi.Models;
using System.Linq;

namespace PokeApi.Client
{
    public sealed class PokeClient : IPokeClient
    {
        private readonly HttpClient httpClient;

        public PokeClient()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/")
            };
        }

        public async Task<Pokemon> GetPokemonAsync(string name) =>
            JsonSerializer.Deserialize<Pokemon>(await GetContent($"pokemon/{name}"));

        public async Task<Pokemon> GetPokemonAsync(int id) =>
            JsonSerializer.Deserialize<Pokemon>(await GetContent($"pokemon/{id}"));

        public async Task<PokemonList> GetPokemonsAsync(int? offset = null, int? limit = null)
        {
            var offsetParam = (int)(offset is null ? 1 : offset);
            var limitParam = (int)(limit is null ? int.MaxValue : limit);
            var totalList = JsonSerializer.Deserialize<PokemonList>(
                await GetContent($"pokemon-species/?offset={offsetParam}&limit={limitParam}"));
            totalList.Previous = offsetParam > 1 ?
                new Uri($"{httpClient.BaseAddress}pokemon/{offsetParam - 1}") : null;
            totalList.Next = offsetParam + limitParam < totalList.TotalCount - 1 ?
                new Uri($"{httpClient.BaseAddress}pokemon/{offsetParam + limitParam}") : null;
            totalList.Items = totalList.Items.Skip(offsetParam).Take(limitParam).ToArray();
            return totalList;
        }

        private async Task<string> GetContent(string url)
        {
            var response = await this.httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            throw new Exception("Request insuccessful");
        }
    }
}
