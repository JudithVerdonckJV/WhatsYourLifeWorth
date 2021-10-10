using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSkeletonShootingBehaviour : BaseShootingBehaviour
{
    static private Transform playerTarget = null;
    private EnemyAI enemyAI = null;

    protected override void Awake()
    {
        base.Awake();
        enemyAI = GetComponent<EnemyAI>();
    }

    protected override void Start()
    {
        base.Start();
        playerTarget = GameObject.Find("Player").transform;
    }

    protected override void Update()
    {
        if (playerTarget == null) return;
        
        if (enemyAI.CanEnemyShoot())
        {
            _Target = playerTarget.position;
        
            base.Update();
            Shoot(1, 0f, false, 1, 0f, 0f);
        }
        else
        {
            _CurrentReloadTime = _MaxReloadTime;
        }
    }
}
