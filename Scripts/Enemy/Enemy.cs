using SpaceShooter;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;


namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Magic = 1 }
        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
                (int power, TDProjectile.DamageType type, int armor) =>
                {//base armor
                    switch (type)
                    {
                        case TDProjectile.DamageType.Magic:return power;
                        default: return Mathf.Max(power-armor, 1);

                    }

                },
                (int power, TDProjectile.DamageType type, int armor) =>
                {//magic armor
                    if (TDProjectile.DamageType.Base==type)
                    {
                        armor=armor/2;
                    }
                     return Mathf.Max(power-armor, 1);
                }
        };

        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_gold = 1;
        [SerializeField] private int m_Mana = 1;
        [SerializeField] private int m_armor = 1;
        [SerializeField] private ArmorType m_ArmorTupe;


        private Destructible m_destructible;

        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("View").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);
            GetComponentInChildren<StandUp>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_damage = asset.damage;
            m_armor = asset.armor;
            m_ArmorTupe = asset.armorType;
            m_gold = asset.gold;
            m_Mana = asset.mana;

        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }
        public void GivePlayerMana()
        {
            TDPlayer.Instance.ChangeMana(m_Mana);
        }
        public void TakeDamage(int damage, TDProjectile.DamageType m_DamageType)
        {
            m_destructible.ApplyDamage(ArmorDamageFunctions[(int)m_ArmorTupe](damage, m_DamageType, m_armor));
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
#endif

}