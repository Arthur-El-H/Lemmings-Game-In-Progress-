using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lossState : IState
{
    Unit owner;
    bool fire;
    float f;

    tryMap tryMap;
    createAnother createAnother;
    List<Board> testedBoards;

    GameObject variableLoss;

    public lossState (Unit owner, bool fire, List<Board> testedBoards) { this.owner = owner; this.fire = fire; this.testedBoards = testedBoards; }

    public void Enter()
    {
        if (fire) { owner.fireLoss.SetActive(true); variableLoss = owner.fireLoss; }
        else { owner.dropLoss.SetActive(true); variableLoss = owner.dropLoss; }
        f = 1f;

        tryMap = owner.tryAgain.GetComponent<tryMap>();
        createAnother = owner.newMap.GetComponent<createAnother>();
        owner.closeTip.SetActive(false);

    }

    public void Execute()
    {
        f = f - .005f;
        if (f > 0) { variableLoss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, f); }
        else
        {
            owner.fireLoss.SetActive(false);
            owner.dropLoss.SetActive(false); 
            owner.tryAgain.SetActive(true);
            owner.newMap.SetActive(true);
            owner.closeTip.SetActive(true);
        }

        if (tryMap.tryMapAgain)
        {
            owner.stateMachine.ChangeState(new TestingState(owner, testedBoards));
        }

        if (createAnother.createAnotherMap)
        {
            owner.stateMachine.ChangeState(new StartState(owner, owner.StartScreen));
        }

    }

    public void Exit()
    {
        tryMap.tryMapAgain = false;
        createAnother.createAnotherMap = false;

        owner.tryAgain.SetActive(false);
        owner.newMap.SetActive(false);

    }
}
