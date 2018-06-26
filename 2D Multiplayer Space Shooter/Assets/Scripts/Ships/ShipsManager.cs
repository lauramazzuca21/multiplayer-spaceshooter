using UnityEngine;
using System.Collections;

public class ShipsManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] FastShipSprites;
    [SerializeField]
    private Sprite[] ResistantShipSprites;
    [SerializeField]
    private Sprite[] StrongShipSprites;
    [SerializeField]
    private GameObject[] shipsPrefabs;

	private GameObject[] instantiatedShips = null;

    private bool[] _isRespawning;

	private Ships[] _playersChoices;
	private int _activePlayers;

    // Use this for initialization
	public void InstantiateShips(int activePlayers, Ships[] choices)
    {
        instantiatedShips = new GameObject[activePlayers];
		_isRespawning = new bool[activePlayers];
		_playersChoices = choices;
		_activePlayers = activePlayers;
        

        int j = 0;

        //ogni nave deve chiamre il suo? se si come?

        foreach (Transform child in transform)
        {
			if (j < _activePlayers)
            {
				instantiatedShips[j] = Instantiate(shipsPrefabs[(int) _playersChoices[j]], child);
                SpriteRenderer spriteRenderer = instantiatedShips[j].GetComponent<SpriteRenderer>();
                
				switch ((int)_playersChoices[j])
				{
					case 0:
						spriteRenderer.sprite = FastShipSprites[j];
						break;
					case 1:
						spriteRenderer.sprite = ResistantShipSprites[j];
                        break;
					case 2:
                        spriteRenderer.sprite = StrongShipSprites[j];
                        break;
					default:
						Debug.LogError("Should never reach this point");
						break;
				}
                
                instantiatedShips[j].transform.parent = child;
				instantiatedShips[j].layer = Constants.LAYER_OFFSET + j;

				foreach (Transform shipChild in instantiatedShips[j].GetComponentsInChildren<Transform>(true))
				{
					shipChild.gameObject.layer = Constants.LAYER_OFFSET + j;
				}
				_isRespawning[j] = false;
            }

            j++;
        }
    }


    /* IEnumerator grants the possibility to wait inactively 
 * for a certain amount of time and then continues to 
 * execute the code ater the yield call. */
	protected IEnumerator WaitAndRespawn(int deadShip)
    {
		yield return new WaitForSeconds(5.0f);

		_isRespawning[deadShip] = false;
		instantiatedShips[deadShip].GetComponent<Ship>().ResetHealth();
		instantiatedShips[deadShip].GetComponent<ShipLifeHandler>().IsDead = false;
		instantiatedShips[deadShip].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

	    int i = 0;
		while (i < _activePlayers)
		{
			if (instantiatedShips[i].GetComponent<ShipLifeHandler>().IsDead && !_isRespawning[i])
			{
				instantiatedShips[i].SetActive(false);
				_isRespawning[i] = true;
				StartCoroutine(this.WaitAndRespawn(i));
			}

			i++;
		}

    }
}
