using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GPUI
{
    public class UiWindowModal : UiWindow
    {

        [TabGroup("Tabs", "UI Elements")] public UiButton closeButton;

        [TabGroup("Tabs", "UI Elements")] public bool useManagerForClosing = false;


        public void Awake()
        {

            //To-Do:

            if (this.skinData != null && closeButton != null)
            {

                closeButton.skinData = (this.skinData as UiWindowModalSkinDataObject).closeButtonSkinData;
                closeButton.ApplySkinData();

            }

            base.Awake();

        }

        private void Start()
        {

            
            
        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            closeButton?.ApplySkinData();

        }

    }
}