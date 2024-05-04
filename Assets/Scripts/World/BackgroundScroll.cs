using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float offset;
    private Material _material;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        _material = renderer.material;
    }

    private void Update()
    {
        offset -= scrollSpeed * Time.deltaTime;

        _material.SetFloat("_yOffset", offset);
    }
}
