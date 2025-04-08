using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        

        [SerializeField] private int Money;
        [SerializeField] private Text MoneyText;
        [SerializeField] private BuyUpgrade[] sales;
        private void Start()
        {
            
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();

        }

        public void UpdateMoney()
        {
            Money = MapComplition.Instance.TotalScore;
            Money -= Upgrades.GetTotalCost();
            MoneyText.text = Money.ToString();

            foreach (var slot in sales)
            {
                slot.CheckCost(Money);
            }
        }


    }

}
