using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardChoosing : IState
{
    Unit owner;

    public BoardChoosing (Unit owner) { this.owner = owner; }

    GameObject numberPalette;
    GameObject ChooseButton;

    boardNumberChoosing Board_2;
    boardNumberChoosing Board_4;
    boardNumberChoosing Board_6;

    boardNumberChoosing Chosen;
    boardNumberChoosing newChosen;
    chooseButton chooseButton;



    public void Enter()
    {
        numberPalette = owner.numberPalette;
        ChooseButton = owner.chooseButton;
        owner.howManyBoards.SetActive(true);
        owner.closeTip.SetActive(false);


        Board_2 = owner.Board_2;
        Board_4 = owner.Board_4;
        Board_6 = owner.Board_6;

        Board_2.unChoose();
        Board_4.unChoose();
        Board_6.unChoose();

        numberPalette.SetActive(true);
        ChooseButton.SetActive(true);
    }

    public void Execute()
    {
        chooseBoard();
    }

    public void Exit()
    {
        numberPalette.SetActive(false);
        ChooseButton.SetActive(false);
        owner.howManyBoards.SetActive(false);
    }



    private void chooseBoard()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider.gameObject != null)
            {
                newChosen = (boardNumberChoosing) hit.collider.GetComponent(typeof(boardNumberChoosing));
                chooseButton = (chooseButton) hit.collider.GetComponent(typeof(chooseButton));

                if (newChosen)
                {
                    Board_2.unChoose();
                    Board_4.unChoose();
                    Board_6.unChoose();
                    newChosen.choose();

                    Chosen = newChosen;
                }

                else if (chooseButton) 
                {
                    if (Chosen == Board_2) owner.stateMachine.ChangeState(new BoardBuilding(owner, 2));
                    if (Chosen == Board_4) owner.stateMachine.ChangeState(new BoardBuilding(owner, 4));
                    if (Chosen == Board_6) owner.stateMachine.ChangeState(new BoardBuilding(owner, 6));
                }

            }
        }
    }
}
