using System;
using System.Collections.Generic;
using FluentAssertions;
using PokeApi.Models;

namespace PokeApi.Tests.Comparers
{
    internal class PokemonComparer : IEqualityComparer<Pokemon>
    {
        private readonly IEqualityComparer<PokemonAbility> abilityComparer = new PokemonAbilityComparer();
        private readonly IEqualityComparer<PokemonType> typeComparer = new PokemonTypeComparer();

        public bool Equals(Pokemon x, Pokemon y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name && x.Height == y.Height && Equals(x.Types, y.Types) && Equals(x.Abilities, y.Abilities);
        }

        public int GetHashCode(Pokemon obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Height, obj.Types, obj.Abilities);
        }

        private bool Equals(PokemonAbility[] x, PokemonAbility[] y)
        {
            try
            {
                x.Should().BeEquivalentTo(y, config => config.Using(abilityComparer));
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool Equals(PokemonType[] x, PokemonType[] y)
        {
            try
            {
                x.Should().BeEquivalentTo(y, config => config.Using(typeComparer));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
