using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PokeApi.Models;

namespace PokeApi.Tests
{
    internal static class TestData
    {
        public static readonly Dictionary<string, Pokemon> NameDictionary;
        public static readonly Dictionary<int, Pokemon> IdDictionary;

        public static readonly List<PaginationParameters> OffsetsLimits = new List<PaginationParameters>
        {
            new PaginationParameters(0, 1),
            new PaginationParameters(0, 10),
            new PaginationParameters(20, 40),
            new PaginationParameters(20, 0),
            new PaginationParameters(1125, 1),
            new PaginationParameters(null, null)
        };

        public const int TotalCount = 1126;
        public const int DefaultLimit = 20;

        static TestData()
        {
            var directory = new DirectoryInfo("TestData");
            var files = directory.GetFiles();

            var pokemons = new List<Pokemon>(files.Length);

            foreach (var file in files)
            {
                using var streamReader = new StreamReader(file.OpenRead());
                var content = streamReader.ReadToEnd();

                var pokemon = JsonSerializer.Deserialize<Pokemon>(content);
                pokemons.Add(pokemon);
            }

            NameDictionary = pokemons.ToDictionary(x => x.Name);
            IdDictionary = pokemons.ToDictionary(x => x.Id);
        }
    }

    internal sealed class PaginationParameters
    {
        public int? Offset { get; }

        public int? Limit { get; }

        public PaginationParameters(int? offset, int? limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
    }
}
