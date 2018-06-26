using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour {

    private static readonly float DEFAULT_FIREDELAY = 0.25f;

	[SerializeField]
	private GameObject _bulletPrefab;
	[SerializeField]
	private GameObject _enhancedBulletPrefab;
	[SerializeField]
	private AudioClip _bulletSound;
	[SerializeField]
	private AudioClip _enhancedBulletSound;

	private GameObject bullet;
	private Bullet bulletFunctions;
    [SerializeField]
	private PowerupHandler _powerupHandler;

	private float _cooldownTimer;
	private float _damageDealtModifier;

	private float _enhancedShotDamage;
	private float _enhancedShotSpeed;  

	public float DamageDealtModifier
    {
        get { return this._damageDealtModifier; }
        set { this._damageDealtModifier = value; }
    }

	public float EnhancedShotDamage
    {
		get { return this._enhancedShotDamage; }
		set { this._enhancedShotDamage = value; }
    }

	public float EnhancedShotSpeed
    {
		get { return this._enhancedShotSpeed; }
		set { this._enhancedShotSpeed = value; }
    }

	// Use this for initialization
	private void Start () {
		_cooldownTimer = DEFAULT_FIREDELAY;
		//_powerupHandler = GetComponent<PowerupHandler>();
	}

	// Update is called once per frame
	private void Update () {
		_cooldownTimer -= Time.deltaTime;

		if (Input.GetButton("Fire1") && _cooldownTimer <= 0) {
			_cooldownTimer = DEFAULT_FIREDELAY;
			if (_powerupHandler.EnhancedShotStatus)
			{
				bullet = Instantiate(_enhancedBulletPrefab, transform.position, transform.rotation);
				bullet.layer = gameObject.layer;
              


				bulletFunctions = bullet.GetComponent<Bullet>();            
				//modifier so that the bullet damages the enemies its default value times the owner ship modifier

				bulletFunctions.Damage = (bulletFunctions.Damage + EnhancedShotDamage) * _damageDealtModifier;
				bulletFunctions.Speed += EnhancedShotSpeed;
				AudioSource.PlayClipAtPoint(_enhancedBulletSound, transform.position);

				_powerupHandler.EnhancedShotStatus = false;

			} 
			else 
			{
				bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
				bullet.layer = gameObject.layer;
                
                bulletFunctions = bullet.GetComponent<Bullet>();
				//modifier so that the bullet damages the enemies its default value times the owner ship modifier\

				bulletFunctions.Damage = bulletFunctions.Damage * _damageDealtModifier;
            
				AudioSource.PlayClipAtPoint(_bulletSound, transform.position);
			}

		}
	}
}
