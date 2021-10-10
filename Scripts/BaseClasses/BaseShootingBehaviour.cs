using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShootingBehaviour : MonoBehaviour
{
    protected Vector2 _Target;
    private Vector2 shootDir;
    private Vector2 socketPosition;

    private Animator animator = null;
    [SerializeField] private GameObject projectile = null;
    private Transform fireSocketTransform = null;
    private Transform shootingRootTransform = null;

    [SerializeField] protected float _MaxReloadTime = 1f;
    protected float _CurrentReloadTime = 0f;

    private int frameCounter = 0;
    private bool attacking = false;

    [SerializeField] private int damage = 1;
    
    protected AudioSource _AudioSource = null;

    protected virtual void Awake()
    {
        _CurrentReloadTime = _MaxReloadTime;
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        shootingRootTransform = gameObject.transform.Find("ShootingRoot").transform;
        fireSocketTransform = shootingRootTransform.gameObject.transform.Find("ShootingSocket");
    }

    protected virtual void Update()
    {
        if (frameCounter >= 30)
        {
            attacking = false;
            frameCounter = 0;
        }
        if (attacking) frameCounter++;

        _CurrentReloadTime += Time.deltaTime;
        RotateTowardsTarget();
    }

    private void LateUpdate()
    {
        if (animator == null) return;
        animator.SetFloat("TargetHorizontal", _Target.x - transform.position.x);
        animator.SetFloat("TargetVertical", _Target.y - transform.position.y);
        animator.SetBool("IsAttacking", attacking);
    }

    private Vector3 rootToSocket;
    private Vector3 toLeft;
    private Vector3 toRight;
    private Quaternion rotationLeft;
    private Quaternion rotationRight;
    private float delay = 0.2f;

    public void Shoot(int nrOfArrows, float angle, bool shootInCone, int waves, float waveAngleDiff, float waveDelay, float newReloadTime = -1)
    {
        if (_CurrentReloadTime >= _MaxReloadTime)
        {
            if (_AudioSource != null) _AudioSource.Play();
            
            if (newReloadTime > 0) _MaxReloadTime = newReloadTime;
            delay = waveDelay;
            rootToSocket = fireSocketTransform.position - shootingRootTransform.position;
            if (!shootInCone)
            {
                rotationLeft = fireSocketTransform.rotation;
                rotationRight = fireSocketTransform.rotation;
            }

            if (nrOfArrows % 2 == 0)
            {
                int arrowsOnSide = nrOfArrows / 2;
                for (int wave = 1; wave <= waves; wave++)
                {
                    for (int i = 0; i < arrowsOnSide; i++)
                    {
                        toLeft = Quaternion.AngleAxis(-angle * i - angle / 2 + (wave - 1) * waveAngleDiff, Vector3.forward) * rootToSocket;
                        toRight = Quaternion.AngleAxis(angle * i + angle / 2 + (wave - 1) * waveAngleDiff, Vector3.forward) * rootToSocket;

                        if (shootInCone)
                        {
                            rotationLeft = Quaternion.LookRotation(Vector3.forward, toLeft);
                            rotationRight = Quaternion.LookRotation(Vector3.forward, toRight);
                        }

                        StartCoroutine(SpawnProjectile(toLeft, rotationLeft, wave * delay));
                        StartCoroutine(SpawnProjectile(toRight, rotationRight, wave * delay));
                    }
                }
            }
            else
            {
                int arrowsOnSide = nrOfArrows / 2;
                for (int wave = 1; wave <= waves; wave++)
                {
                    Vector3 straight = Quaternion.AngleAxis((wave - 1) * waveAngleDiff, Vector3.forward) * rootToSocket;
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, straight);
                    StartCoroutine(SpawnProjectile(rootToSocket, rotation, wave * delay)); // straight ahead

                    for (int i = 1; i <= arrowsOnSide; i++)
                    {
                        toLeft = Quaternion.AngleAxis(-angle * i + (wave - 1) * waveAngleDiff, Vector3.forward) * rootToSocket;
                        toRight = Quaternion.AngleAxis(angle * i + (wave - 1) * waveAngleDiff, Vector3.forward) * rootToSocket;

                        if (shootInCone)
                        {
                            rotationLeft = Quaternion.LookRotation(Vector3.forward, toLeft);
                            rotationRight = Quaternion.LookRotation(Vector3.forward, toRight);
                        }

                        StartCoroutine(SpawnProjectile(toLeft, rotationLeft, wave * delay));
                        StartCoroutine(SpawnProjectile(toRight, rotationRight, wave * delay));
                    }
                }
            }

            _CurrentReloadTime = 0f;
            attacking = true;
        }
    }

    private void RotateTowardsTarget()
    {
        if (socketPosition == null || shootingRootTransform == null)
        {
            Debug.Log("Shooting system transform(s) missing!");
            return;
        }
        
        socketPosition.x = shootingRootTransform.position.x;
        socketPosition.y = shootingRootTransform.position.y;
        
        shootDir = _Target - socketPosition;
        shootingRootTransform.localRotation = Quaternion.LookRotation(Vector3.forward, shootDir);
    }

    IEnumerator SpawnProjectile(Vector2 position, Quaternion rotation, float delay)
    {
        if (projectile == null) yield break;

        yield return new WaitForSeconds(delay);

        Vector3 endPos = position;
        GameObject newProjectile = Instantiate(projectile, shootingRootTransform.position + endPos, rotation);
        newProjectile.GetComponent<BaseProjectile>().SetDamage(damage);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
