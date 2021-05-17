using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public Unit owner;
    public List<Board> boardInput = new List<Board>();
    public List<Board> currentBoards = new List<Board>();
    public informationGatherer infoGatherer;
    int amountOfBoards;
    bool isPlaying;
    public void setIsPlaying(bool playing) { isPlaying = playing; }
    public bool getIsPlaying() { return isPlaying; }
    public void setCurrentBoards(List<Board> boards )
    {
        currentBoards = boards;
        amountOfBoards = currentBoards.Count;
    }

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

    List<int> clockwiseHelper4 = new List<int> {1, 3, 0, 2 };
    List<int> clockwiseHelper6 = new List<int> { 1, 2, 5, 0, 3, 4 };


    public void startManagingMoves(List<dragableLemming> lemmingss, List<Direction> directionss)
    {
        startingLemmings = lemmingss;
        manageMoves(lemmingss, directionss);
        loss = false;
        win = false;
    }

    public void manageMoves(List<dragableLemming> lemmingss, List<Direction> directionss)
    {
        lemmings = new List<dragableLemming>(lemmingss);
        directions = new List<Direction>(directionss);

        twoLists.directions.Clear();
        twoLists.lemmings.Clear();
        twoLists = getTargetFields();

        moveLemmings(twoLists.lemmings);
        if (loss) { loose(fellOff); return; }

        checkForWin(startingLemmings);
        if (win) { owner.stateMachine.ChangeState(new WinState(owner, boardInput)); owner.infoGatherer.sendWin(); return; }

        checkForFlame(twoLists.lemmings);
        if (loss) { loose(fire); return; }

        checkForClockWise(twoLists.lemmings);
        checkForCounterClockwise(twoLists.lemmings);

        checkForFlame(startingLemmings);
        if (loss) { loose(fire); return; }

        checkForTrappedLemming(startingLemmings);
        if (loss) { loose(trapped); return; }

        reactToFields();

        infoGatherer.retrieveHeat(calcHeat());
        infoGatherer.retrieveWinDistances(calcDistToWin());

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
             lemming.moveToTarget();
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
                else /*loose(fellOff)*/ loss = true; break;

            case Direction.Down:
                if (startField < 25) { return (startField + 5); }
                else loss = true; break;

            case Direction.Left:
                if ((startField % 5) > 0) { return (startField - 1); }
                else loss = true; break;

            case Direction.Right:
                if ((startField % 5) != 4) { return (startField + 1); }
                else loss = true; break;
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
    private void checkForClockWise(List<dragableLemming> lemmings)
    {
        int i = 0;
        foreach (dragableLemming lemming in lemmings)       //how often?
        {
            if (lemming.Feld.GetTag() == "imUhrzeigersinn") { i++; }
        }

        for (int f = 0; f < i; f++)                         // i == 1 most of the time
        {

            for (int g = 0; g < (amountOfBoards); g++)      // every lemming gets its new board
            {
                int currentIdentity = startingLemmings[g].board.identity;
                startingLemmings[g].board = findBoardByIdentity(clockwiseHelper(currentIdentity));
            }
        }
        if (i > 0)
        {
            foreach (dragableLemming lemming in startingLemmings)   //every lemming is placed on its new board
            {
                lemming.Feld = lemming.board.boardFelder[lemming.currentFeld];
                lemming.transform.position = lemming.Feld.GetAnchorPoint();
            }
        }
        
    }
    private void checkForCounterClockwise(List<dragableLemming> lemmings)
    {
        int i = 0;
        foreach (dragableLemming lemming in lemmings)       //how often?
        {
            if (lemming.Feld.GetTag() == "ggUhrzeigersinn") { i++; }
        }

        for (int f = 0; f < i; f++)                         // i == 1 most of the time
        {

            for (int g = 0; g < (amountOfBoards); g++)      // every lemming gets its new board
            {
                int currentIdentity = startingLemmings[g].board.identity;
                startingLemmings[g].board = findBoardByIdentity(counterClockwiseHelper(currentIdentity));
            }

        }

        if (i > 0)
        {
            foreach (dragableLemming lemming in startingLemmings)   //every lemming is placed on its new board
            {
                lemming.Feld = lemming.board.boardFelder[lemming.currentFeld];
                lemming.transform.position = lemming.Feld.GetAnchorPoint();
            }
        }
        
    }    
    int clockwiseHelper(int ownBoard)
    {
        if (amountOfBoards == 2)      { if (ownBoard == 0) { return 1; } else { return 0; } }
        else if (amountOfBoards == 4) { return clockwiseHelper4[ownBoard]; }
        else                          { return clockwiseHelper6[ownBoard]; }
    }
    int counterClockwiseHelper(int ownBoard)
    {
        if (amountOfBoards == 2)      { if (ownBoard == 0) { return 1; } else { return 0; } }
        else if (amountOfBoards == 4) {Debug.Log("should return " + clockwiseHelper4.FindIndex(a => a == ownBoard));
            Debug.Log(startingLemmings[clockwiseHelper4.FindIndex(a => a == ownBoard)].board.identity + "is its identity");
            return clockwiseHelper4.FindIndex(a => a == ownBoard); }
        else                          { return clockwiseHelper6.FindIndex(a => a == ownBoard); }
    }
    Board findBoardByIdentity(int id)
    {
        int newId = currentBoards.FindIndex(a => a.identity == id );
        return currentBoards[newId];
    }
    void loose(string reason)
    {
        owner.stateMachine.ChangeState(new lossState(owner, boardInput, reason));
        owner.infoGatherer.sendLoss(reason);
    }

    // for informationgatherer
    public List<List<int>> nextToFire; //alle Felder, welche direkt neben Feuer sind; Mehrfachvorkommen möglich und sinnvoll
    public List<List<int>> fireFields;

    public void initNextToFire()
    {
        Debug.Log("fire initiated");
        Debug.Log(startingLemmings.Count + " and " + currentBoards.Count);
        nextToFire = new List<List<int>>(); //alle Felder, welche direkt neben Feuer sind; Mehrfachvorkommen möglich und sinnvoll
        fireFields = new List<List<int>>();

        for (int i=0; i<currentBoards.Count; i++)
        {
            fireFields.Add(new List<int>());
            for(int h = 0; h < currentBoards[i].boardTags.Count; h++ )
            {
                Debug.Log("ähhh" + currentBoards[i].boardTags[h]);
                if (currentBoards[i].boardTags[h] == "Flamme") { fireFields[i].Add(h); }
            }
        }

        for (int i = 0; i < fireFields.Count; i++)
        {
            nextToFire.Add(new List<int>());  //9.5.
            foreach (int fire in fireFields[i])
            {
                nextToFire[i].AddRange(adjacent_Y(fire));
            }
        }
    } //called by playstate.enter and testingstate.enter
    private List<int> adjacent_Y(int startField)
    {
        if (startField <= 4)
        {
            return adjacent_X(startField, 0);          //oben an Grenze
        }
        else if (startField > 24)
        {
            return adjacent_X(startField, 2);          //unten an Grenze
        }
        else
        {
            return adjacent_X(startField, 1);          //mittig
        }
    }
    private List<int> adjacent_X(int startField, int yPos)
    {
        List<int> fields = new List<int>();

        if      ((startField % 5) > 0) { return adjacent_Final(startField, yPos, 0); } //linke Grenze
        else if ((startField % 5) < 4) { return adjacent_Final(startField, yPos, 2); } //recht Grenze
        else {                           return adjacent_Final(startField, yPos, 1); } //mittigrecht Grenze
    }
    private List<int> adjacent_Final(int startField, int yPos, int xPos)
    {
        List<int> fields = new List<int>();

        if (xPos == 0 && yPos == 0) { fields.AddRange(new List<int> { startField + 5, startField + 1, startField + 6 }); return fields; } //OL
        if (xPos == 2 && yPos == 0) { fields.AddRange(new List<int> { startField + 5, startField - 1, startField + 4 }); return fields; } //OR
        if (xPos == 0 && yPos == 2) { fields.AddRange(new List<int> { startField - 5, startField + 1, startField - 4 }); return fields; } //UL
        if (xPos == 2 && yPos == 2) { fields.AddRange(new List<int> { startField - 5, startField - 1, startField - 6 }); return fields; } //UR

        if (yPos == 0) { fields.AddRange(new List<int> { startField + 1, startField - 1, startField + 4, startField + 5, startField + 6 }); return fields; } //O
        if (yPos == 2) { fields.AddRange(new List<int> { startField + 1, startField - 1, startField - 4, startField - 5, startField - 6 }); return fields; } //U

        if (xPos == 0) { fields.AddRange(new List<int> { startField - 5, startField - 4, startField + 1, startField + 5, startField + 6 }); return fields; } //L
        if (xPos == 2) { fields.AddRange(new List<int> { startField - 5, startField - 6, startField - 1, startField + 4, startField + 5 }); return fields; } //R

        // ab hier xPos == yPos == 1
        fields.AddRange(new List<int> {  startField - 6, startField - 5, startField - 4,
                                         startField - 1,                 startField + 1,
                                         startField + 4, startField + 5, startField + 6});
        return fields;
    }
    private List<int> calcHeat()
    {
        List<int> results = new List<int>();
        for (int i=0; i < startingLemmings.Count; i++)
        {
            int individualheat = 0;
            int curFeld = startingLemmings[i].currentFeld;
            if(nextToFire.Count>0) foreach (int fire in nextToFire[i])
            {
                if (fire == curFeld) individualheat++;
            }
            results.Add(individualheat);
        }

        return results;
    }
    public List<int> winFields;
    public void initWinFields()
    {
        winFields = new List<int>();
        for (int i = 0; i < currentBoards.Count; i++)
        {
            int Ziel = 0;
            for (int g = 0; g < currentBoards[i].boardTags.Count; g++)
            {
                Debug.Log(currentBoards[i].boardTags[g]);
                if (currentBoards[i].boardTags[g] == "Ziel") Ziel = g;
            }
            winFields.Add(Ziel);
        }
    }
    private List<int> calcDistToWin()
    {
        
        List<int> results = new List<int>();

        for (int i = 0; i < startingLemmings.Count; i++)
        {
            int Ziel = winFields[i];
            int curFeld = startingLemmings[i].currentFeld;

            int xDistance = Math.Abs(curFeld % 5 - Ziel % 5);
            int yDistance = Math.Abs(curFeld / 5 - Ziel / 5);

            results.Add(xDistance + yDistance);
        }

        return results;
    }
}
