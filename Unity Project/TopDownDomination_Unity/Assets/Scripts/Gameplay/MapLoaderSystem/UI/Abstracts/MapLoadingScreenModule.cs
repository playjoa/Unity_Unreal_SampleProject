using Gameplay.MapLoaderSystem.Controller;
using Gameplay.MapLoaderSystem.Data;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.Abstracts
{
    public abstract class MapLoadingScreenModule : MonoBehaviour
    {
        protected MapData MapData { get; private set; }

        public void Initiate(MapData loadingMapData)
        {
            MapData = loadingMapData;
            OnInitiated(MapData);
        }

        public abstract void OnInitiated(MapData mapData);

        public virtual void OnUpdate(MapLoaderController mapLoader)
        {
            
        }
    }
}