using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{

    [RequireComponent(typeof(MapLevel))]
    public class BranchLevels : MonoBehaviour
    {

        [SerializeField] private MapLevel rootLevel;
        [SerializeField] private Text pointText;
        [SerializeField] private int needPoints = 3;

        public void TryActive()
        {
            gameObject.SetActive(rootLevel.IsComplited);
            if (needPoints > MapComplition.Instance.TotalScore)
            {
                pointText.text = needPoints.ToString();

            }
            else
            {
                pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }

        }
    }

}
