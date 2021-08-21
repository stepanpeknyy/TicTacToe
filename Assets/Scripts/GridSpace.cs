using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public Sprite exTarget;
    public Sprite circleTarget;
    public GameObject image;

    private GameController gameController;
    public void SetSpace()
    {
        if (gameController.GetComputerMove())
        {
            return;
        }
        else
        {
            image.SetActive(true);
            
            if (gameController.GetPlayerSide() == "X")
            {
                image.GetComponent<Image>().sprite = exTarget;
            }
            else if (gameController.GetPlayerSide() == "O")
            {
                image.GetComponent<Image>().sprite = circleTarget;
            }
            else
            {
                Debug.LogError ("Wrong side!");    
            }
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn();

        }
 
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }


 
}
