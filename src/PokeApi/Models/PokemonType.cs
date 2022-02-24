using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public sealed class PokemonType
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public PokemonTypeInfo Info { get; set; } 
    }
}
