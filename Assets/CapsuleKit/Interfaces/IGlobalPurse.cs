using UnityEngine;

namespace Capsule.Core
{
    public interface IGlobalPurse
    {
        int Get(string currency);
        void Set(string currency, int amount);
        void Add(string currency, int amount);
        bool Spend(string currency, int amount);
        bool HasEnough(string currency, int amount);
        Sprite GetIcon(string currency);
    }
}
