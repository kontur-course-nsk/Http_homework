using System;
using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public sealed class PokemonAbilityInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }
}
