using System;
using System.Collections.Generic;
using PokeApi.Models;

namespace PokeApi.Tests.Comparers
{
    internal class PokemonAbilityComparer : IEqualityComparer<PokemonAbility>
    {
        public bool Equals(PokemonAbility x, PokemonAbility y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Slot == y.Slot && x.IsHidden == y.IsHidden && x.Info.Name == y.Info.Name && x.Info.Url == y.Info.Url;
        }

        public int GetHashCode(PokemonAbility obj)
        {
            return HashCode.Combine(obj.Slot, obj.IsHidden, obj.Info);
        }
    }
}
