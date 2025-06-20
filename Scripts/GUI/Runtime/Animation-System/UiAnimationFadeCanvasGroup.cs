using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace GPUI
{
    [System.Serializable]

    public class UiAnimationFadeCanvasGroup : UiAnimationBase
    {

        public float fadeValue;


        public override Tween Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental,
            Ease easeType = Ease.Unset)
        {

            Tween tween = null;

            List<CanvasGroup> canvasGroups = new List<CanvasGroup>();

            for (int i = 0; i < graphics.Count; i++)
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

                    tween = DOTween.Sequence().Insert(0f,
                        canvasGroups[i].DOFade(fadeValue, duration).SetLoops(-1, loopType).SetEase(easeType));

                }
                else
                {

                    tween = DOTween.Sequence()
                        .Insert(0f, canvasGroups[i].DOFade(fadeValue, duration).SetEase(easeType));

                }

            }

            return tween;

        }

    }
}