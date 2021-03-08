using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardNumberChoosing : MonoBehaviour
{
    private bool beingChosen;

    public void choose()
    {
        beingChosen = true;
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
    }

    public void unChoose()
    {
        beingChosen = false;
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }
}
