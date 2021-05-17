using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platzhalter : MonoBehaviour
{
    public Board board; //Platzhalters board
    public GameObject currentField = null;
    private GameObject leer;
    public feldPalette feldPalette;
    public Vector2 anchorPoint;
    public int boardPos;

    public void tellBoard (string tag)
    {
        board.getEntry(boardPos, tag);
    }
    public void getLemming (dragableLemming lemming)
    {
        board.changeLemmingPos(boardPos);
    }

    public void respawnEmpty()
    {
        currentField = Instantiate(leer, anchorPoint, Quaternion.identity);
        tellBoard("leer");
    }

    void Awake()
    {
        board = this.transform.parent.GetComponent<Board>();
        anchorPoint = transform.position;
        leer = GameObject.Find("Manager").GetComponent<levelCreator>().leer;
    }
}
