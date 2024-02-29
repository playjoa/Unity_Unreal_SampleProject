using UnityEditor;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    [CustomEditor(typeof(GameUIController))]
    public class GameModeUIControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var uiHealthController = (GameUIController)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                uiHealthController.UpdateChildModules();
                EditorUtility.SetDirty(uiHealthController);
            }
        }
    }
}