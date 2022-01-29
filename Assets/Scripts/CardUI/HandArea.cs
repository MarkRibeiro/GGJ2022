using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandArea : MonoBehaviour
{
    int lastChildCount;
    [SerializeField]
    float radius;
    [SerializeField]
    float angle;
    [SerializeField]
    Vector2 center;

    private static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, -angle));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, angle));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
        UpdatePositions();
    }

    private void Awake()
    {
        UpdatePositions();
        lastChildCount = transform.childCount;
    }
    private void LateUpdate()
    {
        if (transform.childCount != lastChildCount)
        {
            UpdatePositions();
            lastChildCount = transform.childCount;
        }
    }

    private void UpdatePositions()
    {
        int childrenCount = transform.childCount;
        if (childrenCount == 0)
        {
            return;
        }
        float step = childrenCount > 1 ? 2 * angle / (childrenCount - 1) : 0;
        foreach (Transform child in transform)
        {
            var anim = child.GetComponent<CardAnimation>();
            if (anim == null)
            {
                child.SetPositionAndRotation(
                                center + Rotate(Vector2.up * radius, -angle + step * child.GetSiblingIndex()),
                                Quaternion.Euler(0, 0, -angle + step * child.GetSiblingIndex()));
            }
            else
            {
                anim.GoToPosition(center + Rotate(Vector2.up * radius, -angle + step * child.GetSiblingIndex()));
                anim.SetRotation(Quaternion.Euler(0, 0, -angle + step * child.GetSiblingIndex()));
            }

        }
    }
}
