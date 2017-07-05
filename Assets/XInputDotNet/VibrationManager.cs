using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE
    using XInputDotNetPure; // Required in C#
#endif

public class VibrationManager : MonoBehaviour
{
    #if UNITY_STANDALONE
        PlayerIndex playerIndex = PlayerIndex.One;
    #endif

    private float timer = 1;
    private float vibrationDuration = 0.3f;

    void Start()
    {
    }

    void OnDestroy()
    {
        #if UNITY_STANDALONE
            GamePad.SetVibration(playerIndex, 0, 0);
        #endif
    }
    // Update is called once per frame
    void Update () {
        #if UNITY_STANDALONE
            timer += Time.deltaTime;
            if (timer > vibrationDuration)
            {
                GamePad.SetVibration(playerIndex, 0, 0);
            }
            else
            {
                GamePad.SetVibration(playerIndex, 1, 1);
            }
        #endif
    }
    public void vibrate()
    {
        #if UNITY_STANDALONE
            //GamePad.SetVibration(playerIndex, 1, 1);
            timer = 0;
        #endif
    }
}
