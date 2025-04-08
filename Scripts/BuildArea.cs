using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class BuildArea : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] buldableTowers;
        public void SetBuldableTowers(TowerAsset[] towers)
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
               
            }
            else
            {
                buldableTowers = towers;
            }
        }
        public static event Action<BuildArea> OnClickEvent;
        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}