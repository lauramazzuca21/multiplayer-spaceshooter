using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveThroughUI : MonoBehaviour
{
	[SerializeField]
	private GameObject[] UIObjects;
	private static int currentIndex;

	private void Start()
	{
		currentIndex = 0;
	}



	public void UpdateUIOnConnection(float horizontalAxis, bool fireButton)
	{
		if (horizontalAxis != 0)
		{
			currentIndex = currentIndex == 1 ? 0 : 1;
			UIObjects[currentIndex].GetComponent<Button>().Select();
		}
        
		if(fireButton)
		{
			//UIObjects[currentIndex].GetComponent<Button>().OnClick.Invoke();
		}
	}
}
