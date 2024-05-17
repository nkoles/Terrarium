using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherMatScript : MonoBehaviour
{
    [Range (0f, 1f)]public float transparencyMinumum;
    public float fadeSpeed;
    public bool isFaded;

    private Material _material;
    void Start()
    {
        _material = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFaded)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }

    void FadeOut()
    {
        float alpha = _material.color.a; 

        if(_material.color.a > transparencyMinumum)
        {
            alpha = Mathf.Lerp(_material.color.a, transparencyMinumum, fadeSpeed);
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, alpha);
    }

    void FadeIn()
    {
        float alpha = _material.color.a; 

        if(_material.color.a < 1f)
        {
            alpha = Mathf.Lerp(_material.color.a, 1, fadeSpeed);
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, alpha);
    }
}
