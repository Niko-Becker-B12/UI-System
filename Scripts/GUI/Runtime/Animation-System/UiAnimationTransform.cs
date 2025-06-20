using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.Utilities;



namespace GPUI
{
    public class UiAnimationTransform : UiAnimationBase
    {

        public bool isAbsolute = false;

        public List<Vector3> transforms = new List<Vector3>();
        public List<Vector3> rotations = new List<Vector3>();
        public List<Vector3> scales = new List<Vector3>();


        public override Tween Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental,
            Ease easeType = Ease.Unset)
        {

            Tween tween = null;

            transforms.SetLength(graphics.Count);
            rotations.SetLength(graphics.Count);
            scales.SetLength(graphics.Count);

            for (int i = 0; i < graphics.Count; i++)
            {

                if (loop)
                {

                    if (isAbsolute)
                        tween = DOTween.Sequence().Insert(0f,
                            graphics[i].rectTransform.DOAnchorPos(transforms[i], duration).SetLoops(-1, loopType)
                                .SetEase(easeType));
                    else
                        tween = DOTween.Sequence().Insert(0f,
                            graphics[i].rectTransform
                                .DOAnchorPos(
                                    new Vector3(graphics[i].rectTransform.anchoredPosition.x,
                                        graphics[i].rectTransform.anchoredPosition.y, 0f) + transforms[i], duration)
                                .SetLoops(-1, loopType).SetEase(easeType));

                    tween = DOTween.Sequence().Insert(0f,
                        graphics[i].rectTransform.DOLocalRotate(rotations[i], duration).SetLoops(-1, loopType)
                            .SetEase(easeType));
                    tween = DOTween.Sequence().Insert(0f,
                        graphics[i].rectTransform.DOScale(scales[i], duration).SetLoops(-1, loopType)
                            .SetEase(easeType));

                }
                else
                {

                    if (isAbsolute)
                        tween = DOTween.Sequence().Insert(0f,
                            graphics[i].rectTransform.DOAnchorPos(transforms[i], duration).SetEase(easeType));
                    else
                        tween = DOTween.Sequence().Insert(0f,
                            graphics[i].rectTransform
                                .DOAnchorPos(
                                    new Vector3(graphics[i].rectTransform.anchoredPosition.x,
                                        graphics[i].rectTransform.anchoredPosition.y, 0f) + transforms[i], duration)
                                .SetEase(easeType));

                    tween = DOTween.Sequence().Insert(0f,
                        graphics[i].rectTransform.DOLocalRotate(rotations[i], duration).SetEase(easeType));
                    tween = DOTween.Sequence().Insert(0f,
                        graphics[i].rectTransform.DOScale(scales[i], duration).SetEase(easeType));

                }

            }

            return tween;

        }

    }
}