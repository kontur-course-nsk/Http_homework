using System;
using System.Threading.Tasks;
using PokeApi.Client;

namespace PokeApi
{
    public sealed class Program
    {
        public static async Task Main()
        {
            var client = new PokeClient();

            var pokemon = await client.GetPokemonAsync("magnemite");
        }
    }
}
