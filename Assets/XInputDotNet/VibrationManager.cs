using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class VibrationManager : MonoBehaviour
{
    PlayerIndex playerIndex = PlayerIndex.One;
    private float timer = 0;
    
    void OnDestroy()
    {
        GamePad.SetVibration(playerIndex, 0, 0);
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
    }
    public void vibrate()
    {
        GamePad.SetVibration(playerIndex, 1, 1);
        timer = 0;
    }
}
