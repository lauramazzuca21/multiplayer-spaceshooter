
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLifeHandler : MonoBehaviour
{
    //in physics 2D Invulnerable layer is set to not collide with anything
    private static readonly int INVULN_LAYER = 12;
    private static readonly float INVULN_TIMER = 0.5f;

    [SerializeField]
    private int _correctLayer;
    private float _invulnTimer;
    private bool _isDead;
    private float _health;

    //per ora lo mettiamo commentato, poi vediamo se serve
    //private Animator _deathAnimation;
 

    public float Health
    {
        get { return this._health; }
        set { this._health = value; }
    }

    public bool IsDead
    {
        get { return this._isDead; }
		set { this._isDead = value; }
    }


    private void Start()
    {
        _isDead = false;
        _invulnTimer = 0;
    
        _correctLayer = gameObject.layer;

        //per ora lo mettiamo commentato, poi vediamo se serve
        //_deathAnimation = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Health <= 0) Die(); //_deathAnimation.SetBool("ShipIsDead", true);

        
        if (this.gameObject.layer == INVULN_LAYER)
        {
            _invulnTimer -= Time.deltaTime;

            if (_invulnTimer <= 0) gameObject.layer = this._correctLayer;
        } 
        
    }

    public void Die()
    {
        _isDead = true;
    }


    public void TakeDamage(float damageTaken)
    {
        Health -= damageTaken;
        gameObject.layer = INVULN_LAYER;
        _invulnTimer = INVULN_TIMER;

    }

}
