using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingState : IState
{
    Unit owner;
    public List<Board> boardInput;
    public List<Board> test_boards;

    List<dragableLemming> startingLemmings = new List<dragableLemming>();
    List<Direction> startingDirections = new List<Direction>();

    private float controlTime;
    private float coolDownTime = .5f;

    public TestingState(Unit owner, List<Board> boards ) { this.owner = owner; this.boardInput = boards; }

    public void Enter()
    {
        test_boards = new List<Board>();

        test_boards = owner.BoardBuilder.buildBoardsWithTags(owner.boardsToSave(boardInput));

        owner.MoveManager.boardInput = boardInput;
        owner.MoveManager.currentBoards = test_boards;

        foreach (Board board in test_boards)
        {
            board.lemming.Feld = board.boardFelder[board.lemming.currentFeld];
        }
    }

    public void Execute()
    {
        if (Time.time > controlTime + coolDownTime)
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                    moveLemmings(Direction.Up);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                    moveLemmings(Direction.Down);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                    moveLemmings(Direction.Right);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                    moveLemmings(Direction.Left);
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

    public void moveLemmings(Direction direction)
    {
        startingDirections.Clear();
        startingLemmings.Clear();
        for (int i = 0; i < test_boards.Count; i++)
        {
            startingLemmings.Add(test_boards[i].lemming);
            startingDirections.Add(direction);
        }
        owner.MoveManager.startManagingMoves(startingLemmings, startingDirections);
        controlTime = Time.time;
    }
}
