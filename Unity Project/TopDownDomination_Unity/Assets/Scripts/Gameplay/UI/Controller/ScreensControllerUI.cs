using System.Collections.Generic;
using Gameplay.UI.Base;
using UnityEngine;

namespace Gameplay.UI.Controller
{
    public class ScreensControllerUI : MonoBehaviour
    {
        [Header("UI Screens")]
        [SerializeField] private List<UIScreen> uiScreens;

        private void Awake()
        {
            foreach (var uiScreen in uiScreens)
            {
                uiScreen.Initiate();
            }
        }
    }
}