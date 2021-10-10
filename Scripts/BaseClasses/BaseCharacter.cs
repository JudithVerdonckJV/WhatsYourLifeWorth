using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected PlayerShootingBehaviour _ShootingBehaviour;
    protected BaseMovement _BaseMovement;

    protected virtual void Awake()
    {
        _ShootingBehaviour = GetComponent<PlayerShootingBehaviour>();
        _BaseMovement = GetComponent<BaseMovement>();
    }
}
