using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickMovement : MonoBehaviour
{
    private const float ANGLE_STEP = 90f;
    private float _currentZRot, _currentYRot, _currentYHeight;
    private float _unlerpedZRot, unlerpedYRot;

    public float animationRoundingStep = 0.5f;
    public float animationRoundingDirection = 0.5f;
    public float animationRoundingHeight = 0.5f;
    [Range (0f, 1f)] public float lerpAnimationStep = 0.5f;
    [Range (0f, 1f)] public float lerpAnimationDirection = 0.5f;
    [Range (0f, 1f)] public float lerpAnimationHeight = 0.5f;

    public float heightDifferece = 1f;
    
    void Start()
    {
        _unlerpedZRot = 0;
        _currentYRot = 0;
        _currentZRot = _unlerpedZRot;
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3 (0, _currentYRot, _currentZRot);
        gameObject.transform.localPosition = new Vector3(0, _currentYHeight, 0);
    }

    public void startStepAnimation(int rotation)
    {
        
        float targetZAngle = _unlerpedZRot - ANGLE_STEP;
        StartCoroutine("rotateZAnimation", targetZAngle);

        _unlerpedZRot -= ANGLE_STEP;

        if(_unlerpedZRot / ANGLE_STEP % 2 != 0)
        {
            StartCoroutine("heightAnimation", heightDifferece);
        }
        else
        {
            StartCoroutine("heightAnimation", 0);
        }

        switch (rotation)
        {
            case 0:
                StartCoroutine("rotateDirectionAnimation", 0);
                break;
            case 1:
                StartCoroutine("rotateDirectionAnimation", 90);
                break;
            case 2:
                StartCoroutine("rotateDirectionAnimation", 180);
                break;
            case 3:
                StartCoroutine("rotateDirectionAnimation", 270);
                break;
            default:
                Debug.LogWarning("Setting Mite Rotation out of bounds! (should be from 0 to 3)", gameObject.transform);
                StartCoroutine("rotateDirectionAnimation", 0);
                break;
        }

        
    }

    IEnumerator rotateZAnimation( float target)
    {
        while (_currentZRot > target)
        {
            if(_currentZRot > target + animationRoundingStep)
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
        bool turnLeft = false;

        if(target <= _currentYRot - ANGLE_STEP) turnLeft = true;

        if(turnLeft)
        {
            while (_currentYRot > target)
            {
                if(_currentYRot > target + animationRoundingDirection)
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
        else
        {
            while (_currentYRot < target)
            {
                if(_currentYRot < target - animationRoundingDirection)
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
        
    }

    IEnumerator heightAnimation(float target)
    {
        while (_currentYHeight != target)
        {
            if(_currentYHeight < target - animationRoundingHeight || _currentYHeight > target + animationRoundingHeight)
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
