using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class UiAnimationFadeCanvasGroup : UiAnimationBase
{

    public float fadeValue;


    public override void Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental, Ease easeType = Ease.Unset)
    {

        List<CanvasGroup> canvasGroups = new List<CanvasGroup>();

        for(int i = 0; i < graphics.Count; i++)
        {

            if (graphics[i].TryGetComponent<CanvasGroup>(out CanvasGroup group))
                canvasGroups.Add(group);
            else
                continue;

        }

        for (int i = 0; i < canvasGroups.Count; i++)
        {

            if (loop)
            {

                canvasGroups[i].DOFade(fadeValue, duration).SetLoops(-1, loopType).SetEase(easeType);

            }
            else
            {

                canvasGroups[i].DOFade(fadeValue, duration).SetEase(easeType);

            }

        }

    }

}