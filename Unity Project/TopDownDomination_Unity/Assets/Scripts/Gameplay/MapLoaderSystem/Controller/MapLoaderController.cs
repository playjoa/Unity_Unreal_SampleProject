using System;
using System.Collections;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.MapLoaderSystem.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Extensions;

namespace Gameplay.MapLoaderSystem.Controller
{
    public class MapLoaderController : MonoBehaviour, IGameplaySystem
    {
        public MapData CurrentLoadingMap { get; private set; }
        public string ProgressPercentage { get; private set; }
        public float Progress { get; private set; }

        public event Action<MapLoaderController> OnStartedLoadingScreen;
        public event Action<MapLoaderController> OnFinishedLoadingScreen;

        public IEnumerator Initiate(GameController gameSystemsController)
        {
            CurrentLoadingMap = gameSystemsController.GameData.MapData;

            Debug.Log($"----Initializing MapLoader: {CurrentLoadingMap.MapSceneName}----");

            var sceneOperation = SceneManager.LoadSceneAsync(CurrentLoadingMap.MapSceneName, LoadSceneMode.Additive);
            OnStartedLoadingScreen?.Invoke(this);
            while (!sceneOperation.isDone)
            {
                var progress = ProgressClamped(sceneOperation.progress);

                ProgressPercentage = progress.FloatToPercentage();
                Progress = progress;
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(CurrentLoadingMap.MapSceneName));
            OnFinishedLoadingScreen?.Invoke(this);

            Debug.Log($"----Done Initializing MapLoader----");
        }
        
        public void OnCleanUp()
        {
        }

        private static float ProgressClamped(float progress) => Mathf.Clamp01(progress / .9f);
    }
}