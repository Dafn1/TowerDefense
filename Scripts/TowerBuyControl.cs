using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_towerAsset;
        
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;
        [SerializeField] private Transform buildArea;

        public void SetTowerAssets(TowerAsset asset)
        {
            m_towerAsset = asset;
        }
        public void SetBuildArea(Transform value)
        {
            buildArea = value;
        }

        private void Start()
        {

            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
            m_text.text = m_towerAsset.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_towerAsset.GUISprite;
        }
        
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_towerAsset.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            
            TDPlayer.Instance.TryBuild(m_towerAsset, buildArea);
            BuildArea.HideControls();
        }

        private void OnDestroy()
        {
            TDPlayer.Instance.GoldUpateUnsubscribe(GoldStatusCheck);
            
        }
    }
}