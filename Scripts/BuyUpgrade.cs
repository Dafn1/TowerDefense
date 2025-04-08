using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image UpgradeIcon;
        private int costNumber = 0;
        [SerializeField] private Text Level, CostText;
        [SerializeField] private Button buyButton;
        public void Initialize()
        {
            UpgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            if (savedLevel >= asset.costByLevel.Length)
            {
                Level.text = $"Level: {savedLevel} (Max)";
               
                buyButton.interactable = false;
                buyButton.transform.Find("Star (1)").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                CostText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                Level.text = $"Level: {savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                CostText.text = costNumber.ToString();


            }



        }
        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumber;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

       
    }
}

