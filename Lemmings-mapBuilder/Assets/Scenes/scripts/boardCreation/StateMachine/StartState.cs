using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    Unit owner;
    public float f;

    public StartState(Unit owner, GameObject StartScreen) { this.owner = owner; this.StartScreen = StartScreen; }

    GameObject StartScreen;


    public void Enter()
    {
        owner.fillScrollView();
        StartScreen.SetActive(true);
        f = 1f;
    }

    public void Execute()
    {
        f = f - .01f;
        if (f > 0) { StartScreen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, f); }
        else
        {
            owner.stateMachine.ChangeState(new Startscreen(owner));
        }
    }

    public void Exit()
    {
        StartScreen.SetActive(false);
    }
}
