using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IState
{
    Unit owner;
    float time;
    Save save;
    List<Board> boards;

    List<dragableLemming> startingLemmings = new List<dragableLemming>();
    List<Direction> startingDirections = new List<Direction>();

    private float controlTime;
    private float coolDownTime = .5f;


    public PlayState(Unit owner, Save save)
    {this.owner = owner; this.save = save; }

    public void Enter()
    {
        boards = new List<Board>();
        boards = owner.BoardBuilder.buildBoardsWithTags(save);

        owner.MoveManager.currentBoards = boards;

        foreach (Board board in boards)
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
        for (int i = 0; i < boards.Count; i++)
        {
            boards[i].delete();
        }
    }

    public void moveLemmings(Direction direction)
    {
        startingDirections.Clear();
        startingLemmings.Clear();
        for (int i = 0; i < boards.Count; i++)
        {
            startingLemmings.Add(boards[i].lemming);
            startingDirections.Add(direction);
        }
        owner.MoveManager.startManagingMoves(startingLemmings, startingDirections);
        controlTime = Time.time;
    }
}
