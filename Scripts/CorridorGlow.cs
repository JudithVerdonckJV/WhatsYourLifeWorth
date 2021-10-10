using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGlow : MonoBehaviour
{
    private SpriteRenderer sprite = null;
    [SerializeField] private float period = 1f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float midline = 0.5f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Color temp = sprite.color;
        temp.a = amplitude * Mathf.Sin(Time.time * period) + midline;
        sprite.color = temp;
    }
}
