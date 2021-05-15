using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableLemming : MonoBehaviour
{
    private bool isDragging = false;
    public Vector2 position; //original Position
    public Board board;
    Vector2 helper = new Vector2(0, 0);
    private Animator anim;
    informationGatherer infoGatherer;

    //dragging
    #region
    void OnMouseDown()
    {
        infoGatherer.lemmingGrabbed();
        anim.Play("pick_up_lemming");
        isDragging = true;
        //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        this.gameObject.layer = 2;
    }

    private void OnMouseUp()
    {
        anim.Play("creation_lemming_idle");
        isDragging = false;
        this.gameObject.layer = 0;
        //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

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
    #endregion

    public void lemmingUpdate()
    {
        if (isDragging) 
        {
            helper = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(helper.x, helper.y, 11);
        }

        if(isMoving)
        {
            moveTowardsTarget();
        }
    }


    //testingState
    #region
    public Direction savedStep = Direction.None;
    public Direction currentDirection;
    public Vector2 currentPos;
    public IFeld Feld;
    Vector2 target;
    private bool isMoving;
    public int currentFeld;
    private float speed = 1.7f;
    int framesToWait;

    public void moveToTarget()
    {
        target = Feld.GetAnchorPoint();
        isMoving = true;

        switch (currentDirection)
        {
            case Direction.Left: anim.Play("startLeft"); break;
            case Direction.Right: anim.Play("startRight"); break;
            case Direction.Down: anim.Play("StartForward"); break;
            case Direction.Up: anim.Play("startBackwards"); break;
        }
        framesToWait = 120;
    }

    public void moveTowardsTarget()
    {
        if(framesToWait > 0) { framesToWait--; return; }
        //Debug.Log(framesToWait);
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if((Vector2)transform.position == target) 
        { 
            isMoving = false;
            switch (currentDirection)
            {
                case Direction.Left: anim.Play("endLeft"); break;
                case Direction.Right: anim.Play("endRight"); break;
                case Direction.Down: anim.Play("endForward"); break;
                case Direction.Up: anim.Play("endBackwards"); break;
            }
        }
    }

    void Start()
    {
        board = this.transform.parent.GetComponent<Board>();
        position = transform.position;
        savedStep = Direction.None;
        anim = gameObject.GetComponent<Animator>();
        infoGatherer = GameObject.Find("Manager").GetComponent<informationGatherer>();
    }
    #endregion
}
