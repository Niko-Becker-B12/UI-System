using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class UiAnimationFade : UiAnimationBase
{

    public float fadeValue;


    public override void Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental, Ease easeType = Ease.Unset)
    {

        for(int i = 0; i < graphics.Count; i++)
        {

            if(loop)
            {

                graphics[i].DOFade(fadeValue, duration).SetLoops(-1, loopType).SetEase(easeType);

            }
            else
            {

                graphics[i].DOFade(fadeValue, duration).SetEase(easeType);

            }

        }

    }

}