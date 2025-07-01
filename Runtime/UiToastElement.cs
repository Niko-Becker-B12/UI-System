using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Adapters;
using LitMotion.Collections;
using LitMotion.Extensions;
using Sirenix.Utilities;

namespace GPUI
{
    public class UiToastElement : UiElement
    {

        public UiText headerText;
        public UiText messageText;

        public Graphic icon;
        public UiButton closeButton;

        [SuffixLabel("s", true)] public float lifetime = 0;
        float time = 0;

        public event Action OnToastActive;
        public event Action OnToastInactive;

        bool lifetimeTimerStarted = false;
        bool canClose = false;

        [FoldoutGroup("Events")] public Function OnSetActive;

        [FoldoutGroup("Events")] public Function OnSetInactive;

        public UiButton additionalButton;


        private void Start()
        {

            OnSetActive.functionName.AddListener(() =>
            {

                OnToastActive?.Invoke();

                if (lifetime > 0)
                    lifetimeTimerStarted = true;
                else
                    canClose = true;

            });

            OnSetInactive.functionName.AddListener(() =>
            {

                if (lifetimeTimerStarted) //|| canClose && time == lifetime)
                {

                    lifetimeTimerStarted = false;
                    time = 0;

                    OnToastInactive?.Invoke();

                    //
                    Destroy(this.gameObject);

                }

            });

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (skinData != null && skinData is UiToastElementSkinDataObject)
            {

                UiToastElementSkinDataObject toastSkinData = skinData as UiToastElementSkinDataObject;

                if (icon != null)
                    (icon as Image).sprite = toastSkinData.toastIcon;

                closeButton.skinData = toastSkinData.closeButtonSkinData;
                additionalButton.skinData = toastSkinData.additionalButtonSkinData;

            }

        }

        public override void FadeElement(bool fadeIn = false)
        {

            if (fadeIn)
            {

                if (canvasGroup.alpha > 0f)
                    return;

                if (setActiveAnimation == null || setActiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    LMotion.Create(canvasGroup.alpha, 1f, .5f)
                        .WithOnComplete(() =>
                        {
                            canvasGroup.blocksRaycasts = true;
                            canvasGroup.interactable = true;
                            
                            Function.InvokeEvent(OnSetActive, this);
                            
                        })
                        .BindToAlpha(canvasGroup);

                }
                else
                    setActiveAnimation.Play();

            }
            else
            {

                if (canvasGroup.alpha < 1f)
                    return;

                if (setInactiveAnimation == null || setInactiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    LMotion.Create(canvasGroup.alpha, 0f, .5f)
                        .WithOnComplete(() =>
                        {
                            canvasGroup.blocksRaycasts = false;
                            canvasGroup.interactable = false;
                            
                            Function.InvokeEvent(OnSetInactive, this);
                            
                        })
                        .BindToAlpha(canvasGroup);

                }
                else
                    setInactiveAnimation.Play();

            }

        }

        private void Update()
        {

            if (!lifetimeTimerStarted)
                return;

            if (time < lifetime)
                time = time + Time.deltaTime;
            else
                FadeElement(false);

        }

    }
}