using UnityEngine;
using UnityEngine.UI;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.World
{
    public class TownBackgroundController : MonoBehaviour
    {
        public Image BackgroundImage;

        public Sprite OakfieldSprite;
        public Sprite IronforgeSprite;
        public Sprite RivermereSprite;

        public void UpdateTownBackground(TownId town)
        {
            if (BackgroundImage == null)
            {
                Debug.LogWarning("BackgroundImage not assigned!");
                return;
            }

            Sprite spriteToUse = null;

            switch (town)
            {
                case TownId.Oakfield:
                    spriteToUse = OakfieldSprite;
                    break;

                case TownId.Ironforge:
                    spriteToUse = IronforgeSprite;
                    break;

                case TownId.Rivermere:
                    spriteToUse = RivermereSprite;
                    break;
            }

            BackgroundImage.sprite = spriteToUse;
        }

        private void Start()
        {
            if (GameManager.instance != null)
                UpdateTownBackground(GameManager.instance.currentTown);
        }
    }
}