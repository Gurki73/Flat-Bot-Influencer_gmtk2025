using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Beastarium
{
    public class BeastariumDatabase : MonoBehaviour
    {
        public static BeastariumDatabase Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Prevent duplicates
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep between scenes
        }

        public BeastariumTable GetTableForDomain(BeastariumTable table)
        {

            return table;
        }
    }
}

