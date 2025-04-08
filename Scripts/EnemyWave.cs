using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        public static Action<float> OnWavePrepare;
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }
        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }
        [SerializeField] private PathGroup[] groups;

        [SerializeField] private float prepeareTime = 10f;
        public float GetRemainingTime()
        {
            return prepeareTime-Time.time;
        }

        private void Awake()
        {
            enabled = false;
        }

        private event Action OnWaweReady;
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepeareTime);
            prepeareTime += Time.time;
            enabled = true;
            OnWaweReady += spawnEnemies;
        }
        private void Update()
        {
            if (Time.time >= prepeareTime)
            {
                enabled = false;
                OnWaweReady?.Invoke();
            }
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumeratSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }

            }

        }

        [SerializeField] private EnemyWave next;
        internal EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaweReady -= spawnEnemies;
            if (next) next.Prepare(spawnEnemies);
            return next;

        }

    }
}