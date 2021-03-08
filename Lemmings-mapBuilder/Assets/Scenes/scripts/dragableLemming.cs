using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableLemming : MonoBehaviour
{
    private bool isDragging = false;
    public Vector2 position; //original Position
    public Vector2 currentPos;
    public Board board;

    public IFeld Feld;
    public int currentFeld;

    public Direction savedStep = Direction.None;

    void OnMouseDown()
    {
        isDragging = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        this.gameObject.layer = 2;
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUP");
        RaycastHit2D Feld;
        Platzhalter platzhalter = null;

        Feld = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 0), 1000f, 1024);
        if (Feld) { platzhalter = Feld.transform.GetComponent<Platzhalter>(); }
        this.gameObject.layer = 0;

        if (platzhalter && (platzhalter.board.identity == board.identity))
        {
            platzhalter.getLemming(this);
            position = platzhalter.anchorPoint;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

            isDragging = false;
            platzhalter = null;
        }

        else
        {
            isDragging = false;
            transform.position = position;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void moveTo(Vector2 target)
    {
        transform.position = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        board = this.transform.parent.GetComponent<Board>();
        position = transform.position;
        savedStep = Direction.None;
    }

    // Update is called once per frame
    public void lemmingUpdate()
    {
        if(isDragging) { transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3 (0,0,10); }
    }
}
