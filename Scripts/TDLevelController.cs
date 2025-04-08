using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;


        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActive();
                LevelResultController.Instance.Show(false);
            };
            m_ReferenceTime += Time.time;

            void LifeScoreChenge(int _)
            {
                levelScore -= 1;
                TDPlayer.Instance.OnLifeUpdate -= LifeScoreChenge;
            }
            TDPlayer.Instance.OnLifeUpdate += LifeScoreChenge;

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActive();
                if (m_ReferenceTime <= Time.time)
                {
                    levelScore -= 1;
                }
                print(levelScore);
                MapComplition.SaveEpisodeResult(levelScore);
            });



        }
        private void StopLevelActive()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;

                }
            }

            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }
    }

}
