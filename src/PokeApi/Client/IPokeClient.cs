using System.Threading.Tasks;
using PokeApi.Models;

namespace PokeApi.Client
{
    public interface IPokeClient
    {
        public Task<Pokemon> GetPokemonAsync(string name);

        public Task<Pokemon> GetPokemonAsync(int id);

        public Task<PokemonList> GetPokemonsAsync(int? offset = null, int? limit = null);
    }
}
