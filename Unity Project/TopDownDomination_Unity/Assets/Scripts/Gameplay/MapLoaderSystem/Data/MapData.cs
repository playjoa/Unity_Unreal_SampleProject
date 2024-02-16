using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.MapLoaderSystem.Data
{
    [CreateAssetMenu(menuName = "GameSystems/Data/MapData", fileName = "New MapData")]
    public class MapData : ScriptableObjectWithId
    {
        [Header("Texts")]
        [SerializeField] private string mapName = "Desert";
        [SerializeField] private string mapDescription = "A giant wasteland!";

        [Header("Images")]
        [SerializeField] private Sprite mapIcon;
        [SerializeField] private Sprite mapSplashScreen;
        
        [Header("Scene Configuration")]
        [SerializeField] private string mapSceneName = "Desert";
        
        public string MapName => mapName;
        public string MapDescription => mapDescription;
        
        public Sprite MapIcon => mapIcon;
        public Sprite MapSplashScreen => mapSplashScreen;
        
        public string MapSceneName => mapSceneName;
    }
}