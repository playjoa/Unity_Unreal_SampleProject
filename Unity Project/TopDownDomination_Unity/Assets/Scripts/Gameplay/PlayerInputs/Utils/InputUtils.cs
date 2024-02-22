using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.PlayerInputs.Utils
{
    public static class InputUtils
    {
        private static int UILayer => LayerMask.NameToLayer("UI");

        public static bool MouseOnTopOfUI()
        {
            return IsPointerOverAnyUIElement(GetEventSystemRaycastResults());
        }

        private static IEnumerable<RaycastResult> GetEventSystemRaycastResults()
        {
            var raycastResults = new List<RaycastResult>();
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }

        private static bool IsPointerOverAnyUIElement(IEnumerable<RaycastResult> eventSystemRaycastResults)
        {
            return eventSystemRaycastResults.Any(curRaycastResult => curRaycastResult.gameObject.layer == UILayer);
        }
    }
}