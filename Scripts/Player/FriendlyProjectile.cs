using UnityEngine;

public class FriendlyProjectile : BaseProjectile
{
    [SerializeField] Transform spawnPointFX = null;
    [SerializeField] AudioClip arrowImpact = null;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Health otherHealth = other.GetComponent<Health>();

            if (otherHealth == null) return;
            if (_ExplosionFX == null) return;

            otherHealth.Damage(_Damage);
            GameObject effect = Instantiate(_ExplosionFX, spawnPointFX.position, Quaternion.identity);
            effect.GetComponent<AudioSource>().clip = arrowImpact;
            effect.GetComponent<AudioSource>().Play();
            Destroy(effect, 0.4f);
            Destroy(gameObject);
        }
    }
}
