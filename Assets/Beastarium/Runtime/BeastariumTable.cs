using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;

namespace Beastarium
{
    [System.Serializable]
    public class BeastariumRow
    {
        public List<float> modifiers; // row = enemy, cols = attack
    }


    [CreateAssetMenu(menuName = "Beastarium/Table")]
    public class BeastariumTable : ScriptableObject
    {
        public string tableName;
        public List<Enemy> enemies;
        public List<Attack> attacks;

        public List<BeastariumRow> matrix = new(); // rows = enemies, cols = attacks

        [TextArea(2, 5)]
        public string explanation;


        public float GetModifier(Enemy enemy, Attack attack)
        {
            int row = enemies.IndexOf(enemy);
            int col = attacks.IndexOf(attack);

            if (row < 0 || col < 0 || row >= matrix.Count || col >= matrix[row].modifiers.Count)
                return 0f; // fallback = neutral

            return matrix[row].modifiers[col];
        }
    }

}


