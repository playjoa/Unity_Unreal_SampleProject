using Gameplay.Entity.Base.Components.UI;
using UnityEditor;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    [CustomEditor(typeof(GameModeUIController))]
    public class GameModeUIControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var uiHealthController = (GameModeUIController)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                uiHealthController.UpdateChildModules();
                EditorUtility.SetDirty(uiHealthController);
            }
        }
    }
}