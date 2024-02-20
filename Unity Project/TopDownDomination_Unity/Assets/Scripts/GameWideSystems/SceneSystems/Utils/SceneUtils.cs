using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameWideSystems.SceneSystems.Utils
{
    public enum GameScene
    {
        ReloadCurrent,
        StartUp,
        Main,
        Gameplay
    }
    
    public static class SceneUtils
    {
        public static GameScene CurrentGameScene { get; private set; } = GameScene.StartUp;
        
        private static readonly Dictionary<GameScene, string> GameScenes = new()
        {
            { GameScene.StartUp, APPSTARTUP_SCENE },
            { GameScene.Main, MAIN_SCENE },
            { GameScene.Gameplay, GAMEPLAY_SCENE }
        };
        
        public static void LoadScene(GameScene targetGameScene)
        {
            var targetSceneToLoad = targetGameScene.Equals(GameScene.ReloadCurrent) ? CurrentGameScene : targetGameScene;
            var sceneId = GetSceneId(targetSceneToLoad);

            if (sceneId == string.Empty)
            {
                Debug.LogError($"Scene Id: {sceneId} and/or GameScene {targetSceneToLoad} is not valid!");
                return;
            }
            
            LoadScene(sceneId, targetGameScene);
        }
        
        private static void LoadScene(string targetScene, GameScene gameScene)
        {
            SceneManager.LoadScene(targetScene);
            CurrentGameScene = gameScene;
        }

        public static string GetSceneId(GameScene gameScene)
        {
            return GameScenes.TryGetValue(gameScene, out var sceneIdFound) ? sceneIdFound : string.Empty;
        }

        private const string APPSTARTUP_SCENE = "0_StartUp";
        private const string MAIN_SCENE = "1_Main";
        private const string GAMEPLAY_SCENE = "2_Gameplay";
    }
}