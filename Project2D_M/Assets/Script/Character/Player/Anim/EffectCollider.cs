using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class EffectCollider : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;
    PolygonCollider2D meshCollider = null;
    SkeletonRenderer skeletonRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshCollider = this.GetComponent<PolygonCollider2D>();
        skeletonRenderer = this.GetComponent<SkeletonRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        DrawBoundingBoxes(skeletonRenderer.transform, skeletonRenderer.skeleton);
    }

    private void DrawBoundingBoxes(Transform transform, Skeleton skeleton)
    {
        
        int count = 0;
        Vector2[] temp = new Vector2[0];
        foreach (var slot in skeleton.Slots)
        {
            var bba = slot.Attachment as BoundingBoxAttachment;
            if (bba != null)
            {
                temp = DrawBoundingBox(slot, bba, transform);
                if (temp != null)
                {
                    System.Array.Resize(ref temp, count+1);
                    meshCollider.SetPath(count, temp);
                    count++;
                }
            }
        }

        //meshCollider.pathCount = count;
        //count = 0;
        //foreach (var slot in skeleton.Slots)
        //{
        //    var bba = slot.Attachment as BoundingBoxAttachment;
        //    if (bba != null)
        //    {
        //        temp = DrawBoundingBox(slot, bba, transform);
        //        if (temp != null)
        //        {
        //            meshCollider.SetPath(count, temp);
        //            count++;
        //        }
        //    }
        //}
    }

    private Vector2[] DrawBoundingBox(Slot slot, BoundingBoxAttachment box, Transform t)
    {
        if (box.Vertices.Length <= 2) return null; // Handle cases where user creates a BoundingBoxAttachment but doesn't actually define it.

        var worldVerts = new float[box.WorldVerticesLength];
        box.ComputeWorldVertices(slot, worldVerts);

        Vector3 vert = Vector3.zero;

        Vector2[] temp = new Vector2[worldVerts.Length/2];

        for (int i = 0; i < worldVerts.Length; i += 2)
        {
            vert.x = worldVerts[i];
            vert.y = worldVerts[i + 1];
            vert.z = 0;

            vert = t.TransformPoint(vert);

            temp[i/2] = vert * 1.5f;
        }
        return temp;
    }
}
