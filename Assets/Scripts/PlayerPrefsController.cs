using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string SIDE = "side";
    const float MAX_SIDE = 1f;
    const float MIN_SIDE = 0f;

    const string SINGLE_PLAYER= "singlePlayer";

    public static void SetSide(float side)
    {
        if (side >= MIN_SIDE  && side  <= MAX_SIDE )
        {
            PlayerPrefs.SetFloat(SIDE, side);
        }
        else
        {
            Debug.LogError("Side setting is out of range");
        }
    }

    public static float GetSide()
    {
        return PlayerPrefs.GetFloat(SIDE);
    }

    public static void SetMode(float singlePlayer)
    {
        PlayerPrefs.SetFloat(SINGLE_PLAYER, singlePlayer);
    }
    public static float GetMode()
    {
        return PlayerPrefs.GetFloat(SINGLE_PLAYER);
    }

}
