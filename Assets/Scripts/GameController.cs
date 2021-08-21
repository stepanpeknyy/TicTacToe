using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random= UnityEngine.Random ;



public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject[] imageList;
    public string playerSide;

    [SerializeField] GameObject  gameOverCanvas;
    [SerializeField] Text gameOverText;
    [SerializeField] Button hint;
    [SerializeField] Button undo;
    
    [SerializeField] GameObject panelX;
    [SerializeField] GameObject panelO;
    [SerializeField] Text timerText;

    public Sprite exTarget;
    public Sprite circleTarget;
    public Sprite hintEx;
    public Sprite hintCircle;
    public string computerSide;
    public bool computerMove=false;
    public float delay=100f;
    int value;
    bool singlePlayerMode;
    public string[] track= new string [81];
    public GameObject[] trackImages= new GameObject [81];
    public int moveNumber=0;
    bool isGameFinished = false;
    float time;
    int computerChoice=-1;
   
    private void Awake()
    {
        SetGameControllerReferenceOnButton();
        SetMode();
        CheckForSide();
        undo.interactable = false;      
    }

    IEnumerator Start()
    {
        while(!isGameFinished)
        {
            yield return new WaitForSeconds(0.1f);
            timerText.text = "Time: " + Math.Round(time, 1).ToString() ;
        }
    }
    


    IEnumerator  ComputerMove()
    {
        hint.interactable = false;
        undo.interactable = false;
        yield return new WaitForSeconds(1 );  

        if (!isGameFinished)
        {
            while (buttonList[value].GetComponentInParent<Button>().interactable == false || computerChoice ==value )
            {
                value = Random.Range(0, buttonList.Length);
            }
        }
        computerChoice = value;
        if (buttonList[value].GetComponentInParent<Button>().interactable == true)
        {
            buttonList[value].text = GetComputerSide();
            imageList[value].SetActive(true);
            if (GetComputerSide() == "X")
            {
                imageList[value].GetComponent<Image>().sprite  = exTarget;
            }
            else if (GetComputerSide() == "O")
            {
                imageList[value].GetComponent<Image>().sprite = circleTarget;
            }
            else
            {
                Debug.LogError("Wrong side!");
            }
            buttonList[value].GetComponentInParent<Button>().interactable = false;
            hint.interactable = true;
            undo.interactable = true;
            DisplayWhoseTurn();
            CheckWinForComputer();
            RecordMoves();
            ChangeSide();
        }
    }

    public bool GetComputerMove()
    {
        return computerMove;
    }
    private void Update()
    {
        if(!isGameFinished)
        {
            time += Time.deltaTime;
        }
    }


    public void Hint()
    {
        bool interactable = false;
        while (!interactable)
        {
            value = Random.Range(0, buttonList.Length);
            if (buttonList[value].GetComponentInParent<Button>().interactable == true)
            {               
                interactable = true;
            }
        }
        if (interactable)
        {
            hint.interactable = false;
            imageList[value].SetActive(true);
            Color newColor = new Color(0.3f, 0.3f, 0.3f, 0.7f);
            if (GetPlayerSide() == "X")
            {
                imageList[value].GetComponent<Image>().sprite = hintEx;               
            }
            else if (GetPlayerSide() == "O")
            {
                imageList[value].GetComponent<Image>().sprite = hintCircle;

            }
            else
            {
                Debug.LogError("Wrong side!");
            }
            imageList[value].GetComponent<Image>().color = newColor;

        }
    }



    private void CheckForSide()
    {
        
        if (singlePlayerMode == false)
        {
            if (PlayerPrefsController.GetSide() == 0)
            {
                playerSide = "X";
            }
            else if (PlayerPrefsController.GetSide() == 1)
            {
                playerSide = "O";              
            }
            else
            {
                Debug.LogError("side slider value is wrong");
            }
        }
        else
        {
            if (PlayerPrefsController.GetSide() == 0)
            {
                playerSide = "X";
                computerSide = "O";
            }
            else if (PlayerPrefsController.GetSide() == 1)
            {
                playerSide = "O";
                computerSide = "X";
            }
            else
            {
                Debug.LogError("side slider value is wrong");
            }
        }



    }

    private void SetMode()
    {
        if (PlayerPrefsController.GetMode() == 0)
        {
            singlePlayerMode = true;
        }
        else if (PlayerPrefsController.GetMode() == 1)
        {
            singlePlayerMode = false;
        }
    }

    private void DisplayWhoseTurn()
    {
        if (playerSide == "X")
        {
            panelX.SetActive(true);
            panelO.SetActive(false);
        }
        else
        {
            panelO.SetActive(true);
            panelX.SetActive(false);
        }
    }

    private void SetGameControllerReferenceOnButton()
    {
        for(int i=0; buttonList.Length >i; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide ;
    }

    public string GetComputerSide()
    {
        return computerSide;
    }

    public void EndTurn()
    {
        hint.interactable = true;
        undo.interactable = true;
        DisplayWhoseTurn();

        if (singlePlayerMode == false)
        {
            CheckWinForPlayer();
        }
        else
        {
            CheckWinForPlayer();
            CheckWinForComputer();

        }
        ChangeSide();
        RecordMoves();
        if (computerMove && singlePlayerMode)
        {
            StartCoroutine(ComputerMove());
        }

        NormalizeGridSpace();
    }

    private void NormalizeGridSpace()
    {
        for (int i = 0; i < imageList.Length; i++)
        {
            imageList[i].GetComponent<Image>().color = Color.white;
            if (buttonList[i].GetComponentInParent<Button>().interactable == true)
            {
                imageList [i].GetComponent<Image>().sprite = null;
                imageList[i].SetActive(false);
            }
        }
    }

    private void CheckWinForComputer()
    {
        if (buttonList[0].text == computerSide && buttonList[1].text == computerSide && buttonList[2].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[3].text == computerSide && buttonList[4].text == computerSide && buttonList[5].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[6].text == computerSide && buttonList[7].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[0].text == computerSide && buttonList[3].text == computerSide && buttonList[6].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[1].text == computerSide && buttonList[4].text == computerSide && buttonList[7].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[2].text == computerSide && buttonList[5].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver();
        }
        else if (buttonList[6].text == computerSide && buttonList[4].text == computerSide && buttonList[2].text == computerSide)
        {
            GameOver();
        }
        else
        {
           //CheckForDraw();
        }
    }

    private void CheckWinForPlayer()
    {
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[6].text == playerSide && buttonList[4].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        else
        {
            CheckForDraw();
        }
    }

    private void CheckForDraw()
    {
        int count = 0;
        for (int i = 0; buttonList.Length > i; i++)
        {
            if (buttonList[i].GetComponentInParent<Button>().interactable == false)
            {
                count++;
                if (count == buttonList.Length)
                {
                    Draw();
                }
            }

        }
    }

    private void Draw()
    {
        gameOverCanvas.SetActive(true);
        gameOverText.text =  "DRAW!";
        panelO.SetActive(true);
        panelX.SetActive(true);
        hint.interactable = false;
        undo.interactable = false;
        isGameFinished = true;
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameOverText.text = playerSide + " is WINNER!";
        for (int i =0; i< buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        panelO.SetActive(true);
        panelX.SetActive(true);
        hint.interactable = false;
        undo.interactable = false;
        isGameFinished = true;
    }

    void ChangeSide() 
    {
        if (singlePlayerMode ==false)
        {
            playerSide = (playerSide == "X") ? "O" : "X";
        }
        else
        {
            playerSide = (playerSide == "X") ? "O" : "X";
            computerMove = (computerMove == false) ? true : false;
        }

    }


    public void UndoMove()
    {

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (moveNumber == 1)
            {
                imageList[i].SetActive(false) ;    
                imageList[i].GetComponent<Image>().sprite = null;

                buttonList[i].text = "";
                buttonList[i].GetComponentInParent<Button>().interactable = true;
            }
            else
            {
                buttonList[i].text = track[i + 9 * (moveNumber - 2)];

                imageList[i] = trackImages[i + 9*(moveNumber - 2)];
                if (buttonList[i].text != track[i + 9 * (moveNumber - 1)])
                {
                    buttonList[i].GetComponentInParent<Button>().interactable = true;
                    imageList[i].SetActive(false);
                }
            }
        }



        ChangeSide();
        RecordMoves();
        moveNumber -= 2;
        if (moveNumber <= 0)
        {
            undo.interactable = false;
        }
        if (singlePlayerMode)
        {
            StartCoroutine(ComputerMove());
        }
    }
    private void RecordMoves()
    {
        int count = 0;
        for (int i = moveNumber * 9; i < buttonList.Length + moveNumber * 9; i++)
        {
            track[i] = buttonList[count].text;
            trackImages[i] = imageList[count];
            count++;
        }
        moveNumber++;
    }
}
