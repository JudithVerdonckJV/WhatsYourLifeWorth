using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkeletonShootingBehaviour : BaseShootingBehaviour
{
    private Transform playerTarget = null;
    private int skillNr = 0;
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

            if (skillNr == 0)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(12, 30f, true, 4, 15f, 0.1f);
            }
            else if (skillNr > 0)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(2, 180f, true, 2, 90f, 0.1f);
            }
            skillNr %= 3;

            base.Update();
        }
        else
        {
            _CurrentReloadTime = _MaxReloadTime;
        }
    }
}
