using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public abstract class Spawner : MonoBehaviour
    {
        protected abstract GameObject GeneratedSpawnedEntity();


        /// <summary>
        /// Зона спавна.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Режим спавна.
        /// </summary>
        public enum SpawnMode
        {
            /// <summary>
            /// На методе Start()
            /// </summary>
            Start,

            /// <summary>
            /// Периодически используя время m_RespawnTime
            /// </summary>
            Loop,
        }

        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// Кол-во объектов которые будут разом заспавнены.
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// Время респавна. Здесь важно заметить что респавн таймер должен быть больше времени жизни объектов.
        /// </summary>
        [SerializeField] private float m_RespawnTime;
        [SerializeField] private float m_firstSpawnTime;

        private float m_Timer;
        private bool m_Running = true;

        private void Start()
        {
            m_Timer = m_firstSpawnTime;
        }
        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if(m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }

            if (m_SpawnMode == SpawnMode.Start)
            {
                if (m_Timer <= 0 && m_Running == true)
                {
                    SpawnEntities();
                    m_Running = false;
                }
            }
        }

        private void SpawnEntities()
        {
            for(int i = 0; i < m_NumSpawns; i++)
            {
                var e = GeneratedSpawnedEntity();
                e.transform.position = m_Area.RandomInsideZone;
            }
        }
    }
}