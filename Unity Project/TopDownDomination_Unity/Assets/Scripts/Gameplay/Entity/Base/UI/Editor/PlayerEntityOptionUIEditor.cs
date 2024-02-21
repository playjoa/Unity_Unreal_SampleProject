using UnityEditor;
using UnityEngine;

namespace Gameplay.Entity.Base.UI
{
    [CustomEditor(typeof(PlayerEntityOptionUI))]
    public class PlayerEntityOptionUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var uiHealthController = (PlayerEntityOptionUI)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                uiHealthController.UpdateChildModules();
                EditorUtility.SetDirty(uiHealthController);
            }
        }
    }
}