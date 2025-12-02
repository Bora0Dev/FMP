using UnityEngine;
using UnityEngine.EventSystems;
using MerchantsRoad.Core;

namespace MerchantsRoad.UI.Map
{
    public class TownButton : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TownId Town;
        public MapController Map;

        private RectTransform _rect;

        private void Awake()
        {
            _rect = transform as RectTransform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Map != null)
            {
                Map.ShowTownHover(Town, _rect);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Map != null)
            {
                Map.HideHover();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Map != null)
            {
                Map.RequestTravel(Town);
            }
        }
    }
}