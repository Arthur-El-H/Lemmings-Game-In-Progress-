using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState
{
    Unit owner;

    public WinState(Unit owner) { this.owner = owner; }



    public void Enter()
    {
        owner.Win.SetActive(true);
        owner.newMap.SetActive(true);
    }

    public void Execute()
    {
        if (owner.newMap.GetComponent<createAnother>().createAnotherMap)
        {
            owner.stateMachine.ChangeState(new StartState(owner, owner.StartScreen));
        }
    }

    public void Exit()
    {
        owner.Win.SetActive(false);
        owner.newMap.SetActive(false);
    }
}