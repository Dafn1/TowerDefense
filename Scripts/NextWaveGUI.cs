using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{

    public class NextWaveGUI : MonoBehaviour
    {

        [SerializeField] private Text bonusAmmount;
        private EnemyWavesManager manager;
        private float timeToNextWave;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWavesManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            bonusAmmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }
    }
}

