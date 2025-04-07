using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;


[Serializable]
public class UiAnimationBase
{

    public float duration;

    public virtual void Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental, Ease easeType = Ease.Unset)
    {



    }

}