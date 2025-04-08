using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset RadiusUp;

        [SerializeField] private float m_Radius;
        [SerializeField] private Turret[] turrets;
        private Destructible target = null;

        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;


            turrets = GetComponentsInChildren<Turret>();
            int levelUpgrade = Upgrades.GetUpgradeLevel(RadiusUp);
            if (levelUpgrade > 0)
            {
                m_Radius += m_Radius / 2 * levelUpgrade;
            }

            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.m_TurretProperties);
            }

            GetComponentInChildren<BuildArea>().SetBuldableTowers(asset.m_UpgrateTO);
        }

        private void Start()
        {

        }
        
        private void Update()
        {

            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        Rigidbody2D enemyRb = target.GetComponent<Rigidbody2D>();
                        if (enemyRb != null)
                        {
                            Vector2 enemyPos = target.transform.position;
                            Vector2 enemyVel = enemyRb.velocity;

                            float distanceToEnemy = Vector2.Distance(transform.position, enemyPos);

                           
                            float projectileSpeed = turret.m_TurretProperties.ProjectilePrefab.Velocity;

                            
                            float timeToTarget = distanceToEnemy / projectileSpeed;

                           
                            Vector2 futurePos = enemyPos + enemyVel * timeToTarget;

                            
                            Vector2 dir = futurePos - (Vector2)turret.transform.position;
                            turret.transform.up = dir;

                           
                            turret.Fire();
                        }

                    }
                }

                else
                {
                    target = null;
                }
            }

            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}