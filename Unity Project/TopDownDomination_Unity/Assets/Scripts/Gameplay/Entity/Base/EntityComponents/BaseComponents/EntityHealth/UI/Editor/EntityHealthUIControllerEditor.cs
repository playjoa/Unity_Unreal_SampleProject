using Gameplay.Entity.Base.Components.UI;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityHealth.UI
{
    [CustomEditor(typeof(EntityHealthUIController))]
    public class EntityHealthUIControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var uiHealthController = (EntityHealthUIController)target;
            if (GUILayout.Button("Update Child Modules (Editor Only)"))
            {
                uiHealthController.UpdateChildModules();
                EditorUtility.SetDirty(uiHealthController);
            }
        }
    }
}