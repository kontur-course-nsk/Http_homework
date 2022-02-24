using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PokeApi.Client;
using PokeApi.Models;
using PokeApi.Tests.Comparers;

namespace PokeApi.Tests
{
    [TestFixture]
    internal sealed class Tests
    {
        private IPokeClient pokeClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.pokeClient = new PokeClient();
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.NameDictionary))]
        public async Task GetPokemonByNameTest(KeyValuePair<string, Pokemon> expectedPokemon)
        {
            var actualPokemon = await this.pokeClient.GetPokemonAsync(expectedPokemon.Key);

            AssertPokemon(actualPokemon, expectedPokemon.Value);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.IdDictionary))]
        public async Task GetPokemonByIdTest(KeyValuePair<int, Pokemon> expectedPokemon)
        {
            var actualPokemon = await this.pokeClient.GetPokemonAsync(expectedPokemon.Key);

            AssertPokemon(actualPokemon, expectedPokemon.Value);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.OffsetsLimits))]
        public async Task GetPokemonsTest(PaginationParameters pagination)
        {
            var pokemons = await this.pokeClient.GetPokemonsAsync(pagination.Offset, pagination.Limit);
            pokemons.TotalCount.Should().Be(TestData.TotalCount);

            var offset = GetOffset(pagination.Offset);
            var count = GetLimit(pagination.Limit);

            pokemons.Items.Length.Should().Be(count);

            if (offset == 0)
            {
                pokemons.Previous.Should().BeNull();
            }
            else
            {
                pokemons.Previous.Should().NotBeNull();
            }

            if (count + offset >= TestData.TotalCount)
            {
                pokemons.Next.Should().BeNull();
            }
            else
            {
                pokemons.Next.Should().NotBeNull();
            }
        }

        private static void AssertPokemon(Pokemon actual, Pokemon expected)
        {
            actual.Height.Should().BePositive();

            actual.Should().BeEquivalentTo(
                expected,
                config => config
                    .Using(new PokemonComparer()));
        }

        private static int GetOffset(int? offset)
        {
            return offset > 0 ? offset.Value : 0;
        }
        private static int GetLimit(int? limit)
        {
            return limit > 0 ? limit.Value : TestData.DefaultLimit;
        }
    }
}
