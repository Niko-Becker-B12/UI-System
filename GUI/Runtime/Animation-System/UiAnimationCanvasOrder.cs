using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.Utilities;



namespace GPUI
{
    public class UiAnimationCanvasOrder : UiAnimationBase
    {

        public int layer;


        public override Tween Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental,
            Ease easeType = Ease.Unset)
        {

            Tween tween = null;

            for (int i = 0; i < graphics.Count; i++)
            {

                if (graphics[i].TryGetComponent(out Canvas canvas))
                    canvas.sortingOrder = layer;

            }

            return tween;

        }

    }
}