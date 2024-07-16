using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimationComponent : SerializedMonoBehaviour
{

    [NonSerialized, OdinSerialize]
    public List<UiAnimationData> animationData = new List<UiAnimationData>();


    private void Start()
    {

        for (int i = 0; i < animationData.Count; i++)
        {

            if (animationData[i].playOnAwake)
            {

                animationData[i].Play();

            }
            else
                continue;

        }

    }

    public void Play(string data = "")
    {

        for (int i = 0; i < animationData.Count; i++)
        {

            if (animationData[i].trigger == data)
            {

                animationData[i].Play();

            }
            else
                continue;

        }

    }

}