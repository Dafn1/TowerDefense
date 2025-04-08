using UnityEngine;
using System.Collections.Generic;


namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        
        private List<TowerBuyControl> m_ActiveControl;
        private RectTransform m_RectTransform;


        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildArea.OnClickEvent += MoveToBuildArea;
            gameObject.SetActive(false);


        }
        private void OnDestroy()
        {
            BuildArea.OnClickEvent -= MoveToBuildArea;
        }


        private void MoveToBuildArea(BuildArea buildArea)
        {
            if (buildArea)
            {
                var position = Camera.main.WorldToScreenPoint(buildArea.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                m_ActiveControl = new List<TowerBuyControl>();
                foreach (var asset in buildArea.buldableTowers)
                {

                    if (asset.IsAvailabe())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);

                        newControl.SetTowerAssets(asset);
                    }


                }

                if (m_ActiveControl.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                        m_ActiveControl[i].transform.position += offset;
                    }
                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildArea(buildArea.transform.root);
                    }

                }
            }
            else
            {
                if (m_ActiveControl != null)
                {
                    foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                    m_ActiveControl.Clear();
                }

                gameObject.SetActive(false);
            }
        }

        
    }
}