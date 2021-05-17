using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Platzhalter Feld01;
    #region
    public Platzhalter Feld02;
    public Platzhalter Feld03;
    public Platzhalter Feld04;
    public Platzhalter Feld05;
    public Platzhalter Feld11;
    public Platzhalter Feld12;
    public Platzhalter Feld13;
    public Platzhalter Feld14;
    public Platzhalter Feld15;
    public Platzhalter Feld21;
    public Platzhalter Feld22;
    public Platzhalter Feld23;
    public Platzhalter Feld24;
    public Platzhalter Feld25;
    public Platzhalter Feld31;
    public Platzhalter Feld32;
    public Platzhalter Feld33;
    public Platzhalter Feld34;
    public Platzhalter Feld35;
    public Platzhalter Feld41;
    public Platzhalter Feld42;
    public Platzhalter Feld43;
    public Platzhalter Feld44;
    public Platzhalter Feld45;
    public Platzhalter Feld51;
    public Platzhalter Feld52;
    public Platzhalter Feld53;
    public Platzhalter Feld54;
    #endregion
    public Platzhalter Feld55;
    public List<Platzhalter> board;
    public int identity;

    public List<string> boardTags;
    public List<IFeld> boardFelder;

    public dragableLemming lemming;
    public int lemmingPos;
    public int zielCount;

    public void Awake()
    {
        board.Add(Feld01);
        #region
        board.Add(Feld02);
        board.Add(Feld03);
        board.Add(Feld04);
        board.Add(Feld05);
        board.Add(Feld11);
        board.Add(Feld12);
        board.Add(Feld13);
        board.Add(Feld14);
        board.Add(Feld15);
        board.Add(Feld21);
        board.Add(Feld22);
        board.Add(Feld23);
        board.Add(Feld24);
        board.Add(Feld25);
        board.Add(Feld31);
        board.Add(Feld32);
        board.Add(Feld33);
        board.Add(Feld34);
        board.Add(Feld35);
        board.Add(Feld41);
        board.Add(Feld42);
        board.Add(Feld43);
        board.Add(Feld44);
        board.Add(Feld45);
        board.Add(Feld51);
        board.Add(Feld52);
        board.Add(Feld53);
        board.Add(Feld54);
        #endregion
        board.Add(Feld55);

        for (int i = 0; i < 30; i++)
        {
            board[i].boardPos = i;
            boardTags.Add("leer");
        }
        
        boardFelder = new List<IFeld>();
        lemmingPos = 25;
    }

    public void getEntry(int identity, string tag)
    {
        boardTags[identity] = tag;
    }

    public void changeLemmingPos(int newPos)
    {
        lemmingPos = newPos;
    }

    public void delete()
    {
         foreach (Platzhalter platzhalter in board) {
             if (platzhalter.currentField)
             {
                Destroy(platzhalter.currentField);
             }
             Destroy(platzhalter);
         }
         Destroy(this.gameObject);
    }
}
