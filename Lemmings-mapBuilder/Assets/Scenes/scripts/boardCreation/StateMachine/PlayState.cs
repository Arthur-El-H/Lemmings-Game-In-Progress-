using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IState
{
    Unit owner;
    Save save;
    List<Board> boards;

    List<dragableLemming> startingLemmings = new List<dragableLemming>();
    List<Direction> startingDirections = new List<Direction>();

    private float controlTime;
    private float coolDownTime = .5f;

    public PlayState(Unit owner, Save save) {this.owner = owner; this.save = save; }

    public void Enter()
    {
        boards = new List<Board>();
        Debug.Log(save.boardTags.Count +  " is how much boards im going to build");
        boards = owner.BoardBuilder.buildBoardsWithTags(save);

        owner.setCurrentSave(save);
        owner.MoveManager.setCurrentBoards(boards);
        owner.MoveManager.setIsPlaying(true);
        owner.MoveManager.initNextToFire();
        owner.MoveManager.initWinFields();

        foreach (Board board in boards)
        {
            board.lemming.Feld = board.boardFelder[board.lemming.currentFeld];
        }
        owner.destroyColliders(boards);
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

        foreach (dragableLemming lemming in startingLemmings) { lemming.lemmingUpdate(); }

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
            boards[i].lemming.currentDirection = direction;
            startingDirections.Add(direction);
        }
        owner.MoveManager.startManagingMoves(startingLemmings, startingDirections);
        controlTime = Time.time;
    }
}
