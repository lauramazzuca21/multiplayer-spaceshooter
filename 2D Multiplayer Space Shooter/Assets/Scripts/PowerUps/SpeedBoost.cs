using UnityEngine;
using System.Collections;

public class SpeedBoost : PowerUp
{

	private const float LIFE_TIMER = 10f;
	// Use this for initialization

	override protected void Start()
    {
		this.lifeTimer = LIFE_TIMER;
    }

    // Update is called once per frame
	override protected void Update()
    {
		lifeTimer -= Time.deltaTime;

		if (lifeTimer <= 0) this.DestroyPowerUp();

        
    }
    
	override protected void OnTriggerEnter2D(Collider2D collision)
	{
		Ship shipCollided = collision.GetComponent<Ship>();
		PowerupHandler handler = shipCollided.GetComponentInChildren<PowerupHandler>();


        if (shipCollided != null && handler != null)
        {
			handler.SpeedBoostOn(2f);
            this.DestroyPowerUp();
        }
    
	}

}
