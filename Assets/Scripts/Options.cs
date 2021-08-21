using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider sideSlider;
    [SerializeField] Slider difficultySlider;
    
    public float GetSide()
    {
        return  sideSlider.value ; 
    }

    private void Start()
    {
        sideSlider.value = PlayerPrefsController.GetSide();
        
       
    }


   public void SaveAndExit()
    {
        PlayerPrefsController.SetSide(sideSlider.value);
        FindObjectOfType<SceneLoader>().LoadMainMenuScene();
    }
}
