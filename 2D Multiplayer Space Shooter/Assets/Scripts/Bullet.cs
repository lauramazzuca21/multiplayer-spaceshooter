//Abstract class to define what a generic bullet should implement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

	private float _speed;
    private float _damage;

	public float Speed
    {
		get { return this._speed; }
		set { this._speed = value; }
    }

    public float Damage
    {
		get { return this._damage; }
		set { this._damage = value; }
    }

	// Use this for initialization
	protected abstract void Start();
	
	// Update is called once per frame
	protected abstract void Update();


	//** MOVE THE BULLET **//
	protected abstract void Movement();

    //COLLISION HANDLER
	protected abstract void OnTriggerEnter2D(Collider2D collision);
}
