using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Dragable : MonoBehaviour
{
    public feldPalette feldPalette;

    void OnMouseDown()
    {
        feldPalette.currentDragged = Instantiate(this.gameObject, new Vector3 (transform.position.x, transform.position.y, 21), Quaternion.identity) ;
        Destroy(feldPalette.currentDragged.GetComponent<BoxCollider2D>());
        feldPalette.gameObject.layer = 2;

        foreach (Transform trans in feldPalette.gameObject.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 2;
            trans.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        }

        feldPalette.isDragging = true;
        feldPalette.currentTag = this.tag; 
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUP");
        RaycastHit2D Feld;
        Platzhalter platzhalter = null;

        Debug.Log(transform.position);

        Feld = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 0), 1000f, 1024);
        if (Feld) { platzhalter = Feld.transform.GetComponent<Platzhalter>(); }
        
            if(platzhalter)
            {
                    Destroy(platzhalter.currentField);
                    platzhalter.currentField = feldPalette.currentDragged;
                    platzhalter.currentField.transform.position = platzhalter.anchorPoint;
                    platzhalter.tellBoard(feldPalette.currentTag);

                    feldPalette.currentDragged = null;
                    feldPalette.isDragging = false;
                    feldPalette.gameObject.layer = 0;
                    foreach (Transform trans in feldPalette.gameObject.GetComponentsInChildren<Transform>(true))
                    {
                        trans.gameObject.layer = 0;
                        trans.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
            }
        
            else
            {
                    Destroy(feldPalette.currentDragged);

                    feldPalette.currentDragged = null;
                    feldPalette.isDragging = false;
                    feldPalette.gameObject.layer = 0;
                    foreach (Transform trans in feldPalette.gameObject.GetComponentsInChildren<Transform>(true))
                    {
                        trans.gameObject.layer = 0;
                        trans.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
            }
    }
}
