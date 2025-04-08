using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;
using System;
using UnityEngine.SocialPlatforms.Impl;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resaultImage;

        public bool IsComplited { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }
        public void SetLevelData(Episode episode, int score)
        {

        }

        public int Initialise()
        {
            var score = MapComplition.Instance.GetEpisodeScore(m_Episode);
            resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                resaultImage[i].color = Color.white;
            }
            return score;
        }
    }

}
