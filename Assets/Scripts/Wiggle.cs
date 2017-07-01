using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    public bool ShouldWiggle = true;
    private float _elapsed = 0.0f;
    public void WiggleMethod()
    {
        if (ShouldWiggle)
        {
            _elapsed += Time.deltaTime * 10;
            transform.Rotate(Vector3.Cross(transform.localRotation.eulerAngles, Vector3.right), (float)Mathf.Sin(_elapsed) * 10);
        }
    }
}
