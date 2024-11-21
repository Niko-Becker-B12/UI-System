using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace BoothNavigator
{

    public class BoothNavigatorManager : MonoBehaviour
    {

        public GameObject boothMarkerPrefab;

        [ReadOnly]
        public BoothTour currentTour = null;

        public List<BoothArea> boothAreas = new List<BoothArea>();

        public List<BoothTour> boothTours = new List<BoothTour>();


        public event Action<BoothTopic> OnTopicAddedToTour;


        public void Start()
        {

            //UiToastHolder.Instance.CreateNewToast(out int index, "Welcome!", "This is the WebGPU-Demo\nfor our UI-Toolkit (GPUI).\nThis demo also showcases our\nUnity-based Booth-Navigator.\nEnjoy!", UiToastHolder.ToastElementType.message, 0, null, true);
            
            for(int i = 0; i < boothAreas.Count; i++)
            {

                for(int  j = 0; j < boothAreas[i].topics.Count; j++)
                {

                    GameObject newUiButton = Instantiate(boothMarkerPrefab);
                    newUiButton.transform.position = boothAreas[i].topics[j].position;

                    LookAtConstraint lookAt = newUiButton.GetComponent<LookAtConstraint>();
                    lookAt.AddSource(new ConstraintSource { sourceTransform = Camera.main.transform, weight = 1f});


                    UiButton uiButton = newUiButton.GetComponentInChildren<UiButton>();
                    uiButton.skinData = boothAreas[i].iconSkinObject;

                    uiButton.ApplySkinData();

                }

            }

        }

        [Button]
        public void AddTopicToCurrentTour(BoothTopic newTopic)
        {

            OnTopicAddedToTour?.Invoke(newTopic);

        }

    }


}