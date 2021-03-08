using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingState : IState
{
    Unit owner;
    public List<Board> boardInput;
    public List<Board> test_boards;
    public Board currentBoard;
    GameObject leeresBrett;
    GameObject currentLeeresBrett;
    public levelCreator levelCreator;

    List<dragableLemming> startingLemmings;
    List<Direction> startingDirections;

    List<dragableLemming> lemmings;
    List<Direction> directions;

    List<dragableLemming> workingLemmings;
    List<Direction> workingDirections;
    public twoLists twoLists;

    int manageMoveState = 1;

    private int missedLemmings;
    private float controlTime;
    private float coolDownTime = .5f;

    bool loss = false;
    bool win = false;

    private dragableLemming varLemming;

    public TestingState(Unit owner, List<Board> boards ) { this.owner = owner; this.boardInput = boards; }



    public void Enter()
    {
        levelCreator = GameObject.Find("Manager").GetComponent<levelCreator>();
        test_boards = new List<Board>();


        workingLemmings = new List<dragableLemming>();
        workingDirections = new List<Direction>();
        startingLemmings = new List<dragableLemming>();
        startingDirections = new List<Direction>();

        buildBoards(boardInput);

        twoLists = new twoLists();
        twoLists.lemmings = new List<dragableLemming>();
        twoLists.directions = new List<Direction>();


    }

    public void Execute()
    {
        if (Time.time > controlTime + coolDownTime)
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                for (int i = 0; i < test_boards.Count; i++)
                {
                    startingLemmings.Add(test_boards[i].lemming);
                    startingDirections.Add(Direction.Up);
                }
                manageMoves(startingLemmings, startingDirections);
                startingDirections.Clear();
                startingLemmings.Clear();
                controlTime = Time.time;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                for (int i = 0; i < test_boards.Count; i++)
                {
                    startingLemmings.Add(test_boards[i].lemming);
                    startingDirections.Add(Direction.Down);
                }
                manageMoves(startingLemmings, startingDirections);
                startingDirections.Clear();
                startingLemmings.Clear();
                controlTime = Time.time;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                for (int i = 0; i < test_boards.Count; i++)
                {
                    startingLemmings.Add(test_boards[i].lemming);
                    startingDirections.Add(Direction.Right);
                }
                manageMoves(startingLemmings, startingDirections);
                startingDirections.Clear();
                startingLemmings.Clear();
                controlTime = Time.time;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                for (int i = 0; i < test_boards.Count; i++)
                {
                    startingLemmings.Add(test_boards[i].lemming);
                    startingDirections.Add(Direction.Left);
                }
                manageMoves(startingLemmings, startingDirections);
                startingDirections.Clear();
                startingLemmings.Clear(); 
                controlTime = Time.time;
            }
        }
    }

    public void Exit()
    {
        for (int i = 0; i < test_boards.Count; i++)
        {
            test_boards[i].delete();
        }
    }

    public void manageMoves(List<dragableLemming> lemmingss, List<Direction> directionss)
    {
        lemmings = new List<dragableLemming>(lemmingss);
        directions = new List<Direction>(directionss);

        if (lemmings.Count == 0) { Debug.Log("Stop"); return; }
        switch (manageMoveState)
        {
            case 1:
                workingLemmings.Clear();
                workingDirections.Clear();
                missedLemmings = 0;
                for (int i = 0; i < lemmings.Count; i++)
                {
                    if ( 
                        lemmings[i].board.boardFelder[lemmings[i].currentFeld].TryFrom(directions[i]) &&
                        lemmings[i].board.boardFelder[getAdjacentField(lemmings[i].currentFeld, directions[i])].TryTo(directions[i])
                        )
                    {
                        workingLemmings.Add(lemmings[i]);
                        workingDirections.Add(directions[i]);
                        int x = i - missedLemmings;
                        workingLemmings[x].currentFeld = getAdjacentField(workingLemmings[x].currentFeld, workingDirections[x]);
                        workingLemmings[x].Feld = workingLemmings[x].board.boardFelder[workingLemmings[x].currentFeld];
                    }
                    else { missedLemmings++; }
                }
                manageMoveState = 2;

                break;

            case 2:   
                
                for (int i = 0; i < workingLemmings.Count; i++)
                {
                    workingLemmings[i].moveTo(workingLemmings[i].Feld.GetAnchorPoint());                                    
                }
                lemmings = new List<dragableLemming>(workingLemmings);
                directions = new List<Direction>(workingDirections);

                workingLemmings.Clear();
                workingDirections.Clear();
                manageMoveState = 3;
                manageMoves(lemmings, directions);
                return;
                
            case 3:
                twoLists.directions.Clear();
                twoLists.lemmings.Clear();
                checkForFlame(lemmings);
                if (loss) { Debug.Log("Loss"); owner.stateMachine.ChangeState(new lossState(owner, true, boardInput)); lemmings.Clear(); directions.Clear(); return; }
                checkForWin(lemmings);
                if (win) { Debug.Log("win"); owner.stateMachine.ChangeState(new WinState(owner)); lemmings.Clear(); directions.Clear(); return; }
                checkForClockWise(lemmings);
                checkForCounterClockwise(lemmings);
                for (int i = 0; i < lemmings.Count; i++)
                {
                    twoLists = lemmings[i].Feld.BeAt(lemmings[i], twoLists);
                }
                manageMoveState = 1;

                workingLemmings.Clear();
                workingDirections.Clear();
                workingLemmings = new List<dragableLemming>(twoLists.lemmings);
                workingDirections = new List<Direction>(twoLists.directions);

                break;
        }
        manageMoves(workingLemmings, workingDirections);
    }

    private void checkForFlame(List<dragableLemming> lemmings)
    {
        foreach (dragableLemming lemming in lemmings)
        {
            if (lemming.Feld.GetTag() == "Flamme") { loss = true; }
        }
    }
    private void checkForWin(List<dragableLemming> lemmings)
    {
        int i = 0;
        foreach (dragableLemming lemming in lemmings)
        {
            if (lemming.Feld.GetTag() == "Ziel") { i++; }
        }
        Debug.Log("check for win. i ist " + i);
        if (i == test_boards.Count) { win = true; }
    }
    private void checkForClockWise(List<dragableLemming> lemmings)
    {        
        int i = 0;
        foreach (dragableLemming lemming in lemmings)
        {
            if (lemming.Feld.GetTag() == "imUhrzeigersinn") { i++; }
        }

        for (int f = 0; f < i; f++)
        {
            varLemming = lemmings[0];
            for (int g = 0; g < (lemmings.Count - 1); g++)
            {
                lemmings[g].board = lemmings[g + 1].board;
            }
            lemmings[lemmings.Count-1].board = varLemming.board;

            foreach(dragableLemming lemming in lemmings)
            {
                lemming.Feld = lemming.board.boardFelder[lemming.currentFeld];
                lemming.transform.position = lemming.Feld.GetAnchorPoint();
            }
        }
    }
    private void checkForCounterClockwise(List<dragableLemming> lemmings)
    {
        int i = 0;
        foreach (dragableLemming lemming in lemmings)
        {
            if (lemming.Feld.GetTag() == "ggUhrzeigersinn") { i++; }
        }
        for (int f = 0; f < i; f++)
        {
            Debug.Log("So of läufts schon: " + f);

            varLemming = lemmings[0];
            for (int g = 0; g < (lemmings.Count - 1); g++)
            {
                lemmings[g].board = lemmings[g + 1].board;
            }
            lemmings[lemmings.Count-1].board = varLemming.board;

            foreach (dragableLemming lemming in lemmings)
            {
                lemming.Feld = lemming.board.boardFelder[lemming.currentFeld];
                lemming.transform.position = lemming.Feld.GetAnchorPoint();
            }
        }
    }
    private int getAdjacentField(int startField, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:  
                if (startField > 4) { return (startField - 5); }
                else owner.stateMachine.ChangeState(new lossState(owner, false, boardInput)); break;

            case Direction.Down:
                if (startField < 25) { return (startField + 5); }
                else owner.stateMachine.ChangeState(new lossState(owner, false, boardInput)); break;

            case Direction.Left:
                if ((startField%5) > 0) { return (startField - 1); }
                else owner.stateMachine.ChangeState(new lossState(owner, false, boardInput)); break;

            case Direction.Right:
                if ((startField%5) < 5) { return (startField + 1); }
                else owner.stateMachine.ChangeState(new lossState(owner, false, boardInput)); break;
        }
        return 0;
    }

    private void buildBoards(List<Board> boards)
    {
        for (int i = 0; i < boards.Count; i++)
        {
            buildBoard(boards[i], i);
        }
    }

    private void buildBoard(Board board, int i)
    {
        currentLeeresBrett = GameObject.Instantiate(levelCreator.leeresBrett, getPos(i), Quaternion.identity) as GameObject;
        test_boards.Add(currentLeeresBrett.GetComponent<Board>());
        test_boards[i].identity = i;
        for (int f = 0; f < 30; f++)
        {
            test_boards[i].board[f].currentField = createFromTag(board.boardTags[f], test_boards[i].board[f].anchorPoint);
            test_boards[i].boardFelder.Add(test_boards[i].board[f].currentField.GetComponent<IFeld>());
        }
        test_boards[i].lemmingPos = board.lemmingPos;
        test_boards[i].lemming.currentFeld = test_boards[i].lemmingPos;
        test_boards[i].lemming.position = test_boards[i].boardFelder[test_boards[i].lemmingPos].GetAnchorPoint();
        test_boards[i].lemming.transform.position = test_boards[i].lemming.position;

    }

    private Vector3 getPos(int identity)
    {
        switch (boardInput.Count)
        {
            case 2: return new Vector3((6 * identity + identity), 1, 0);

            case 4: return new Vector3(6 * identity + identity, 1, 0);

            case 6: 
                if (identity < 3)  return new Vector3(6 * identity + identity -1, 1, 0); 
                else  return new Vector3(6 * (identity - 3) + (identity - 3) -1, -7, 0); 

            case 8:
                if (identity < 4) return new Vector3(6 * identity + identity -1, 1, 0);
                else return new Vector3(6 * (identity - 4) + (identity - 4) -1, -7, 0);
        }
        return Vector3.zero;
    }

    private GameObject createFromTag(string tag, Vector2 pos)
    {
        switch(tag)
        {
            case "ORUL": return GameObject.Instantiate(levelCreator.ORUl, pos , Quaternion.identity);
            case "ORU": return GameObject.Instantiate(levelCreator.ORU, pos, Quaternion.identity);
            case "ORL": return GameObject.Instantiate(levelCreator.ORL, pos, Quaternion.identity);
            case "RUL": return GameObject.Instantiate(levelCreator.RUL, pos, Quaternion.identity);
            case "OUL": return GameObject.Instantiate(levelCreator.OUL, pos, Quaternion.identity);

            case "OL": return GameObject.Instantiate(levelCreator.OL, pos, Quaternion.identity);
            case "OR": return GameObject.Instantiate(levelCreator.OR, pos, Quaternion.identity);
            case "UL": return GameObject.Instantiate(levelCreator.UL, pos, Quaternion.identity);
            case "UR": return GameObject.Instantiate(levelCreator.UR, pos, Quaternion.identity);

            case "OU": return GameObject.Instantiate(levelCreator.OU, pos, Quaternion.identity);
            case "RL": return GameObject.Instantiate(levelCreator.RL, pos, Quaternion.identity);

            case "O": return GameObject.Instantiate(levelCreator.O, pos, Quaternion.identity);
            case "U": return GameObject.Instantiate(levelCreator.U, pos, Quaternion.identity);
            case "L": return GameObject.Instantiate(levelCreator.L, pos, Quaternion.identity);
            case "R": return GameObject.Instantiate(levelCreator.R, pos, Quaternion.identity);

            case "1R": return GameObject.Instantiate(levelCreator._R, pos, Quaternion.identity);
            case "2R": return GameObject.Instantiate(levelCreator._RR, pos, Quaternion.identity);
            case "1L": return GameObject.Instantiate(levelCreator._L, pos, Quaternion.identity);
            case "2L": return GameObject.Instantiate(levelCreator._LL, pos, Quaternion.identity);
            case "1U": return GameObject.Instantiate(levelCreator._U, pos, Quaternion.identity);
            case "2U": return GameObject.Instantiate(levelCreator._UU, pos, Quaternion.identity);
            case "1H": return GameObject.Instantiate(levelCreator._O, pos, Quaternion.identity);
            case "2H": return GameObject.Instantiate(levelCreator._OO, pos, Quaternion.identity);

            case "Ziel": return GameObject.Instantiate(levelCreator.Ziel, pos, Quaternion.identity);
            case "leer": return GameObject.Instantiate(levelCreator.leer, pos, Quaternion.identity);
            case "Flamme": return GameObject.Instantiate(levelCreator.Flamme, pos, Quaternion.identity);

            case "imUhrzeigersinn": return GameObject.Instantiate(levelCreator.imUhrzeigersinn, pos, Quaternion.identity);
            case "ggUhrzeigersinn": return GameObject.Instantiate(levelCreator.ggUhrzeigersinn, pos, Quaternion.identity);
        }        
        return null;
    }
}
