using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lossState : IState
{
    Unit owner;
    float time;
    string reasonOfLoss;

    tryMap tryMap;
    createAnother createAnother;
    List<Board> testedBoards;

    public lossState (Unit owner, List<Board> testedBoards, string reasonOfLoss = "standart") 
    { this.owner = owner; this.testedBoards = testedBoards; this.reasonOfLoss = reasonOfLoss;
        owner.lossReason.GetComponent<UnityEngine.UI.Text>().text = reasonOfLoss; }

    public void Enter()
    {
        owner.lossReason.SetActive(true);
        tryMap = owner.tryAgain.GetComponent<tryMap>();
        createAnother = owner.newMap.GetComponent<createAnother>();
        owner.closeTip.SetActive(false);
        time = 1f;

        owner.setCurrentSave(owner.boardsToSave(testedBoards));
    }

    public void Execute()
    {
        if (time >= 0) { time -= .005f; }
        if (time <= 0)
        {
            owner.lossReason.SetActive(false);

            enableButtons(true);
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
        enableButtons(false);
    }

    void enableButtons(bool enable)
    {
        owner.tryAgain.SetActive(enable);
        owner.newMap.SetActive(enable);
        owner.closeTip.SetActive(enable);
        owner.changeMapBtn.SetActive(enable);
    }
}
