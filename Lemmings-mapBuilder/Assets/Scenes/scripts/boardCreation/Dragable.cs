using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Dragable : MonoBehaviour
{
    public feldPalette feldPalette;

    public Platzhalter myPlatzhalter;

    private bool visible = true;

    void OnMouseDown()
    {
        Debug.Log("you are clicking me");
        if ( ! (myPlatzhalter == null))
        {
            myPlatzhalter.respawnEmpty();
            feldPalette.currentDragged = this.gameObject;
        }

        else
        {
            feldPalette.currentDragged = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y, 21), Quaternion.identity);
        }

        feldPalette.currentDragged.GetComponent<SpriteRenderer>().sortingOrder = 3;
        feldPalette.gameObject.layer = 2;

        changePaletteVisibility();

        feldPalette.isDragging = true;
        feldPalette.currentTag = this.tag; 
    }

    private void OnMouseUp()
    {
        RaycastHit2D Feld;
        Platzhalter platzhalter = null;
        feldPalette.isDragging = false;
        feldPalette.gameObject.layer = 0;
        feldPalette.currentDragged.GetComponent<SpriteRenderer>().sortingOrder = 1;

        changePaletteVisibility();

        Feld = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 0), 1000f, 1024);
        if (Feld) { platzhalter = Feld.transform.GetComponent<Platzhalter>(); }

        if (platzhalter)
        {
            Destroy(platzhalter.currentField);
            platzhalter.currentField = feldPalette.currentDragged;
            platzhalter.currentField.transform.position = platzhalter.anchorPoint;
            platzhalter.tellBoard(feldPalette.currentTag);

            if (myPlatzhalter == null)
            {
                feldPalette.currentDragged.GetComponent<Dragable>().myPlatzhalter = platzhalter;
            }

            else
            {
                myPlatzhalter = platzhalter;
            }
        }
        else { Destroy(feldPalette.currentDragged); }
    }

    private void changePaletteVisibility()
    {
        if (visible)
        {
            feldPalette.gameObject.layer = 2;
            feldPalette.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
            foreach (Transform trans in feldPalette.gameObject.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 2;
                trans.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
            }
            visible = false;
        }
        else
        {
            feldPalette.gameObject.layer = 0;
            feldPalette.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            foreach (Transform trans in feldPalette.gameObject.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 0;
                trans.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            visible = true;
        }
    }
}
