using System;
using System.Collections;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;


namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damade = 2;
            [SerializeField] private Color m_TargetColor;

            public void Use()
            {
                TDPlayer.Instance.ChangeMana(-m_Cost);
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damade, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }
        }
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            public void Use()
            {

                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinerVelocity();
                }



                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinerVelocity();
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinerVelocity();
                    }
                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }
                EnemyWavesManager.OnEnemySpawn += Slow;

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButon.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_TimeButon.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());


            }
        }
        [SerializeField] private Image m_TargetCircle;
        [SerializeField] private Button m_TimeButon;
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();



    }
}

