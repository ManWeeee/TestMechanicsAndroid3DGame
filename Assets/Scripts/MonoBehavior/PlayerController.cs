using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField]private float _rotationSpeed;

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        Vector3 offset = moveDirection * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + offset);
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if(Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newRotation = Vector3.RotateTowards(transform.forward, moveDirection, _rotationSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newRotation);
        }
    }
}
