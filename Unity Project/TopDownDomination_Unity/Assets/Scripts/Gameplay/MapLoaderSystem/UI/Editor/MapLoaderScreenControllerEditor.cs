using Gameplay.MapLoaderSystem.Components;
using UnityEditor;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.UI.Editor
{
    public class MapLoaderScreenControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mapLoaderScreenController = (MapLoaderScreenController)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                mapLoaderScreenController.UpdateChildModules();
                EditorUtility.SetDirty(mapLoaderScreenController);
            }
        }
    }
}