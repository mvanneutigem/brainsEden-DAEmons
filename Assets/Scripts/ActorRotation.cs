using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRotation : MonoBehaviour
{
    //Angle HAS to be smaller as 180 cause check is hardcoded for simpler math
    public float Angle = 45;
    public float _RotationSpeed = 0.25f;
    private bool _IsIncreasing = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 rotation = transform.rotation.eulerAngles;
	    if (_IsIncreasing)
	    {
	        rotation.y += _RotationSpeed;
	        if (rotation.y < 180 && rotation.y > Angle)
	            _IsIncreasing = false;
	    }
	    else
	    {
            rotation.y -= _RotationSpeed;
            if (rotation.y > 180 && rotation.y < 360 - Angle)
                _IsIncreasing = true;
        }
	    transform.rotation = Quaternion.Euler(rotation);
	}
}
