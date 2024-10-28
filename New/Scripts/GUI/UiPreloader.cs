using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

public class UiPreloader : UiElement
{

    public Arc preloaderArc;
    public Arc preloaderArc2;


    private void LateUpdate()
    {

        preloaderArc.EllipseProperties.BaseAngle += 1f * Time.deltaTime;
        preloaderArc2.EllipseProperties.BaseAngle += 1f * Time.deltaTime;
        preloaderArc.SetAllDirty();
        preloaderArc.ForceMeshUpdate();
        preloaderArc2.SetAllDirty();
        preloaderArc2.ForceMeshUpdate();

    }

}