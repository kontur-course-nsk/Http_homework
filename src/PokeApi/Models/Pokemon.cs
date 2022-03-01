using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public sealed class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
       
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("types")]
        public PokemonType[] Types { get; set; }

        [JsonPropertyName("abilities")]
        public PokemonAbility[] Abilities { get; set; }
    }
}
