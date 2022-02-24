using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public sealed class PokemonAbility
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
        
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("ability")]
        public PokemonAbilityInfo Info { get; set; }
    }
}
