using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_14
 * 팀              : 1팀
 * 스크립트 용도   : Spine의 애니메이션에 맞춰 PolygonCollider2D의 콜라이더 크기를 조절(CharacterTransform를 기준으로 한다.)
 */
 [RequireComponent(typeof(PolygonCollider2D))]
public class SpineAnimCollider : MonoBehaviour
{
    private MeshRenderer m_meshRenderer = null;
    private SkeletonRenderer m_skeletonRenderer = null;
    private PolygonCollider2D m_meshCollider = null;

    public Transform ObjectTransform;

    private void Awake()
    {
        m_meshRenderer = this.GetComponent<MeshRenderer>();
        m_meshCollider = this.GetComponent<PolygonCollider2D>();
        m_skeletonRenderer = this.GetComponent<SkeletonRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_meshRenderer.materials.Length > 0)
            m_meshCollider.enabled = true;
        else m_meshCollider.enabled = false;

        if (m_meshCollider.enabled)
            DrawBoundingBoxes(m_skeletonRenderer.transform, m_skeletonRenderer.skeleton);
    }

    private void DrawBoundingBoxes(Transform transform, Skeleton skeleton)
    {
        int count = 1;
        Vector2[] temp;
        foreach (var slot in skeleton.Slots)
        {
            var bba = slot.Attachment as BoundingBoxAttachment;
            if (bba != null)
            {
                temp = DrawBoundingBox(slot, bba, transform);
                if (temp != null)
                {
                    m_meshCollider.pathCount = count;
                    m_meshCollider.SetPath(count - 1, temp);
                    count++;
                }
            }
        }
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
            if (ObjectTransform != null)
                vert = ObjectTransform.InverseTransformPoint(vert);
            temp[i/2] = (Vector2)vert * 1f;

            //Debug.Log("Code : " + vert.x + "/" + vert.y);
        }
        return temp;
    }
}
