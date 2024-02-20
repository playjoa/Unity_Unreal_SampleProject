using GameWideSystems.SceneSystems.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Components
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Header("Target Scene")] 
        [SerializeField] private GameScene targetScene = GameScene.Gameplay;

        [Header("Button")]
        [SerializeField] private Button loadSceneButton;

        private void Awake()
        {
            loadSceneButton.onClick.AddListener(OnLoadSceneClickHandler);
        }

        private void OnDestroy()
        {
            loadSceneButton.onClick.RemoveListener(OnLoadSceneClickHandler);
        }

        private void OnLoadSceneClickHandler()
        {
            SceneUtils.LoadScene(targetScene);
        }
    }
}