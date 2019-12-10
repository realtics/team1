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
	private Transform m_objectTransform = null;
    private void Awake()
    {
        m_meshRenderer = this.GetComponent<MeshRenderer>();
        m_meshCollider = this.GetComponent<PolygonCollider2D>();
        m_skeletonRenderer = this.GetComponent<SkeletonRenderer>();
        m_objectTransform = this.transform.root;
    }
    public void ColliderDraw(float _collisionSize = 1.0f)
    {
        if (m_meshRenderer.materials.Length == 0)
            return;

        DrawBoundingBoxes(m_skeletonRenderer.transform, m_skeletonRenderer.skeleton,_collisionSize);
    }

    private void DrawBoundingBoxes(Transform transform, Skeleton skeleton,float _collisionSize)
    {
        int count = 1;
        ExposedList<Slot>.Enumerator enmerator = skeleton.slots.GetEnumerator();
        while(enmerator.MoveNext())
        {
            BoundingBoxAttachment bba = enmerator.Current.Attachment as BoundingBoxAttachment;
            if (bba != null)
            {
                Vector2[] boxArray = DrawBoundingBox(enmerator.Current, bba, transform, _collisionSize);
                if (boxArray != null)
                {
                    m_meshCollider.pathCount = count;
                    m_meshCollider.SetPath(count - 1, boxArray);
                    count++;
                }
            }
        }
    }

    private Vector2[] DrawBoundingBox(Slot slot, BoundingBoxAttachment box, Transform t, float _collisionSize)
    {
        if (box.Vertices.Length <= 2) return null;

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
            vert = m_objectTransform.InverseTransformPoint(vert);
            temp[i/2] = (Vector2)vert * _collisionSize;
        }
		return temp;
    }

	public void DeleteCollider()
	{
		m_meshCollider.pathCount = 0;
	}
}
