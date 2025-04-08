using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class EnemyAsset : ScriptableObject
    {
        [Header("Look")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animations;
        public bool flipX = false;

        [Header("Game parameters")]
        public float moveSpeed = 1f;
        public int hp = 1;
        public int armor = 0;
        public Enemy.ArmorType armorType;
        public int score = 1;
        public float radius = 0.21f;
        public int damage = 1;
        public int gold = 1;
        public int mana = 1;
        
    }
}