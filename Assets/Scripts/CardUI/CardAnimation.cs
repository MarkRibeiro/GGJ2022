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
        while (t < positionTime)
        {
            positionTime += Time.deltaTime;
            transform.position = new Vector3(positionCurve.Evaluate(t / positionTime), transform.position.y, transform.position.z);
            yield return null;
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
        while (t < rotationTime)
        {
            rotationTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationCurve.Evaluate(t / rotationTime));
            yield return null;
        }
        rotationCoroutine = null;
    }
}
