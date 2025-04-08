using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDProjectile : Projectile
    {
        public enum DamageType { Base, Magic }

        [SerializeField] private DamageType m_DamadeType;
        [SerializeField] private Sound m_ShotSound = Sound.ProjArrow;
        [SerializeField] private Sound m_HitSound = Sound.ISEnemyDamageArrow;

        private void Start()
        {
            m_ShotSound.Play();

        }

        protected override void OnHit(RaycastHit2D hit)
        {
            m_HitSound.Play();
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamadeType);

            }
        }
    }

}
