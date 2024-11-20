using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.Utilities;



public class UiAnimationTransform : UiAnimationBase
{

    public List<Vector3> transforms = new List<Vector3>();
    public List<Vector3> rotations = new List<Vector3>();
    public List<Vector3> scales = new List<Vector3>();


    public override void Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental, Ease easeType = Ease.Unset)
    {

        transforms.SetLength(graphics.Count);
        rotations.SetLength(graphics.Count);
        scales.SetLength(graphics.Count);

        for (int i = 0; i < graphics.Count; i++)
        {

            if(loop)
            {

                graphics[i].rectTransform.DOAnchorPos(transforms[i], duration).SetLoops(-1, loopType).SetEase(easeType);
                graphics[i].rectTransform.DORotate(rotations[i], duration).SetLoops(-1, loopType).SetEase(easeType);
                graphics[i].rectTransform.DOScale(scales[i], duration).SetLoops(-1, loopType).SetEase(easeType);

            }
            else
            {

                graphics[i].rectTransform.DOAnchorPos(transforms[i], duration).SetEase(easeType);
                graphics[i].rectTransform.DORotate(rotations[i], duration).SetEase(easeType);
                graphics[i].rectTransform.DOScale(scales[i], duration).SetEase(easeType);

            }

        }

    }

}