using SpaceShooter;
using TowerDefense;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Скрипт прожектайла. Кидается на топ префаба прожектайла.
    /// </summary>
    public class Projectile : Entity
    {
        public void SetFromOtherProjectile(Projectile other)
        {
            other.GetData(out m_Velocity, out m_Lifetime, out m_Damage, out m_ImpactEffectPrefab);
        }

        private void GetData(out float m_Velocity, out float m_Lifetime, out int m_Damage, out ImpactEffect m_ImpactEffectPrefab)
        {
            m_Velocity = this.m_Velocity;
            m_Lifetime = this.m_Lifetime;
            m_Damage = this.m_Damage;
            m_ImpactEffectPrefab = this.m_ImpactEffectPrefab;
        }

        /// <summary>
        /// Линейная скорость полета снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_Lifetime;

        /// <summary>
        /// Повреждения наносимые снарядом.
        /// </summary>
        [SerializeField] protected int m_Damage;

        /// <summary>
        /// Эффект попадания от что то твердое. 
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        [Space(10)]
        [Header("Explosion Settings")]
        [Tooltip("Enables or disables the explosion upon impact.")]
        [SerializeField] private bool m_ExplosionEnabled = false;

        [Tooltip("The radius of the explosion.")]
        [SerializeField] private float m_ExplosionRadius;

        [Tooltip("The damage dealt by the explosion.")]
        [SerializeField] private int m_ExplosionDamage;

        private float m_Timer;

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            // не забыть выключить в свойствах проекта, вкладка Physics2D иначе не заработает
            // disable queries hit triggers
            // disable queries start in collider
            if (hit)
            {
                OnHit(hit);
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {
            var destructible = hit.collider.transform.root.GetComponent<Destructible>();

            if (destructible != null && destructible != m_Parent)
            {
                destructible.ApplyDamage(m_Damage);
                ProjectileExplosion(hit.collider, hit.point);


                if (Player.Instance != null && destructible.HitPoints <= 0)
                {

                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(destructible.ScoreValue);
                    }
                }
            }
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            if (m_ImpactEffectPrefab != null)
            {
                var impact = Instantiate(m_ImpactEffectPrefab.gameObject);
                impact.transform.position = pos;
            }

            Destroy(gameObject);
        }

        private void ProjectileExplosion(Collider2D col, Vector2 pos)
        {
            if (m_ExplosionEnabled)
            {
                OnProjectileLifeEnd(col, pos);

                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);
                foreach (Collider2D hitCollider in hitColliders)
                {
                    Destructible destructible = hitCollider.transform.parent?.GetComponent<Destructible>();
                    if (destructible != null && destructible != m_Parent)
                    {
                        destructible.ApplyDamage(m_ExplosionDamage);
                    }
                }
            }
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_ExplosionRadius);
        }
#endif
    }


}
#if UNITY_EDITOR
namespace TowerDefense
{
    [CustomEditor(typeof(Projectile))]
    public class ProjectileInInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create TD Projectile"))
            {
                var target = this.target as SpaceShooter.Projectile;
                var TDProj = target.gameObject.AddComponent<TDProjectile>();
                TDProj.SetFromOtherProjectile(target);

            }
        }

    }
}
#endif


