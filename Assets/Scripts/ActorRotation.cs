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
	    _ForwardVector = transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!ShouldRotate)
	        return;
	    if (_touched)
	        return;
        Vector3 targetDir = _ForwardVector;
        Quaternion Target = Quaternion.identity;
	    
        if (_IsIncreasing)
	    {
            targetDir.y += Angle;
	        if (targetDir.y > 360)
	        {
	            targetDir.y -= 360;
	        }
            Target = Quaternion.Euler(targetDir);

	        if (Quaternion.Angle(transform.rotation, Target) < 1)
	        {
	            _IsIncreasing = false;
	        }
        }
	    else
	    {
            targetDir.y -= Angle;
            if (targetDir.y < 360)
            {
                targetDir.y += 360;
            }
            Target = Quaternion.Euler(targetDir);
            if (Quaternion.Angle(transform.rotation, Target) < 1)
            {
                _IsIncreasing = true;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Target, Time.deltaTime * _RotationSpeed);
    }

    public void SetShouldRotate(bool value)
    {
        if (value)
        {
            if (!ShouldRotate)
            {
                //shouldrotate gets enabled for the first time in this frame
                _ForwardVector = transform.localRotation.eulerAngles;
                //Debug.Log("set forward");
                //Debug.Log(_ForwardVector);
            }
        }
        ShouldRotate = value;
    }

    public void SetTouched()
    {
        _touched = true;
    }
}
