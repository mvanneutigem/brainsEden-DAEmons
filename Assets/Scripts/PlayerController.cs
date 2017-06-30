﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private CharacterController _characterController;
    private const float SPEED = 5;
    private const float ROTSPEED = 0.5f;

    void Start ()
    {
        _characterController = this.GetComponent<CharacterController>();
    }
	
	void Update ()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        var moveVector = Vector3.forward * vInput + Vector3.right * hInput;

        moveVector = SPEED * moveVector;

        _characterController.Move(moveVector * Time.deltaTime);

        if (moveVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ROTSPEED);
        }
    }
}