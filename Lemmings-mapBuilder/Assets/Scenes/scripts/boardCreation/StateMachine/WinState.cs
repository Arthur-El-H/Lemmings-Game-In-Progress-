using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState, IBoardsOwner
{
    Unit owner;
    List<Board> boards = new List<Board>();

    public WinState(Unit owner, List<Board> boards) { this.owner = owner; this.boards = boards; }



    public void Enter()
    {
        owner.closeTip.SetActive(false);

        owner.setBoardsToSave(boards);
        owner.setCurrentSave(owner.boardsToSave(boards));

        enableButtons(true);
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
        enableButtons(false);
    }

    public Save getBoards()
    {
        return owner.boardsToSave(boards);
    }

    void enableButtons(bool enable)
    {
        owner.changeMapBtn.SetActive(enable);
        owner.saveBtn.SetActive(enable);
        owner.saveMap.SetActive(enable);
        owner.mapNameField.SetActive(enable);
    }
}