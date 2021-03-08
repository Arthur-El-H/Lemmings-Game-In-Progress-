using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilding : IState
{
    Unit owner;
    int board_Number;
    GameObject leeresBrett;
    public Test test;
    public List<Board> boards = new List<Board>();

    public BoardBuilding(Unit owner, int board_Number, GameObject leeresBrett) { this.owner = owner;this.board_Number = board_Number; this.leeresBrett = owner.leeresBrett; }

    

    public void Enter()
    {
        Debug.Log("Wir bauen jetzt " + board_Number + " Boards.");
        owner.FeldPalette.SetActive(true);
        owner.testButton.SetActive(true);
        owner.outOfService.SetActive(true);
        owner.bedingung.SetActive(true);

        buildBoards(board_Number);

        test = GameObject.Find("TestButton").GetComponent<Test>();
    }

    public void Execute()
    {
        if(test.goingToTest) { if(checkForZiele(boards))owner.stateMachine.ChangeState(new TestingState(owner, boards)); }

        for (int i = 0; i < board_Number; i++)
        {
            boards[i].boardUpdate();
        }
    }

    public void Exit()
    {
        owner.FeldPalette.SetActive(false);
        owner.testButton.SetActive(false);
        owner.outOfService.SetActive(false);
        owner.bedingung.SetActive(false);


        for (int i = 0; i < boards.Count; i++)
        {
            boards[i].delete();
        }

        test.goingToTest = false;
    }
    private bool checkForZiele(List<Board> boards)
    {
        int var = 0;
        foreach (Board board in boards)
        {
            foreach (string tag in board.boardTags)
            {
                if (tag == "Ziel") { var++;}
            }
        }
        Debug.Log(var);
        test.goingToTest = false;
        if(var == boards.Count) { return true; }
        else { return false; }

    }
    public void buildBoards (int boardCount)
    {
        switch (boardCount)
        {
            case 2: 
                GameObject leeresBrett21 = GameObject.Instantiate(leeresBrett, new Vector3 (6,1,0), Quaternion.identity) as GameObject;     
                boards.Add(leeresBrett21.GetComponent<Board>()); boards[0].identity = 0;
                GameObject leeresBrett22 = GameObject.Instantiate(leeresBrett, new Vector3 (13, 1, 0), Quaternion.identity) as GameObject;  
                boards.Add(leeresBrett22.GetComponent<Board>()); boards[1].identity = 1;
                break;
            case 4:
                GameObject leeresBrett41 = GameObject.Instantiate(leeresBrett, new Vector3(6, 1, 0), Quaternion.identity) as GameObject;    
                boards.Add(leeresBrett41.GetComponent<Board>()); boards[0].identity = 0;
                GameObject leeresBrett42 = GameObject.Instantiate(leeresBrett, new Vector3(13, -7, 0), Quaternion.identity) as GameObject;
                boards.Add(leeresBrett42.GetComponent<Board>()); boards[1].identity = 1;
                GameObject leeresBrett43 = GameObject.Instantiate(leeresBrett, new Vector3(6, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett43.GetComponent<Board>()); boards[2].identity = 2;
                GameObject leeresBrett44 = GameObject.Instantiate(leeresBrett, new Vector3(13, 1, 0), Quaternion.identity) as GameObject;
                boards.Add(leeresBrett44.GetComponent<Board>()); boards[3].identity = 3;

                break;
            case 6:
                GameObject leeresBrett61 = GameObject.Instantiate(leeresBrett, new Vector3(3, 1, 0), Quaternion.identity) as GameObject;
                boards.Add(leeresBrett61.GetComponent<Board>()); boards[0].identity = 0;
                GameObject leeresBrett62 = GameObject.Instantiate(leeresBrett, new Vector3(9.5f, 1, 0), Quaternion.identity) as GameObject;    
                boards.Add(leeresBrett62.GetComponent<Board>()); boards[1].identity = 1;
                GameObject leeresBrett63 = GameObject.Instantiate(leeresBrett, new Vector3(16, 1, 0), Quaternion.identity) as GameObject; 
                boards.Add(leeresBrett63.GetComponent<Board>()); boards[2].identity = 2;
                GameObject leeresBrett64 = GameObject.Instantiate(leeresBrett, new Vector3(3, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett64.GetComponent<Board>()); boards[3].identity = 3;
                GameObject leeresBrett65 = GameObject.Instantiate(leeresBrett, new Vector3(9.5f, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett65.GetComponent<Board>()); boards[4].identity = 4;
                GameObject leeresBrett66 = GameObject.Instantiate(leeresBrett, new Vector3(16, -7, 0), Quaternion.identity) as GameObject;
                boards.Add(leeresBrett66.GetComponent<Board>()); boards[5].identity = 5;

                break;
            case 8:
                GameObject leeresBrett81 = GameObject.Instantiate(leeresBrett, new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
                boards.Add(leeresBrett81.GetComponent<Board>()); boards[0].identity = 0;
                GameObject leeresBrett82 = GameObject.Instantiate(leeresBrett, new Vector3(6, 1, 0), Quaternion.identity) as GameObject;    
                boards.Add(leeresBrett82.GetComponent<Board>()); boards[1].identity = 1;
                GameObject leeresBrett83 = GameObject.Instantiate(leeresBrett, new Vector3(13, 1, 0), Quaternion.identity) as GameObject;    
                boards.Add(leeresBrett83.GetComponent<Board>()); boards[2].identity = 2;
                GameObject leeresBrett84 = GameObject.Instantiate(leeresBrett, new Vector3(19, 1, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett84.GetComponent<Board>()); boards[3].identity = 3;
                GameObject leeresBrett85 = GameObject.Instantiate(leeresBrett, new Vector3(0, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett85.GetComponent<Board>()); boards[4].identity = 4;
                GameObject leeresBrett86 = GameObject.Instantiate(leeresBrett, new Vector3(6, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett86.GetComponent<Board>()); boards[5].identity = 5;
                GameObject leeresBrett87 = GameObject.Instantiate(leeresBrett, new Vector3(13, -7, 0), Quaternion.identity) as GameObject;   
                boards.Add(leeresBrett87.GetComponent<Board>()); boards[6].identity = 6;
                GameObject leeresBrett88 = GameObject.Instantiate(leeresBrett, new Vector3(19, -7, 0), Quaternion.identity) as GameObject;  
                boards.Add(leeresBrett88.GetComponent<Board>()); boards[7].identity = 7;

                break;
        }
    }
}
