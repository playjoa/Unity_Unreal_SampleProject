using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameWideSystems.GameInitializationSystem.Editor
{
    public static class EditorStartUp
    {
        private const string STARTUP_SCENE_NAME = "0_StartUp";
        private const int STARTUP_SCENE_INDEX = 0;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnStartUpEditor()
        {
            if (!Application.isEditor) return;
            
            var currentScene = SceneManager.GetActiveScene();
            if (currentScene.buildIndex == STARTUP_SCENE_INDEX) return;
            
            Debug.Log("Trying to play game with scene: " + currentScene.name + ". Loading StartUpScene!");
            SceneManager.LoadScene(STARTUP_SCENE_INDEX);
        }
    }
}