using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UiAnimationData
{

    public string trigger;
    public bool playOnAwake = false;
    public bool loop = false;

    public List<Graphic> targetGraphics = new List<Graphic>();

    [NonSerialized, OdinSerialize, PolymorphicDrawerSettings(ShowBaseType = false)]
    public UiAnimationBase animation;

    public void Play()
    {

        animation.Play(targetGraphics, loop);

    }

}