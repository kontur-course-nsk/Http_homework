using System;
using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public sealed class PokemonList
    {
        [JsonPropertyName("count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("next")]
        public Uri Next { get; set; }

        [JsonPropertyName("previous")]
        public Uri Previous { get; set; }

        [JsonPropertyName("results")]
        public PokemonListElement[] Items { get; set; }
    }
}
