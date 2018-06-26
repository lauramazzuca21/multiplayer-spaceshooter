using UnityEngine;
using System.Collections;

public class DefaultBullet : Bullet
{
	protected static readonly float DEFAULT_SPEED = 8f;
    protected static readonly float DEFAULT_DAMAGE = 5f;
    private ScoreManager _scoreManager;


    // Use this for initialization
    protected override void Start()
	{
		this.Speed = DEFAULT_SPEED;
		this.Damage = DEFAULT_DAMAGE;
        _scoreManager = FindObjectOfType<ScoreManager>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        Movement();
    }


    //** MOVE THE BULLET **//
	protected override void Movement()
    {
        Vector3 pos = transform.position;

		Vector3 newPos = new Vector3(0, Speed * Time.deltaTime, 0);

        pos += transform.rotation * newPos;
        transform.position = pos;
        
		float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        //destroy if it reaches the borders or if the timer rings
        if (pos.y > Camera.main.orthographicSize
            || pos.y < -Camera.main.orthographicSize
            || pos.x > widthOrtho
		    || pos.x < -widthOrtho)
        {
            Destroy(gameObject);
        }
    }

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Shield")) Destroy(gameObject);

        ShipLifeHandler shipCollidedWithHandler = collision.gameObject.GetComponent<ShipLifeHandler>();
        if (shipCollidedWithHandler != null)
        {
			shipCollidedWithHandler.TakeDamage(Damage);

            if (shipCollidedWithHandler.IsDead)
            {
                _scoreManager.UpdateScore(gameObject.layer);  
            }

            Destroy(gameObject);
        }
    }

}
