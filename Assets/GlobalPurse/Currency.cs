using UnityEngine;
using Capsule.Core;

namespace GlobalPurse
{
    [CreateAssetMenu(menuName = "GlobalPurse/Currency", fileName = "NewCurrency")]
    public class Currency : ScriptableObject
    {
        public string currencyID;
        public Sprite icon;
        public int defaultAmount;
    }
}
