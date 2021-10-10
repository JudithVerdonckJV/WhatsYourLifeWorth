using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerShootingBehaviour : BaseShootingBehaviour
{

    protected override void Awake()
    {
        base.Awake();
        _AudioSource = GetComponent<AudioSource>();
    }

    public void SetFireRate(float amount)
    {
        _MaxReloadTime = amount;
    }
    
    protected override void Update()
    {
        _Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        base.Update();        
    }
}
