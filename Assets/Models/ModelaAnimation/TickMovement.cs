using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickMovement : MonoBehaviour
{
    private const float ANGLE_STEP = 90f;
    private float _currentZRot, _currentYRot, _currentYHeight;
    private float _unlerpedZRot, _unlerpedYRot;

    public float animationRoundingStep = 0.5f;
    public float animationRoundingDirection = 0.5f;
    public float animationRoundingHeight = 0.5f;
    [Range(0f, 1f)] public float lerpAnimationStep = 0.5f;
    [Range(0f, 1f)] public float lerpAnimationDirection = 0.5f;
    [Range(0f, 1f)] public float lerpAnimationHeight = 0.5f;

    public float heightDifferece = 1f;

    Coroutine zRotationCoroutine, yRotationCoroutine, heightChangeCoroutine;

    void Start()
    {
        _unlerpedZRot = 0;
        _unlerpedYRot = 0;
        _currentYRot = _unlerpedYRot;
        _currentZRot = _unlerpedZRot;
    }

    void Update()
    {
        gameObject.transform.localEulerAngles = new Vector3(0, _currentYRot, _currentZRot);
        gameObject.transform.localPosition = new Vector3(0, _currentYHeight, 0);
    }

    public void startStepAnimation(int rotation)
    {

        Debug.Log("Animation Triggered");
        float targetZAngle = _unlerpedZRot - ANGLE_STEP;
        _currentZRot = SpecialMath.RoundToNumber(_currentZRot, 90);
        if(zRotationCoroutine != null) StopCoroutine(zRotationCoroutine);
        zRotationCoroutine = StartCoroutine("rotateZAnimation", targetZAngle);

        _unlerpedZRot -= ANGLE_STEP;

        _currentYHeight = SpecialMath.RoundToNumber(_currentYHeight, heightDifferece);

        if(heightChangeCoroutine != null) StopCoroutine(heightChangeCoroutine);
        if (_unlerpedZRot / ANGLE_STEP % 2 != 0)
        {
            heightChangeCoroutine = StartCoroutine("heightAnimation", heightDifferece);
        }
        else
        {
            heightChangeCoroutine = StartCoroutine("heightAnimation", 0);
        }

        switch (rotation)
        {
            case 0:
                _unlerpedYRot = 0;
                break;
            case 1:
                _unlerpedYRot = 90;
                break;
            case 2:
                _unlerpedYRot = 180;
                break;
            case 3:
                _unlerpedYRot = 270;
                break;
            default:
                Debug.LogWarning("Setting Mite Rotation out of bounds! (should be from 0 to 3)", gameObject.transform);
                _unlerpedYRot = 0;
                break;
        }

        _currentYRot = SpecialMath.RoundToNumber(_currentYRot, 90);
        if(yRotationCoroutine != null) StopCoroutine(yRotationCoroutine);
        yRotationCoroutine = StartCoroutine("rotateDirectionAnimation", _unlerpedYRot);


    }

    IEnumerator rotateZAnimation(float target)
    {
        while (_currentZRot > target)
        {
            if (_currentZRot > target + animationRoundingStep)
            {
                _currentZRot = Mathf.Lerp(_currentZRot, target, lerpAnimationStep);
            }
            else
            {
                _currentZRot = target;
            }
            yield return null;
        }
    }

    IEnumerator rotateDirectionAnimation(float target)
    {
        if (_currentYRot == 0 && target == 270)
        {
            _currentYRot = 360;
        }

        if(_currentYRot == 270 && target == 0)
        {
            _currentYRot = -90;
        }

        while (_currentYRot != target)
        {
            if (_currentYRot > target + animationRoundingDirection || _currentYRot < target - animationRoundingDirection)
            {
                _currentYRot = Mathf.Lerp(_currentYRot, target, lerpAnimationDirection);
            }
            else
            {
                _currentYRot = target;
            }
            yield return null;
        }

    }

    IEnumerator heightAnimation(float target)
    {
        while (_currentYHeight != target)
        {
            if (_currentYHeight < target - animationRoundingHeight || _currentYHeight > target + animationRoundingHeight)
            {
                _currentYHeight = Mathf.Lerp(_currentYHeight, target, lerpAnimationHeight);
            }
            else
            {
                _currentYHeight = target;
            }
            yield return null;
        }

        
    }

}

public class SpecialMath
{
    public static float RoundToNumber(float input, float roundingNumber)
    {
        float lowNumber = Mathf.FloorToInt(input / roundingNumber) * roundingNumber;
        float highNumber = lowNumber + roundingNumber;

        float distanceFromLow = Mathf.Abs(input - lowNumber);

        if(distanceFromLow >= roundingNumber / 2)
        {
            return highNumber;
        }
        else
        {
            return lowNumber;
        }
    }
}
