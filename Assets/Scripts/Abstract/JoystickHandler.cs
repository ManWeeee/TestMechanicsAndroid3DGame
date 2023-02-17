using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickArea;
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;

    private Vector2 _joystickBackgroundStartPosition; // Field so that the joystick came into start position after it's draged down

    protected Vector2 _inputVector;

    [SerializeField] private Color _activeJoystickColor;
    [SerializeField] private Color _inActiveJoystickColor;

    private bool _joystickIsActive = false;

    private void Start()
    {
        ClickEffect();

        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition; // Remembering starting position of joystick background to return, while we draged up
    }

    private void ClickEffect() // Method that makes animation of joystick
    {
        if (!_joystickIsActive)
        {
            _joystick.color = _activeJoystickColor;
            _joystickIsActive = true;
        }
        else
        {
            _joystick.color = _inActiveJoystickColor;
            _joystickIsActive = false;
        }
    }

    public void OnDrag(PointerEventData eventData) // Method only works when we are draging joystick on screen
    {
        Vector2 joystickPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out joystickPosition)) // Method is taking center of background as a world coordinates, than place where we touched the screen, camera is null as we are using canvas that is Screen size - Overlay, and generate local coordinates from the center of first parameter
        {

            joystickPosition.x = (joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x); // Taking X position inside background of joystick

            joystickPosition.y = (joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y); // Taking Y position inside background of joystick

            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector; // Used because we need to move with a different speed according to how far joystick is draged

            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2)); // Seting the joystick center inside new coordinates
        }
    }

    public void OnPointerDown(PointerEventData eventData) // Method is only working when we pressed the screen(once)
    {
        ClickEffect(); // Changing joystick view

        Vector2 joystickBackgroundPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out joystickBackgroundPosition)) //CHecking whether we touched the Joystick Area
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);// seting new center of joystick backgound
        }
    }

    public void OnPointerUp(PointerEventData eventData) // Method only works when we untouch the screen
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition; // Changing position of the joystick in to it's default position

        ClickEffect(); // Changing Joystick View

        _inputVector = Vector2.zero; // Changing direction vector to zero
        _joystick.rectTransform.anchoredPosition = Vector2.zero; // Changing joystick center to default center
    }
}
