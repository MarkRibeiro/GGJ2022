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
    private void Start() {
        UpdatePositions();
    }
    private void OnEnable() {
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

        float step = (rect.rect.width - 2*margin) / (childrenCount + 1);

        foreach (RectTransform child in transform)
        {
            var anim = child.GetComponent<CardAnimation>();
            if (anim == null)
            {
                child.position = new Vector2(rect.rect.xMin + step * (child.GetSiblingIndex() + 1 ) + margin, 0);
            }
            else
            {
                anim.GoToPosition(new Vector2(rect.rect.xMin + step * (child.GetSiblingIndex() + 1 ) + margin, 0));
            }
        }

    }
}

