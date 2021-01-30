using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ASCII_Camera : MonoBehaviour
{
    public Color color = Color.white;

    [Range(0, 1)]
    public float alpha = 1.0f;

    [Range(0.5f, 10.0f)]
    public float charSize = 1.0f;

    [SerializeField] Shader shader;

    private Material _material;

    Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.color = color;
        material.SetFloat("_Alpha", alpha);
        material.SetFloat("_Scale", charSize);
        Graphics.Blit(source, destination, material);
    }

    void OnDisable()
    {
        if (_material) DestroyImmediate(_material);
    }
}