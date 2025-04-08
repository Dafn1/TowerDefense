using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public TowerAsset[] m_UpgrateTO;
        public int goldCost = 15;
        public Sprite GUISprite;
        public Sprite sprite;
        public TurretProperties m_TurretProperties;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;
        public bool IsAvailabe() => !requiredUpgrade || requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);

    }
}