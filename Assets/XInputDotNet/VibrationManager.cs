using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class VibrationManager : MonoBehaviour
{
    PlayerIndex playerIndex = PlayerIndex.One;
    private float timer = 1;
    private float vibrationDuration = 0.3f;

    void Start()
    {
    }

    void OnDestroy()
    {
        GamePad.SetVibration(playerIndex, 0, 0);
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer > vibrationDuration)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else
        {
            GamePad.SetVibration(playerIndex, 1, 1);
        }

    }
    public void vibrate()
    {
        //GamePad.SetVibration(playerIndex, 1, 1);
        timer = 0;
    }
}
