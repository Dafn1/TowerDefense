using SpaceShooter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : MonoSingleton<Upgrades>
    {
        public const string fileName = "upgrades.dat";
        
      


        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }
        [SerializeField] private UpgradeSave[] save;
        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(fileName, ref save);

        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level += 1;

                    Saver<UpgradeSave[]>.Save(fileName, Instance.save);
                }

            }
        }

        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }
            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
            
        }
    }
}

