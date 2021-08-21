using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    float mode = 0;
    public void SinglePlayer()
    {
        PlayerPrefsController.SetMode(0);
        FindObjectOfType<SceneLoader>().LoadPlayScene();
    }

    public void MultiPlayer()
    {
        PlayerPrefsController.SetMode(1);
        FindObjectOfType<SceneLoader>().LoadPlayScene();
    }
    private void Start()
    {
        mode = PlayerPrefsController.GetMode();
        


    }
}
