using System;
using UnityEditor;
using UnityEngine;
using Utils.UniqueId.Components;

namespace Utils.UniqueId.Editor
{
    [CustomPropertyDrawer(typeof(UniqueIdAttribute))]
    public class UniqueIdDrawer : PropertyDrawer
    {
        public const int BUTTON_WIDTH = 180;
        
        private static bool NoIdSet(SerializedProperty property) => string.IsNullOrEmpty(property.stringValue);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var propertyRect = new Rect(position.x, position.y, position.width - BUTTON_WIDTH, position.height);
            var generateButtonRect = new Rect(position.x + (position.width - BUTTON_WIDTH), position.y, BUTTON_WIDTH / 2f, position.height);
            var copyButtonRect = new Rect(position.x + (position.width - BUTTON_WIDTH / 2f), position.y, BUTTON_WIDTH / 2f, position.height);

            
            GUI.enabled = false;

            if (NoIdSet(property) || IsDuplicateId(property))
            {
                SetNewPropertyId(property);
            }

            EditorGUI.PropertyField(propertyRect, property, label, true);

            GUI.enabled = true;

            if (GUI.Button(generateButtonRect, "Generate"))
            {
                SetNewPropertyId(property);
            }
            
            if (GUI.Button(copyButtonRect, "Copy"))
            {
                EditorGUIUtility.systemCopyBuffer = property.stringValue;
            }
        }

        private bool IsDuplicateId(SerializedProperty property)
        {
            var id = property.stringValue;
            var scriptableObjects = Resources.FindObjectsOfTypeAll<ScriptableObjectWithId>();

            foreach (var scriptableObject in scriptableObjects)
            {
                if (scriptableObject.Id == id && scriptableObject != property.serializedObject.targetObject)
                {
                    return true;
                }
            }

            return false;
        }
        
        private void SetNewPropertyId(SerializedProperty property)
        {
            property.stringValue = GenerateNewId();
        }

        private string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}