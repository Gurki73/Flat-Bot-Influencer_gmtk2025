using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;

namespace GlobalPurse
{
    [CreateAssetMenu(menuName = "GlobalPurse/Currency Set", fileName = "NewCurrencySet")]
    public class CurrencySet : ScriptableObject
    {
        public List<Currency> currencies;
    }
}

