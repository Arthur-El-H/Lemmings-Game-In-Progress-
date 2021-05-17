using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilding : IState
{
    Unit owner;
    int board_Number;
    public Test test;
    public List<Board> boards = new List<Board>();
    public feldPalette feldpalette;
    public Save save;

    public BoardBuilding(Unit owner, int board_Number, Save save = null) { this.owner = owner; this.board_Number = board_Number; this.save = save; }    

    public void Enter()
    {
        enableButtons(true);
        owner.closeTip.SetActive(true);

        feldpalette = owner.feldPalette.GetComponent<feldPalette>();
        owner.currentBoardBuilding = this;
        if (save != null) { boards = owner.BoardBuilder.buildBoardsForEditing(save); }
        else { boards = owner.BoardBuilder.buildBoards(board_Number); }

        board_Number = boards.Count;
    }

    public void Execute()
    {
        for (int i = 0; i < board_Number; i++)
        {
            boards[i].lemming.lemmingUpdate();
            feldpalette.palettenUpdate();
        }
    } 

    public void Exit()
    {
        enableButtons(false);

        for (int i = 0; i < boards.Count; i++)
        {
            boards[i].delete();
        }
    }
    
    public bool checkForZiele(List<Board> boards)
    {
        bool Ziel = true;
        foreach (Board board in boards)
        {
            if (board.zielCount != 1) Ziel = false;
        }
        return Ziel;
    }
    
    void enableButtons(bool enable)
    {
        owner.FeldPalette.SetActive(enable);
        owner.bedingung.SetActive(enable);
        owner.testBtn.SetActive(enable);
    }

    public Save getSave() { return owner.boardsToSave(boards); }
}
