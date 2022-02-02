using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField]
    AnimationCurve positionCurve;
    [SerializeField]
    float positionTime;
    Coroutine positionCoroutine;
    RectTransform _rect;
    RectTransform rect
    {
        get
        {
            if (_rect == null)
                _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }



    public void GoToPosition(Vector3 position)
    {
        if (positionCoroutine != null)
        {
            StopCoroutine(positionCoroutine);
        }
        positionCoroutine = StartCoroutine(GoToPositionCoroutine(position));
    }
    private IEnumerator GoToPositionCoroutine(Vector3 position)
    {
        float t = 0;
        Vector2 startPosition = rect.anchoredPosition;
        while (t < positionTime)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(position.x,0), positionCurve.Evaluate(t/ positionTime));
            yield return new WaitForEndOfFrame();
        }
        positionCoroutine = null;
    }

    [SerializeField]
    AnimationCurve rotationCurve;
    [SerializeField]
    float rotationTime;
    Coroutine rotationCoroutine;

    public void SetRotation(Quaternion rotation)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(SetRotationCoroutine(rotation));
    }

    private IEnumerator SetRotationCoroutine(Quaternion rotation)
    {
        float t = 0;
        Quaternion startRotation = transform.rotation;
        while (t < rotationTime)
        {
            t += Time.deltaTime ;
            transform.rotation = Quaternion.Lerp(startRotation, rotation, rotationCurve.Evaluate(t/ rotationTime));
            yield return new WaitForEndOfFrame();
        }
        rotationCoroutine = null;
    }
}
