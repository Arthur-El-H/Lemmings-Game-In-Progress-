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
    boardNumberChoosing Board_8;

    boardNumberChoosing Chosen;
    boardNumberChoosing newChosen;
    chooseButton chooseButton;



    public void Enter()
    {
        numberPalette = owner.numberPalette;
        ChooseButton = owner.chooseButton;
        owner.howManyBoards.SetActive(true);

        Board_2 = owner.Board_2;
        Board_4 = owner.Board_4;
        Board_6 = owner.Board_6;
        Board_8 = owner.Board_8;

        numberPalette.SetActive(true);
        ChooseButton.SetActive(true);
        Debug.Log("    Enter    ");
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
            {
                newChosen = (boardNumberChoosing) hit.collider.GetComponent(typeof(boardNumberChoosing));
                chooseButton = (chooseButton) hit.collider.GetComponent(typeof(chooseButton));
                Debug.Log(chooseButton);
                if (newChosen)
                {
                    Board_2.unChoose();
                    Board_4.unChoose();
                    Board_6.unChoose();
                    Board_8.unChoose();
                    newChosen.choose();
                    Debug.Log("New Chosen ist " + newChosen);

                    Chosen = newChosen;
                }

                else if (chooseButton) 
                {
                    if (Chosen == Board_2) owner.stateMachine.ChangeState(new BoardBuilding(owner, 2, owner.leeresBrett));
                    if (Chosen == Board_4) owner.stateMachine.ChangeState(new BoardBuilding(owner, 4, owner.leeresBrett));
                    if (Chosen == Board_6) owner.stateMachine.ChangeState(new BoardBuilding(owner, 6, owner.leeresBrett));
                    if (Chosen == Board_8) owner.stateMachine.ChangeState(new BoardBuilding(owner, 8, owner.leeresBrett));
                }

            }
        }
    }
}
