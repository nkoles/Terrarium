using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertlityShaderHandler : MonoBehaviour
{
    public float fertilityValue;
    public Shader fertilityShader;

    [Range (0.0f, 1.0f)] public float lerpSpeed;

    private Material _material;
    private bool hasShader;
    private float _currentFert;

    private float ClampFertility(float input)
    {
        float output = input;

        if (input < 0)
        {
            output = 0;

            Debug.LogWarning("Given Fertlity value is too low (< 0) for the shader to handle! Make sure to check what value is passed into fertilityValue.", gameObject.transform);
        }
        else if (input > 1)
        {
            output = 1;

            Debug.LogWarning("Given Fertlity value is too big (> 0) for the shader to handle! Make sure to check what value is passed into fertilityValue.", gameObject.transform);
        }
        

        return output;
    }

    public void Start()
    {
        _material = GetComponent<Renderer>().material;

        hasShader = false;

        if (_material.shader == fertilityShader)
        {
            hasShader = true;
        }
        else
        {
            Debug.LogWarning("Game Object is missing a material with " + fertilityShader.name + ", Component cannot work!", gameObject.transform);
        }
    }

    public void Update()
    {
        if(hasShader)
        {
            _currentFert = Mathf.Lerp(_currentFert, ClampFertility(fertilityValue), lerpSpeed);
            _material.SetFloat("_Fertility", _currentFert);
        }
    }

}
