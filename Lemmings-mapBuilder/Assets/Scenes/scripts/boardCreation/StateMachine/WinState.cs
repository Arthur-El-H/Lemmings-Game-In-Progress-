using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState, IBoardsOwner
{
    Unit owner;
    List<Board> boards;

    public WinState(Unit owner, List<Board> boards) { this.owner = owner; this.boards = boards; }



    public void Enter()
    {
        owner.setBoardsToSave(boards);
        if (!owner.MoveManager.getIsPlaying()) { owner.setCurrentSave(owner.boardsToSave(boards)); }
        owner.infoGatherer.sendState(oldest_state.Win);
        enableButtons(true);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        enableButtons(false);
        owner.MoveManager.setIsPlaying(false);
    }

    public Save getBoards()
    {
        return owner.boardsToSave(boards);
    }

    void enableButtons(bool enable)
    {
        if(!owner.MoveManager.getIsPlaying())
        {
            owner.changeMapBtn.SetActive(enable);
            owner.saveBtn.SetActive(enable);
            owner.saveMap.SetActive(enable);
            owner.mapNameField.SetActive(enable);
        }
        owner.closeTip.SetActive(enable);
        owner.backToStartBtn.SetActive(enable);
    }
}