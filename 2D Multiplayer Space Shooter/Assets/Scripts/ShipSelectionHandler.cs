using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShipSelectionHandler : MonoBehaviour, ISelectHandler
{
	[SerializeField]
	private Ships ID;

	public void OnSelect(BaseEventData eventData)
	{
		NetworkClientUI.ShipChosen = ID;
	}
}
