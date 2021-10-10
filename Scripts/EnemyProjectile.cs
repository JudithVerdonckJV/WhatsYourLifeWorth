using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : BaseProjectile
{
    [SerializeField] Transform spawnPointFX = null;
    [SerializeField] AudioClip fireballImpact = null;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Health otherHealth = other.GetComponent<Health>();

            if (otherHealth == null)
            {
                return;
            }
            if (_ExplosionFX == null) return;

            otherHealth.Damage(_Damage);
            GameObject effect = Instantiate(_ExplosionFX, spawnPointFX.position, Quaternion.identity);
            effect.GetComponent<AudioSource>().clip = fireballImpact;
            effect.GetComponent<AudioSource>().Play();
            Destroy(effect, 0.4f);
            Destroy(gameObject);
        }
    }
}
