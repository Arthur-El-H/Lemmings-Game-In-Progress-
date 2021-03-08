using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platzhalter : MonoBehaviour
{
    public Board board; //Platzhalters board
    public GameObject currentField = null;
    public feldPalette feldPalette;
    public Vector2 anchorPoint;
    public int boardPos;

    public void tellBoard (string tag)
    {
        board.getEntry(boardPos, tag);
    }
    public void getLemming (dragableLemming lemming)
    {
        lemming.transform.position = anchorPoint;
        board.changeLemmingPos(boardPos);
    }
    void Awake()
    {
        board = this.transform.parent.GetComponent<Board>();
        anchorPoint = transform.position;
    }
}
