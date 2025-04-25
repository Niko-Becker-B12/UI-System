using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace GPUI
{
    [System.Serializable]

    public class UiAnimationFade : UiAnimationBase
    {

        public float fadeValue;


        public override Tween Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental,
            Ease easeType = Ease.Unset)
        {

            Tween tween = null;

            for (int i = 0; i < graphics.Count; i++)
            {

                if (loop)
                {

                    tween = DOTween.Sequence().Insert(0f,
                        graphics[i].DOFade(fadeValue, duration).SetLoops(-1, loopType).SetEase(easeType));

                }
                else
                {

                    tween = DOTween.Sequence().Insert(0f, graphics[i].DOFade(fadeValue, duration).SetEase(easeType));

                }

            }

            return tween;

        }

    }
}