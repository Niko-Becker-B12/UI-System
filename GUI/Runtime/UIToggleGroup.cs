using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUI
{
    public class UIToggleGroup : MonoBehaviour
    {

        public List<UiToggle> toggles = new List<UiToggle>();

        public bool allowSwitchOff = false;

        public int currentActiveButtonIndex = -1;
        public int index = -1;

        public void OnToggleChanged(UiToggle toggle)
        {

            if (!toggle._toggleBehavior.isActive)
            {

                index = toggles.FindIndex((x) => x == toggle);

                if (index == currentActiveButtonIndex)
                    toggle.OnClick(true);
                else
                    currentActiveButtonIndex = index;

            }
            else
            {

                currentActiveButtonIndex = toggles.FindIndex((x) => x == toggle);

            }



        }

    }
}