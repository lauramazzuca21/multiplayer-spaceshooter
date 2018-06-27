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
	private float delayInControls;
    

	private void Start()
	{
		currentIndex = 0;
		delayInControls = Constants.DELTA_TIME_JOYSTICK;
	}


	public void UpdateUIOnConnection(float horizontalAxis)
	{
		delayInControls -= Time.deltaTime;

		if ((horizontalAxis > Constants.DELTA_JOYSTICK || horizontalAxis < -Constants.DELTA_JOYSTICK)
		    && delayInControls <= 0)
		{
			currentIndex = currentIndex == 1 ? 0 : 1;
			UIObjects[currentIndex].GetComponent<Button>().Select();
			delayInControls = Constants.DELTA_TIME_JOYSTICK;
		}
	}

	public void UpdateUIOnSetup(float horizontalAxis, float verticalAxis) 
	{
		delayInControls -= Time.deltaTime;

		if (Mathf.Abs(horizontalAxis) < Mathf.Abs(verticalAxis)
		    && verticalAxis > Constants.DELTA_JOYSTICK 
		    && delayInControls <= 0)
		{
			if (currentIndex == 0)
				currentIndex = UIObjects.Length - 1;
			else
				currentIndex--;
			
			Debug.Log("v>0 currentIndex " + currentIndex);


			Selectable selecting = FindSelectable(UIObjects[currentIndex]);
			selecting.Select();

			delayInControls = Constants.DELTA_TIME_JOYSTICK;
		}
		else if (Mathf.Abs(horizontalAxis) < Mathf.Abs(verticalAxis) 
		         && verticalAxis < -Constants.DELTA_JOYSTICK
		        && delayInControls <= 0)
		{
			if (currentIndex == UIObjects.Length - 1)
				currentIndex = 0;
			else
				currentIndex++;
			Debug.Log("v<0 currentIndex " + currentIndex);
			Selectable selecting = FindSelectable(UIObjects[currentIndex]);
            selecting.Select();
			delayInControls = Constants.DELTA_TIME_JOYSTICK;
		}      

		Slider currentObjectSelected = UIObjects[currentIndex].GetComponent<Slider>();
		if (currentObjectSelected != null)
		{
			if (Mathf.Abs(horizontalAxis) > Mathf.Abs(verticalAxis) && horizontalAxis > Constants.DELTA_JOYSTICK) currentObjectSelected.value++;
			else if (Mathf.Abs(horizontalAxis) > Mathf.Abs(verticalAxis) && horizontalAxis < -Constants.DELTA_JOYSTICK) currentObjectSelected.value--;

		}
	}

	public void Click(bool fireButton)
	{
		if (fireButton)
        {
			if(UIObjects[currentIndex].GetComponent<Button>()) UIObjects[currentIndex].GetComponent<Button>().onClick.Invoke();

			if (UIObjects[currentIndex].GetComponent<Toggle>().isOn) UIObjects[currentIndex].GetComponent<Toggle>().isOn = false;
			else UIObjects[currentIndex].GetComponent<Toggle>().isOn = true;
        }
	}

	private Selectable FindSelectable(GameObject gameObject) {
		
		Selectable selectable = gameObject.GetComponent<Selectable>();

		if (selectable != null) return selectable;
		else
		{
			foreach (Transform child in gameObject.transform)
            {
				selectable = FindSelectable(child.gameObject);
				if (selectable != null) return selectable;
            }
		}

		return null;
	}
}
