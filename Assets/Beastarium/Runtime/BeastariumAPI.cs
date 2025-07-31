using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Capsule.Core;

namespace Beastarium
{
    public static class BeastariumAPI
    {
        public static float GetModifier(Enemy enemy, Attack attack, BeastariumTable table)
        {
            // var table = BeastariumDatabase.Instance.GetTableForDomain(table);
            // return table.entries.FirstOrDefault(e => e.enemy == enemy && e.attack == attack)?.modifier ?? 0f;
            return 0.5f;
        }
    }
}
