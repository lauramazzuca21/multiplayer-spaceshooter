// Controller for PowerUps spawning every tot seconds

using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{  
	//private static readonly int ENHANCED_SHOT = 1;
	//private static readonly int SPEED_BOOST = 2;
	//private static readonly int SHIELD = 3;
	//private static readonly int MEDIKIT = 4;

	private float yBoundaries;
	private float xBoundaries;
	private float SPAWN_TIMER_VALUE = 5f;
	private float timer;
	private Vector3 randomSpawn;

	[SerializeField]
	private GameObject _speedBoostPrefab;
	[SerializeField]
    private GameObject _enhancedShotPrefab;
	[SerializeField]
    private GameObject _shieldPrefab;

    // Use this for initialization
    void Start()
    {
		timer = SPAWN_TIMER_VALUE;
		randomSpawn = new Vector3(0, 0, 0);
    } 

    // Update is called once per frame
    void Update()
    {
		timer -= Time.deltaTime;
        
        if (timer <= 0)
		{
			yBoundaries = Camera.main.orthographicSize - 1f;
			xBoundaries = (Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height) ) - 1f;

			randomSpawn.y = Random.Range(-yBoundaries, yBoundaries);
			randomSpawn.x = Random.Range(-xBoundaries, xBoundaries);
           
			switch (Random.Range(0, 10) % 4) 
			{
				//case ENHANCED_SHOT:
				case 0:
					Instantiate(_enhancedShotPrefab, randomSpawn, new Quaternion(0, 0, 0, 0));
					break;
				//case SPEED_BOOST:
				case 1:
					Instantiate(_speedBoostPrefab, randomSpawn, new Quaternion(0, 0, 0, 0));
					break;
				//case SHIELD:
				case 2:
					Instantiate(_shieldPrefab, randomSpawn, new Quaternion(0, 0, 0, 0));
                    break;
				//case MEDIKIT:
				case 3:
					Instantiate(_shieldPrefab, randomSpawn, new Quaternion(0, 0, 0, 0));
                    break;
				default:
					Debug.Log("Should never get here");
					break;
			}
			timer = SPAWN_TIMER_VALUE;
		}
    }
}
