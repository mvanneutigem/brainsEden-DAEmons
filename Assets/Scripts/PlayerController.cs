using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private CharacterController _characterController;
    private const float SPEED = 8;
    private const float ROTSPEED = 0.5f;
    private Sneeze _sneeze;

    void Start ()
    {
        _characterController = this.GetComponent<CharacterController>();
        _sneeze = GetComponentInChildren<Sneeze>();
    }
	
	void Update ()
    {
        if (GameManager.IsPlayerSneezing || GameManager.Paused) return;
        
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        var moveVector = Vector3.forward * vInput + Vector3.right * hInput;

        moveVector = SPEED * moveVector;

        _characterController.Move(moveVector * Time.deltaTime);

        if (moveVector != Vector3.zero)
        {

            Quaternion targetRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ROTSPEED);

            GetComponent<Wiggle>().WiggleMethod();
        }

        bool sneezeDown = Input.GetButtonDown("Sneeze");
        if (sneezeDown && !GameManager.ResumedThisFrame)
        {
            _sneeze.Play();
        }

    }

}
