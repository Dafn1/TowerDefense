using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWavesManager : MonoBehaviour
    {
        public static Action<Enemy> OnEnemySpawn;
        [SerializeField] private Enemy m_EnemyPrefab;

        [SerializeField] private Path[] m_Path;
        [SerializeField] private EnemyWave currentWawe;
        public event Action OnAllWavesDead;
        private int activeEnemyCount = 0;

        private void RecordEnemyDeadh()
        {
            if (--activeEnemyCount == 0)
            {
                ForceNextWave();
            }
        }

        private void Start()
        {
            currentWawe.Prepare(SpawnEnemies);
        }
        private void SpawnEnemies()
        {
            
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWawe.EnumeratSquads())
            {
                if (pathIndex < m_Path.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab, m_Path[pathIndex].StartAria.RandomInsideZone, Quaternion.identity);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_Path[pathIndex]);
                        activeEnemyCount += 1;
                        e.OnEnd += RecordEnemyDeadh;
                        OnEnemySpawn?.Invoke(e);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invdlid pathIndex in{name}");
                }
            }
            currentWawe = currentWawe.PrepareNext(SpawnEnemies);

        }

        public void ForceNextWave()
        {
            if (currentWawe)
            {
                TDPlayer.Instance.ChangeGold((int)currentWawe.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (activeEnemyCount == 0)
                {
                    OnAllWavesDead?.Invoke();
                }
            }
        }
    }
}

