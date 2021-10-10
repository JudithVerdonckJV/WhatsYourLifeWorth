using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumSkeletonShootingBehaviour : BaseShootingBehaviour
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
                Shoot(3, 10f, true, 1, 0f, 0f);
            }
            else if (skillNr == 1)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(2, 180f, false, 2, 0f, 0.3f);
            }
            skillNr %= 2;

            base.Update();
        }
        else
        {
            _CurrentReloadTime = _MaxReloadTime;
        }
    }
}
