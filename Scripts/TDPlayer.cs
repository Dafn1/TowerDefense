using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        {
            get { return Player.Instance as TDPlayer; }
        }
        public event Action<int> OnManaUpdate;
        public void ManaUpdateSubscribe(System.Action<int> act)
        {

            OnManaUpdate += act;
            act(Instance.m_Mana);
        }

        public event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(System.Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }

        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(System.Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        [SerializeField] private int m_gold;
        [SerializeField] private int m_Mana;

        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }
        public void ChangeMana(int change)
        {
            m_Mana += change;
            OnManaUpdate(m_Mana);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private Tower m_towerPrefab;

        //TODO: ob es genug Gold gibt
        public void TryBuild(TowerAsset towerAsset, Transform buildArea)
        {

            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildArea.position, Quaternion.identity);
            tower.Use(towerAsset);
            tower.GetComponentInChildren<Turret>().m_TurretProperties = towerAsset.m_TurretProperties;
            Destroy(buildArea.gameObject);
        }
        public void GoldUpateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;

        }
        public void ManaUpateUnsubscribe(Action<int> act)
        {
            OnManaUpdate -= act;
        }



        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private UpgradeAsset GolgUp;


        private void Start()
        {

            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            level = Upgrades.GetUpgradeLevel(GolgUp);
            TakeDamage(-level * 5);
            ChangeGold(level * 20);

        }

    }
}