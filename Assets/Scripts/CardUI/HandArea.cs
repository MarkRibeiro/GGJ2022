using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HandArea : MonoBehaviour
{
    int lastChildCount;
    [SerializeField]
    private float padding;
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
        if (transform.childCount != lastChildCount)
        {
            UpdatePositions();
        }
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
            Debug.Log(position);
            Debug.Log(rect.rect.xMin);
            Debug.Log(rect.rect.xMax);
            Debug.Log(rect.rect.width);
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

        float step = rect.rect.width / (childrenCount + 1);

        foreach (RectTransform child in transform)
        {
            var anim = child.GetComponent<CardAnimation>();
            if (anim == null)
            {
                child.position = new Vector2(rect.rect.xMin + step * (child.GetSiblingIndex() + 1 ), 0);
            }
            else
            {
                anim.GoToPosition(new Vector2(rect.rect.xMin + step * (child.GetSiblingIndex() + 1 ), 0));
            }
        }

    }
}

