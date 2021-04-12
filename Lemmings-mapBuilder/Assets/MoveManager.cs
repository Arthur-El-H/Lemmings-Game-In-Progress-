using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public Unit owner;
    public List<Board> boardInput = new List<Board>();
    public List<Board> currentBoards = new List<Board>();

    List<dragableLemming> startingLemmings = new List<dragableLemming>();

    List<dragableLemming> lemmings = new List<dragableLemming>();
    List<Direction> directions = new List<Direction>();

    public twoLists twoLists = new twoLists();

    bool loss = false;
    bool win = false;

    private Board board;
    private dragableLemming currentLemming;

    string fire = "One of your lemmings got into the fire. You lost.";
    string fellOff = "One of your lemmings fell from the map. You lost.";
    string trapped = "One of your lemmings is trapped. You lost.";


    public void startManagingMoves(List<dragableLemming> lemmingss, List<Direction> directionss)
    {
        startingLemmings = lemmingss;
        manageMoves(lemmingss, directionss);
    }

    public void manageMoves(List<dragableLemming> lemmingss, List<Direction> directionss)
    {
        lemmings = new List<dragableLemming>(lemmingss);
        directions = new List<Direction>(directionss);

        twoLists.directions.Clear();
        twoLists.lemmings.Clear();
        twoLists = getTargetFields();
        //check for Loss through adjacent fields

        moveLemmings(twoLists.lemmings);

        checkForWin(startingLemmings);
        if (win) { owner.stateMachine.ChangeState(new WinState(owner, boardInput)); return; }

        checkForFlame(twoLists.lemmings);
        if (loss) { owner.stateMachine.ChangeState(new lossState(owner, boardInput, fire)); return; }

        checkForClockWise(twoLists.lemmings);
        checkForCounterClockwise(twoLists.lemmings);

        checkForFlame(startingLemmings);
        if (loss) { owner.stateMachine.ChangeState(new lossState(owner, boardInput, fire)); return; }

        checkForTrappedLemming(startingLemmings);
        if (loss) { owner.stateMachine.ChangeState(new lossState(owner, boardInput, trapped)); return; }

        reactToFields();

        if (twoLists.lemmings.Count == 0) { return; }
        else { manageMoves(twoLists.lemmings, twoLists.directions); }
    }

    void reactToFields()
    {
        lemmings = new List<dragableLemming>(twoLists.lemmings);
        twoLists.lemmings.Clear();
        twoLists.directions.Clear();
        for (int i = 0; i < lemmings.Count; i++)
        {
            twoLists = lemmings[i].Feld.BeAt(lemmings[i], twoLists);
        }
    }

    void moveLemmings(List<dragableLemming> movingLemmings)
    {
        foreach (dragableLemming lemming in movingLemmings)
        {
            lemming.moveTo(lemming.Feld.GetAnchorPoint());
        }
    }

    twoLists getTargetFields()
    {
        for (int i = 0; i < lemmings.Count; i++)
        {
            IFeld currentFeld = lemmings[i].board.boardFelder[lemmings[i].currentFeld];
            IFeld targetFeld = lemmings[i].board.boardFelder[getAdjacentField(lemmings[i].currentFeld, directions[i])];
            Direction currentDirection = directions[i];
            if (
                currentFeld.TryFrom(currentDirection) &&
                targetFeld.TryTo(currentDirection)
                )
            {
                currentLemming = lemmings[i];
                currentLemming.currentFeld = getAdjacentField(currentLemming.currentFeld, currentDirection);
                currentLemming.Feld = currentLemming.board.boardFelder[currentLemming.currentFeld];

                twoLists.lemmings.Add(currentLemming);
                twoLists.directions.Add(currentDirection);
            }
        }
        return twoLists;
    }

    private int getAdjacentField(int startField, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if (startField > 4) { return (startField - 5); }
                else owner.stateMachine.ChangeState(new lossState(owner, boardInput, fellOff)); break;

            case Direction.Down:
                if (startField < 25) { return (startField + 5); }
                else owner.stateMachine.ChangeState(new lossState(owner, boardInput, fellOff)); break;

            case Direction.Left:
                if ((startField % 5) > 0) { return (startField - 1); }
                else owner.stateMachine.ChangeState(new lossState(owner, boardInput, fellOff)); break;

            case Direction.Right:
                if ((startField % 5) != 4) { return (startField + 1); }
                else owner.stateMachine.ChangeState(new lossState(owner, boardInput, fellOff)); break;
        }
        return 0;
    }

    //checking Fields lemmings are on Functions
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
        if (i == currentBoards.Count) { win = true; }
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
            board = startingLemmings[0].board;
            for (int g = 0; g < (startingLemmings.Count - 1); g++)
            {
                startingLemmings[g].board = startingLemmings[g + 1].board;
            }
            startingLemmings[startingLemmings.Count - 1].board = board;

            foreach (dragableLemming lemming in startingLemmings)
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
            board = startingLemmings[startingLemmings.Count - 1].board;
            for (int g = startingLemmings.Count - 1; g > 0; g--)
            {
                startingLemmings[g].board = startingLemmings[g - 1].board;
            }
            startingLemmings[0].board = board;

            foreach (dragableLemming lemming in startingLemmings)
            {
                lemming.Feld = lemming.board.boardFelder[lemming.currentFeld];
                lemming.transform.position = lemming.Feld.GetAnchorPoint();
            }
        }
    }
    private void checkForTrappedLemming(List<dragableLemming> lemmings)
    {
        foreach (dragableLemming lemming in lemmings)
        {
            if (lemming.Feld.GetTag() == "ORUL")
            {
                loss = true;
                lemming.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, .5f, .5f);
                return;
            }
        }
    }

}
