using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;


namespace GPUI
{
    [Serializable]

    public class UiAnimationBase
    {

        public float duration;

        public bool isRunning = false;

        public virtual Tween Play(List<Graphic> graphics, bool loop = false, LoopType loopType = LoopType.Incremental,
            Ease easeType = Ease.Unset)
        {

            Tween tween = null;

            return tween;

        }

    }
}