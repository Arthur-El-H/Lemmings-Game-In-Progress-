using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    public GameObject leeresBrett;
    public levelCreator levelCreator;
    List<Board> boards = new List<Board>() ;

    public informationGatherer infoGatherer;

    public List<Board> buildBoards(int boardCount)
    {
        List<Board> built = new List<Board>();
        for (int i = 0; i < boardCount; i++)
        {
            Board board = Instantiate(leeresBrett, getPos(i, boardCount), Quaternion.identity).GetComponent<Board>();
            board.identity = i;
            built.Add(board);
        }
        return built;
    }

    string currentTag;
    public List<Board> buildBoardsForEditing(Save save)
    {
        boards = buildBoards(save.boardTags.Count);
        for (int f = 0; f < save.boardTags.Count; f++ )  //foreach list of strings
        {
            boards[f].zielCount = 1;
            for (int i = 0; i < save.boardTags[f].Count; i++)  //foreach string in specific list
            {
                currentTag = save.boardTags[f][i];
                if (currentTag != "leer") { setPlatzhalter(f, i, currentTag); }  //place in according Platzhalter
            }
        }
        return boards;
    }
    void setPlatzhalter(int boardNumber, int tagNumber, string tag)
    {
        Platzhalter platzhalter = boards[boardNumber].board[tagNumber];
        Destroy(platzhalter.currentField);
        platzhalter.currentField = createFromTag(currentTag, platzhalter.anchorPoint);
        platzhalter.currentField.AddComponent<Dragable>();
        platzhalter.currentField.AddComponent<BoxCollider2D>();
        platzhalter.currentField.GetComponent<Dragable>().myPlatzhalter = platzhalter;
        platzhalter.currentField.GetComponent<Dragable>().feldPalette = GameObject.Find("Feld_Palette").GetComponent<feldPalette>();
        platzhalter.tellBoard(tag);
    }

    public List<Board> buildBoardsWithTags(Save boardsToBuild)
    {
        boards = buildBoards(boardsToBuild.boardTags.Count);
        for (int i = 0; i < boards.Count; i++)
        {
            fillBoard(boardsToBuild.boardTags[i], boardsToBuild.lemmingPos[i], i);
            boards[i].boardTags = boardsToBuild.boardTags[i];  //9.5.
        }
        return boards;
    }

    private void fillBoard(List<string> boardTags, int lemmingPos,  int i)
    {
        for (int f = 0; f < 30; f++)
        {
            boards[i].board[f].currentField = createFromTag(boardTags[f], boards[i].board[f].anchorPoint);
            boards[i].boardFelder.Add(boards[i].board[f].currentField.GetComponent<IFeld>());
        }
        boards[i].lemmingPos = lemmingPos;
        boards[i].lemming.currentFeld = boards[i].lemmingPos;
        boards[i].lemming.position = boards[i].boardFelder[boards[i].lemmingPos].GetAnchorPoint();
        boards[i].lemming.transform.position = boards[i].lemming.position;

    }

    private Vector3 getPos(int identity, int amountOfBoards)
    {
        switch (amountOfBoards)
        {
            case 2: return new Vector3((6 * identity + identity), 1, 0);

            case 4:
                if (identity < 2) return new Vector3(6 * identity + identity - 1, 1, 0);
                else return new Vector3(6 * (identity - 2) + (identity - 2) - 1, -7, 0);


            case 6:
                if (identity < 3) return new Vector3(6 * identity + identity - 1, 1, 0);
                else return new Vector3(6 * (identity - 3) + (identity - 3) - 1, -7, 0);
        }
        return Vector3.zero;
    }

    private GameObject createFromTag(string tag, Vector2 pos)
    {
        switch (tag)
        {
            case "ORUL": return GameObject.Instantiate(levelCreator.ORUl, pos, Quaternion.identity);
            case "ORU": return GameObject.Instantiate(levelCreator.ORU, pos, Quaternion.identity);
            case "ORL": return GameObject.Instantiate(levelCreator.ORL, pos, Quaternion.identity);
            case "RUL": return GameObject.Instantiate(levelCreator.RUL, pos, Quaternion.identity);
            case "OUL": return GameObject.Instantiate(levelCreator.OUL, pos, Quaternion.identity);

            case "OL": return GameObject.Instantiate(levelCreator.OL, pos, Quaternion.identity);
            case "OR": return GameObject.Instantiate(levelCreator.OR, pos, Quaternion.identity);
            case "UL": return GameObject.Instantiate(levelCreator.UL, pos, Quaternion.identity);
            case "UR": return GameObject.Instantiate(levelCreator.UR, pos, Quaternion.identity);

            case "OU": return GameObject.Instantiate(levelCreator.OU, pos, Quaternion.identity);
            case "LR": return GameObject.Instantiate(levelCreator.LR, pos, Quaternion.identity);

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
