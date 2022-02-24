using System;
using System.Collections.Generic;
using PokeApi.Models;

namespace PokeApi.Tests.Comparers
{
    public class PokemonTypeComparer : IEqualityComparer<PokemonType>
    {
        public bool Equals(PokemonType x, PokemonType y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Slot == y.Slot && x.Info.Name == y.Info.Name && x.Info.Url == y.Info.Url;
        }

        public int GetHashCode(PokemonType obj)
        {
            return HashCode.Combine(obj.Slot, obj.Info);
        }
    }
}
