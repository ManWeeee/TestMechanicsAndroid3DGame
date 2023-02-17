using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : JoystickHandler
{
    [SerializeField] 
    private PlayerController _playerController;

    private void Update()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            _playerController.MoveCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
            _playerController.RotateCharacter(new Vector3(_inputVector.x, 0, _inputVector.y));
        }
        else {
            _playerController.MoveCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _playerController.RotateCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        }
        
    }
}
