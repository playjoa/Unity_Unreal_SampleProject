using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using UnityEditor;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination
{
    [CustomEditor(typeof(DominationMapController))]
    public class DominationControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var dominationController = (DominationMapController)target;
            if (GUILayout.Button("Get Domination Zones in Map (Editor Only)"))
            {
                dominationController.EditorFindDominationZonesInMap();
                EditorUtility.SetDirty(dominationController);
            }
        }
    }
}