using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace GlobalPurse
{
    [System.Serializable]
    public class CurrencyData
    {
        public int amount;
        public Sprite icon;

        public CurrencyData(int initial = 0, Sprite icon = null)
        {
            this.amount = initial;
            this.icon = icon;
        }
    }
}