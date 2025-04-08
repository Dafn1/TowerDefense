using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class MAinMenuSceneManager : MonoBehaviour
    {
        public void Change()
        {
            SceneManager.LoadScene(0);
        }
        public void Lore()
        {
            SceneManager.LoadScene(1);
        }
        public void LoreBranch()
        {
            SceneManager.LoadScene(5);
        }
        public void LoreBranchSecond()
        {
            SceneManager.LoadScene(9);
        }
        public void LoreFinel()
        {
            SceneManager.LoadScene(12);
        }

    }
}

