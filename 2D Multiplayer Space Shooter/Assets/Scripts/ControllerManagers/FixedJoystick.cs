﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]
    

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam;
	[SerializeField]
	private Text _playerIDText;

	void Start()
    {
		cam = GetComponent<Camera>();
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
		//_playerIDText.text = FindObjectOfType<NetworkClientUI>().playerID.ToString();
		_playerIDText.gameObject.SetActive(false);
    }

	//IDragHandler - OnDrag - Called on the drag object when a drag is happening
    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

		NetworkClientUI.SendControllerInfo(this.Horizontal, this.Vertical);
    }

	//IPointerDownHandler - OnPointerDown - Called when a pointer is pressed on the object
	public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

	//IPointerUpHandler - OnPointerUp - Called when a pointer is released (called on the GameObject that the pointer is clicking)
    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}