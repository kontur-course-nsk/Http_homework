﻿using System;
using System.Net.Http;
using System.Text;
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
            var response = await this.httpClient.GetAsync($"pokemon/{name}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pokemon>(content);
        }

        public async Task<Pokemon> GetPokemonAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"pokemon/{id}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pokemon>(content);
        }

        public async Task<PokemonList> GetPokemonsAsync(int? offset = null, int? limit = null)
        {
            var response = await this.httpClient.GetAsync(GetRequest(offset, limit));

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PokemonList>(content);
        }

        private static string GetRequest(int? offset = null, int? limit = null)
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.Append("pokemon");

            var offsetParam = offset == null ? "" : $"offset={offset}";
            var limitParam = limit == null ? "" : $"limit={limit}";
                        
            if (offset != null || limit != null)
                requestBuilder.Append("?");

            requestBuilder.Append(offsetParam);

            if (offset != null && limit != null)
                requestBuilder.Append("&");

            requestBuilder.Append(limitParam);

            return requestBuilder.ToString();
        }
    }
}
