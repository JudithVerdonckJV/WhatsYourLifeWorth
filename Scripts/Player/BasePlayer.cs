using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasePlayer : BaseCharacter
{
    protected PlayerInteractBehaviour _InteractBehaviour = null;

    private Vector2 movement;
    private int levelsGained = 0;
    private bool fire1ButtonDown = false;
    private bool fire2ButtonDown = false;
    private float fireRate = 1f;

    public int level
    {
        get;
        set;
    }

    public int currentGridIndex
    {
        get;
        set;
    }

    public int tries
    {
        get;
        set;
    }

    public bool bowActive
    {
        get;
        set;
    }

    public bool bowPickedUp
    {
        get;
        set;
    }

    protected override void Awake()
    {
        base.Awake();
        _InteractBehaviour = GetComponent<PlayerInteractBehaviour>();
        currentGridIndex = -1;
        tries = 3;
        bowActive = false;
    }

    private void Update()
    {
        HandleInput();

        if (bowPickedUp && bowActive && fire1ButtonDown && currentGridIndex != -1) _ShootingBehaviour.Shoot(1, 0f, false, 1, 0f, 0f, fireRate);
        if (bowPickedUp && bowActive && fire2ButtonDown && level >= 3 && currentGridIndex != -1) _ShootingBehaviour.Shoot(3, 5f, true, 1, 0f, 0f, 1f);
    }
    
    void HandleInput()
    {
        if (_BaseMovement == null) return;
        if (_ShootingBehaviour == null) return;
        if (_InteractBehaviour == null) return;

        //movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        _BaseMovement.SetMoveDirection(movement);

        //attack input
        if (Input.GetButtonDown("Fire1")) fire1ButtonDown = true;
        if (Input.GetButtonDown("Fire2")) fire2ButtonDown = true;
        if (Input.GetButtonUp("Fire1")) fire1ButtonDown = false;
        if (Input.GetButtonUp("Fire2")) fire2ButtonDown = false;

        //interact input
        if (Input.GetButtonDown("Interact")) _InteractBehaviour.Interact();
    }

    public void LevelUp()
    {
        level++;
        levelsGained++;
        SetStats();
    }

    private void SetStats()
    {
        // player can loose upgrades when dying and having to redo the room (not the full game)
        // make sure to be able to both level up and down
        switch (level)
        {
            case 1:
                fireRate = 1f;
                _ShootingBehaviour.SetDamage(1);
                break;
            case 2:
                fireRate = 0.6f;
                _ShootingBehaviour.SetDamage(1);
                break;
            //case 3: new skill
            case 4:
                fireRate = 0.6f;
                _ShootingBehaviour.SetDamage(2);
                break;
        }
    }

    // When player comes out of main menu (full reset)
    public void ResetPlayer()
    {
        GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
        GetComponent<Health>().SetToMax();
        currentGridIndex = -1;
        levelsGained = 0;
        fire1ButtonDown = false;
        level = 1;
        SetStats();
        tries = 3;
        bowActive = false;
    }

    // When player died, but doesn't need to fully restart yet
    public void PlayerRetry()
    {
        tries--;
        GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
        currentGridIndex = -1;
        level -= levelsGained;
        levelsGained = 0;
        SetStats();
    }

    // When the player changes level after completing the rooms
    public void PlayerNextLevel()
    {
        GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
        currentGridIndex = -1;
        levelsGained = 0;
    }
}
