using System;
using Sirenix.OdinInspector;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.Events;
using GPUI;

namespace GPUI
{
    public class UiTimer : UiElementExtended
    {

        [TabGroup("Tabs", "UI Elements")] public float timer = 0f;

        [TabGroup("Tabs", "UI Elements")] public bool startOnAwake = false;

        private float currentTime;

        [TabGroup("Tabs", "Events")] public UnityEvent OnTimerFinished;
        [TabGroup("Tabs", "Events")] public UnityEvent OnTimerStarted;


        bool timerStarted = false;

        public bool TimerStarted
        {
            get { return timerStarted; }
            set
            {
                timerStarted = value;

                OnTimerStarted?.Invoke();

            }


        }

        protected override void Start()
        {

            base.Start();
            
            if (startOnAwake)
                TimerStarted = true;

        }

        public override void ApplySkinData()
        {
            base.ApplySkinData();

            if (skinData == null)
                return;
            
            if (detailGraphic != null && detailGraphic.TryGetComponent<Sector>(out Sector sector) && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                detailGraphic.color = detailedSkinData.detailColor.normalColor;

                sector.ArcProperties.Length = 1f;

            }

        }

        private void FixedUpdate()
        {

            if (timerStarted && currentTime < timer)
            {
                currentTime += Time.fixedDeltaTime;
                (detailGraphic as Sector).ArcProperties.Length = currentTime / timer;
                (detailGraphic as Sector).SetAllDirty();
            }
            else if (timerStarted && currentTime >= timer)
            {

                timerStarted = false;
                OnTimerFinished?.Invoke();

            }

        }
    }
}