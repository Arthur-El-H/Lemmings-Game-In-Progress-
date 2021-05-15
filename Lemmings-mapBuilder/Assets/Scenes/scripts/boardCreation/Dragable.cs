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
        feldPalette.isDragging = true;
        feldPalette.currentTag = this.tag;

        if ((myPlatzhalter != null))
        {
            //if (this.tag == "Ziel") { myPlatzhalter.transform.parent.GetComponent<Board>().zielCount--; }
            //myPlatzhalter.respawnEmpty();
            //feldPalette.currentDragged = this.gameObject;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                feldPalette.currentDragged = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y, 21), Quaternion.identity);
            }

            else
            {
                if (this.tag == "Ziel") { myPlatzhalter.transform.parent.GetComponent<Board>().zielCount--; }
                myPlatzhalter.respawnEmpty();
                feldPalette.currentDragged = this.gameObject;
                if (this.tag != "leer") { feldPalette.decreaseComplexity(); }
            }
        }

        else
        {
            feldPalette.currentDragged = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y, 21), Quaternion.identity);
            if (this.tag == "Flamme" || this.tag == "Ziel") { feldPalette.informGatherer(); }
        }

        feldPalette.currentDragged.GetComponent<SpriteRenderer>().sortingOrder = 3;
        feldPalette.gameObject.layer = 2;

        changePaletteVisibility();
    }

    private void OnMouseUp()
    {
        RaycastHit2D Feld;
        Platzhalter platzhalter = null;
        feldPalette.isDragging = false;
        feldPalette.gameObject.layer = 0;
        feldPalette.currentDragged.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Dragable fPCurrentDragged = feldPalette.currentDragged.GetComponent<Dragable>();

        changePaletteVisibility();

        Feld = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 0), 1000f, 1024);
        if (Feld) { platzhalter = Feld.transform.GetComponent<Platzhalter>(); }

        if (platzhalter)
        {
            string currentFieldTag;
            if (platzhalter.currentField == null) currentFieldTag = "leer";
            else currentFieldTag = platzhalter.currentField.tag;

            if (currentFieldTag == "leer" && feldPalette.currentTag != "leer") { feldPalette.increaseComplexity(); }
            if (currentFieldTag != "leer" && feldPalette.currentTag == "leer") { feldPalette.decreaseComplexity(); }

            if (currentFieldTag == "Ziel" && feldPalette.currentTag != "Ziel") platzhalter.transform.parent.GetComponent<Board>().zielCount--;
            if (currentFieldTag != "Ziel" && feldPalette.currentTag == "Ziel") platzhalter.transform.parent.GetComponent<Board>().zielCount++;

            Destroy(platzhalter.currentField);
            platzhalter.currentField = feldPalette.currentDragged;
            platzhalter.currentField.transform.position = platzhalter.anchorPoint;
            platzhalter.tellBoard(feldPalette.currentTag);

            fPCurrentDragged.myPlatzhalter = platzhalter;
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
