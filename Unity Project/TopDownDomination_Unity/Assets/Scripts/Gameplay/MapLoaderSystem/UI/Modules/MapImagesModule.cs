using Gameplay.MapLoaderSystem.Abstracts;
using Gameplay.MapLoaderSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.MapLoaderSystem.UI.Modules
{
    public class MapImagesModule : MapLoadingScreenModule
    {
        [Header("Images")]
        [SerializeField] private Image mapIconImage;
        [SerializeField] private Image mapSplashImage;

        public override void OnInitiated(MapData mapData)
        {
            if (mapIconImage) mapIconImage.sprite = mapData.MapIcon;
            if (mapSplashImage) mapSplashImage.sprite = mapData.MapSplashScreen;
        }
    }
}