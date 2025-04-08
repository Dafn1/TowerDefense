using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_ContinueButton;
        private void Start()
        {
            m_ContinueButton.interactable = FileHendler.HasFile(MapComplition.fileName);
        }
        public void NewGame()
        {
           FileHendler.Reset(MapComplition.fileName);
           FileHendler.Reset(Upgrades.fileName);
           SceneManager.LoadScene(1);
        } 

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

}
