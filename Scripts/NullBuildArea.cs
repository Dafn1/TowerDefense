using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NullBuildArea : BuildArea
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}