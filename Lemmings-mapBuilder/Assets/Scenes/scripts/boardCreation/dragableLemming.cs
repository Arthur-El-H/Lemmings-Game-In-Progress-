using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableLemming : MonoBehaviour
{
    private bool isDragging = false;
    public Vector2 position; //original Position
    public Board board;

    //dragging
    #region
    void OnMouseDown()
    {
        isDragging = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        this.gameObject.layer = 2;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        this.gameObject.layer = 0;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        RaycastHit2D Feld;
        Platzhalter platzhalter = null;
        Feld = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 0), 1000f, 1024);
        if (Feld) { platzhalter = Feld.transform.GetComponent<Platzhalter>(); }

        if (platzhalter && (platzhalter.board.identity == board.identity))
        {
            platzhalter.getLemming(this);
            position = platzhalter.anchorPoint;
            transform.position = position;
        }

        else
        {
            transform.position = position;
        }
    }
    public void lemmingUpdate()
    {
        if (isDragging) {
            Vector2 helper = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(helper.x, helper.y, 11);
            }
    }
    #endregion

    //testingState
    #region
    public Direction savedStep = Direction.None;
    public Vector2 currentPos;
    public IFeld Feld;
    public int currentFeld;

    public void moveTo(Vector2 target)
    {
        transform.position = target;
    }

    void Start()
    {
        board = this.transform.parent.GetComponent<Board>();
        position = transform.position;
        savedStep = Direction.None;
    }
    #endregion
}
