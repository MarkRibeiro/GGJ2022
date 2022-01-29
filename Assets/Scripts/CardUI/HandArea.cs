using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandArea : MonoBehaviour
{
    int lastChildCount;
    [SerializeField]
    float radius;
    [SerializeField]
    float angleBase,angleStep,angleMax;
    [SerializeField]
    float height;

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
        Vector2 center = new Vector2(transform.position.x, transform.position.y - height);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, -angleBase));
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, angleBase));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, -angleMax));
        Gizmos.DrawLine(center, center + Rotate(Vector2.up * radius, angleMax));
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
        float angle = Mathf.Clamp(angleBase + angleStep * (transform.childCount - 1),0,angleMax);
        Vector2 center = new Vector2(transform.position.x, transform.position.y - height);
        int childrenCount = transform.childCount;
        if (childrenCount == 0)
        {
            return;
        }
        if(childrenCount == 1)
        {
            RectTransform child = transform.GetChild(0) as RectTransform;
            var anim = child.GetComponent<CardAnimation>();
            if (anim == null)
            {
                child.SetPositionAndRotation(
                                center + Rotate(Vector2.up * radius, 0),
                                Quaternion.identity);
            }
            else
            {
                anim.GoToPosition(center + Rotate(Vector2.up * radius,0));
                anim.SetRotation(Quaternion.identity);
            }
            return;
        }
        float step = 2 * angle / (childrenCount - 1) ;
        foreach (RectTransform child in transform)
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
