using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRotation : MonoBehaviour
{
    //Angle HAS to be smaller as 180 cause check is hardcoded for simpler math
    public float Angle = 45;
    public float _RotationSpeed = 0.25f;
    private bool _IsIncreasing = true;
    public bool ShouldRotate = true;
    private Vector3 _ForwardVector;
    private bool _touched = false;
    // Use this for initialization
    void Start ()
	{
	    _ForwardVector = transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
        Debug.Log(ShouldRotate);
	    if (!ShouldRotate)
	        return;
	    if (_touched)
	        return;
	    Vector3 rotation = transform.rotation.eulerAngles;
	    if (_IsIncreasing)
	    {
	        rotation.y += _RotationSpeed;
	        if (rotation.y < _ForwardVector.y + 180 && rotation.y > _ForwardVector.y + Angle)
	            _IsIncreasing = false;
	    }
	    else
	    {
            rotation.y -= _RotationSpeed;
            if (rotation.y > _ForwardVector.y + 180 && rotation.y < _ForwardVector.y + (360 - Angle))
                _IsIncreasing = true;
        }
	    transform.rotation = Quaternion.Euler(rotation);
	}

    public void SetShouldRotate(bool value)
    {
        if (value)
        {
            if (!ShouldRotate)
            {
                //shouldrotate gets enabled for the first time in this frame
                _ForwardVector = transform.localRotation.eulerAngles;
                Debug.Log("set forward");
            }
        }
        ShouldRotate = value;
    }

    public void SetTouched()
    {
        _touched = true;
    }
}
