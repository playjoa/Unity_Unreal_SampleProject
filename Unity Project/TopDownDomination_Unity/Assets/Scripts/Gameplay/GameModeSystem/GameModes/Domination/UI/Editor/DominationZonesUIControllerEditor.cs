using UnityEditor;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    [CustomEditor(typeof(DominationZonesUIPanel))]
    public class DominationZonesUIControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var uiHealthController = (DominationZonesUIPanel)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                uiHealthController.UpdateChildModules();
                EditorUtility.SetDirty(uiHealthController);
            }
        }
    }
}