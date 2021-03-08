using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public GameObject numberPalette;
    public GameObject chooseButton;
    public boardNumberChoosing Board_2;
    public boardNumberChoosing Board_4;
    public boardNumberChoosing Board_6;
    public boardNumberChoosing Board_8;

    public GameObject FeldPalette;
    public GameObject leeresBrett;
    public GameObject testButton;
    public GameObject StartScreen;
    public GameObject howManyBoards;
    public GameObject outOfService;
    public GameObject Win;
    public GameObject fireLoss;
    public GameObject dropLoss;
    public GameObject tryAgain;
    public GameObject newMap;
    public GameObject bedingung;
    public GameObject closeTip;

    public feldPalette feldPalette;


    private void Awake()
    {
    }
    public void Start()
    {
        numberPalette.SetActive(false);
        FeldPalette.SetActive(false);
        stateMachine.ChangeState(new StartState(this, StartScreen));
        testButton.SetActive(false);
    }

    public void Update()
    {
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }    
    }
}