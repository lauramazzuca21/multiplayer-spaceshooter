using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
	protected float lifeTimer;

	// Use this for initialization
	protected abstract void Start();

    // Update is called once per frame
	protected abstract void Update();

	protected abstract void OnTriggerEnter2D(Collider2D collision);
   
	protected void DestroyPowerUp() {Destroy(gameObject);}
}
