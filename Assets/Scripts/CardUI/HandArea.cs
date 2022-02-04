using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HandArea : MonoBehaviour
{
    int lastChildCount;
    [SerializeField]
    private float margin;
    private RectTransform _rect;

    RectTransform rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }

    private void Awake()
    {
        UpdatePositions();
    }

    private void LateUpdate()
    {
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        int childrenCount = transform.childCount;
        lastChildCount = childrenCount;
        if (childrenCount == 0)
        {
            return;
        }
        if(childrenCount == 1)
        {
            RectTransform child = transform.GetChild(0) as RectTransform;
            var anim = child.GetComponent<CardAnimation>();
            Vector2 position = rect.rect.center;
            if (anim == null)
            {
                child.position = position;
            }
            else
            {
                anim.GoToPosition(position);
                anim.SetRotation(Quaternion.identity);
            }
            return;
        }


        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        float r2 = (rect.rect.width + margin - childrenCount *(childWidth + margin)) ;
        float start = r2 / 2 + childWidth / 2;

        // Debug.Log((childWidth + margin)*childrenCount - margin);
        // Debug.Log(rect.rect.width);

        foreach(RectTransform child in transform)
        {
            var anim = child.GetComponent<CardAnimation>();
            float xposition = rect.rect.xMin + start;
            if (anim == null)
            {
                child.anchoredPosition = new Vector2(xposition + child.GetSiblingIndex() * Mathf.Min(childWidth + margin, rect.rect.width! / (float)childrenCount ), rect.rect.center.y);
            }
            else
            {
                anim.GoToPosition(new Vector2(xposition + child.GetSiblingIndex() * Mathf.Min(childWidth + margin, rect.rect.width! / (float)childrenCount ), rect.rect.center.y));
                anim.SetRotation(Quaternion.identity);
            }
        }

    }

    public float GetMargin()
    {
        return margin;
    }
}

