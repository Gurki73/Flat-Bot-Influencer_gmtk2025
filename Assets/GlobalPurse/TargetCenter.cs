using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;

namespace GlobalPurse
{
    public class TargetCenter : ModuleBase, IGlobalPurse
    {
        public static TargetCenter Instance { get; private set; }

        [SerializeField] private CurrencySet currencySet;

        private Dictionary<string, Currency> currencyDefs = new();
        private Dictionary<string, int> currencyAmounts = new();

        public override void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");
            Instance = this;
            LoadData();
        }

        private void LoadData()
        {
            foreach (var currency in currencySet.currencies)
            {
                string id = currency.currencyID;
                currencyDefs[id] = currency;
                int saved = PlayerPrefs.GetInt("GP_" + id, currency.defaultAmount);
                currencyAmounts[id] = saved;
            }
        }

        private void SaveCurrency(string id)
        {
            if (currencyAmounts.ContainsKey(id))
            {
                PlayerPrefs.SetInt("GP_" + id, currencyAmounts[id]);
                PlayerPrefs.Save();
            }
        }

        public int Get(string currency) => currencyAmounts.TryGetValue(currency, out var amt) ? amt : 0;

        public void Set(string currency, int amount)
        {
            currencyAmounts[currency] = amount;
            SaveCurrency(currency);
        }

        public void Add(string currency, int amount)
        {
            if (!currencyAmounts.ContainsKey(currency)) currencyAmounts[currency] = 0;
            currencyAmounts[currency] += amount;
            SaveCurrency(currency);
        }

        public bool Spend(string currency, int amount)
        {
            if (!HasEnough(currency, amount)) return false;
            currencyAmounts[currency] -= amount;
            SaveCurrency(currency);
            return true;
        }

        public bool HasEnough(string currency, int amount) => Get(currency) >= amount;

        public Sprite GetIcon(string currency) =>
            currencyDefs.TryGetValue(currency, out var def) ? def.icon : null;
    }
}

