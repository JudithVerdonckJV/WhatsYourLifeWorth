using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBossShootingBehaviour : BaseShootingBehaviour
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
                Shoot(5, 45f, false, 2, 0f, 0.7f);
            }
            else if (skillNr == 1)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(9, 10f, true, 3, 0f, 0.6f);
            }
            else
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(2, 20f, true, 6, -10f, 0.2f);
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
