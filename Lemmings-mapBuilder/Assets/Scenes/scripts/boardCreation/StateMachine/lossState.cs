using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lossState : IState
{
    Unit owner;
    float time;
    string reasonOfLoss;

    List<Board> testedBoards;

    public lossState (Unit owner, List<Board> testedBoards, string reasonOfLoss = "standart") 
    { this.owner = owner; this.testedBoards = testedBoards;
        owner.lossReason.GetComponent<UnityEngine.UI.Text>().text = reasonOfLoss; }

    public void Enter()
    {
        owner.lossReason.SetActive(true);
        owner.closeTip.SetActive(false);
        time = 1f;

        if (!owner.MoveManager.getIsPlaying()) { owner.setCurrentSave(owner.boardsToSave(testedBoards)); }
        owner.infoGatherer.sendState(oldest_state.Loss);
    }

    public void Execute()
    {
        if (time >= 0) { time -= .005f; }
        if (time <= 0)
        {
            owner.lossReason.SetActive(false);

            enableButtons(true);
        }
    }

    public void Exit()
    {
        enableButtons(false);
        owner.MoveManager.setIsPlaying(false);
    }

    void enableButtons(bool enable)
    {
        if (!owner.MoveManager.getIsPlaying())
        {
            owner.changeMapBtn.SetActive(enable);
        }
        owner.closeTip.SetActive(enable);
        owner.backToStartBtn.SetActive(enable);
        owner.tryMapAgainBtn.SetActive(enable);
    }
}
