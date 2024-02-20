using Gameplay.Entity.Base.Data;
using GameWideSystems.GameDataSystem.Controller;
using UnityEngine;

namespace Gameplay.Entity.Base.View
{
    public class PlayerCharacter3DView : MonoBehaviour
    {
        [Header("View Components")] 
        [SerializeField] private Transform targetTransform;
        
        private static GameDataController GameData => GameDataController.ME;

        private GameObject _currentCharacterGraphics;
        
        private void Awake()
        {
            if (GameData == null) return;

            SetPlayerEntity3DView(GameData.CurrentPlayerEntityData);
            GameData.OnCurrentPlayerEntityDataSaved += OnPlayerEntityDataChangedHandler;
        }

        private void OnDestroy()
        {
            TryDestroyCurrentCharacter();
            
            if (GameData == null) return;
            GameData.OnCurrentPlayerEntityDataSaved -= OnPlayerEntityDataChangedHandler;
        }

        private void SetPlayerEntity3DView(EntityData entityData)
        {
            TryDestroyCurrentCharacter();
            _currentCharacterGraphics = Instantiate(entityData.EntityGraphicView, targetTransform);
        }

        private void TryDestroyCurrentCharacter()
        {
            if (_currentCharacterGraphics != null)
            {
                Destroy(_currentCharacterGraphics);
            }
        }

        private void OnPlayerEntityDataChangedHandler(EntityData entityData)
        {
            SetPlayerEntity3DView(entityData);
        }
    }
}