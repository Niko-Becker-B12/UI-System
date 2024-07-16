using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.Utilities;


[System.Serializable]
public class UiAnimationTransform : UiAnimationBase
{

    public List<Vector3> transforms = new List<Vector3>();


    public override void Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental)
    {

        transforms.SetLength(graphics.Count);

        for (int i = 0; i < graphics.Count; i++)
        {

            if (transforms[i].magnitude == 0)
                transforms[i] = graphics[i].rectTransform.position;

            if (loop)
                graphics[i].rectTransform.DOAnchorPos(transforms[i], duration).SetLoops(-1, loopType);
            else
                graphics[i].rectTransform.DOAnchorPos(transforms[i], duration);
        }

    }

}