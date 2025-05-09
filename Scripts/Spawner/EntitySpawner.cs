﻿using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public  class EntitySpawner : Spawner
    {
        /// <summary>
        /// Ссылки на то что спавнить.
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        protected override GameObject GeneratedSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}