using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardZombieShootingBehaviour : BaseShootingBehaviour
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

            if (skillNr < 2)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(11, 30f, true, 1, 0f, 0f);
            }
            else if (skillNr == 2)
            {
                if (_CurrentReloadTime > _MaxReloadTime) skillNr++;
                Shoot(5, 20f, true, 2, 0f, 0.2f);
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
