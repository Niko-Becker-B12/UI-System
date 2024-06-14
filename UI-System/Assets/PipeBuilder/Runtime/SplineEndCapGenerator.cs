using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;


[ExecuteInEditMode]
[AddComponentMenu("Splines/Spline End Cap Generator"), RequireComponent(typeof(SplineExtrude))]
public class SplineEndCapGenerator : SplineComponent
{

    public GameObject endCapObject;

    SplineContainer splineContainer;

    SplineExtrude extrude;

    [HideInInspector]
    public List<GameObject> children = new List<GameObject>();

    private void OnEnable()
    {

        splineContainer = GetComponent<SplineContainer>();

        extrude = GetComponent<SplineExtrude>();

        RebuildMesh();

        Spline.Changed += OnSplineChanged;

    }

    private void OnDisable()
    {

        Spline.Changed -= OnSplineChanged;

    }

    void OnSplineChanged(Spline spline, int index, SplineModification mod)
    {

        RebuildMesh();

    }

    void RebuildMesh()
    {

        for(int i = 0; i < children.Count; i++)
        {

            DestroyImmediate(children[i]);
        
        }

        children.Clear();

        if (endCapObject == null)
        {

            for (int i = 0; i < children.Count; i++)
            {

                DestroyImmediate(children[i]);

            }

            children.Clear();

            return;

        }

        for(int i = 0; i < splineContainer.Splines.Count; i++) 
        {

            if (splineContainer.Splines[i].Closed)
                continue;

            GameObject newStartKnotEndCap = Instantiate(endCapObject, (Vector3)splineContainer.Splines[i][0].Position + this.transform.position,
                splineContainer.Splines[i][0].Rotation, this.transform);

            GameObject newEndKnotEndCap = Instantiate(endCapObject, (Vector3)splineContainer.Splines[i][splineContainer.Splines[i].Count - 1].Position + this.transform.position,
                splineContainer.Splines[i][splineContainer.Splines[i].Count - 1].Rotation, this.transform);

            newStartKnotEndCap.transform.localScale = new Vector3(1, 1, -1);

            children.Add(newStartKnotEndCap);
            children.Add(newEndKnotEndCap);

            newStartKnotEndCap.hideFlags = HideFlags.HideInHierarchy;
            newEndKnotEndCap.hideFlags = HideFlags.HideInHierarchy;

        }

    }

}
