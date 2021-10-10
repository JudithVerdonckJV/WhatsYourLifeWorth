using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float _Speed = 1f;
    [SerializeField] protected GameObject _ExplosionFX = null;
    protected int _Damage = 1;

    private float lifetime = 10f;

    protected void Awake()
    {
        Invoke("Kill", lifetime);
    }

    protected virtual void Update()
    {
        transform.position += transform.up * _Speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Kill();
        }
    }

    protected void Kill()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _Damage = damage;
    }
}
