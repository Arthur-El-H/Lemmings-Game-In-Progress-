using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class informationGatherer : MonoBehaviour        //Gathers information for the lemming_oldest
{
    public oldest oldest;
    public sentenceDatabank databank;

    int complexityOne = 0;
    int complexityBorder = 0;

    oldest_state currentState;
    List<Board> currentBoards;
    int complexity; //amount of fields that arent zero
    public void increaseComplexity() 
    { 
        complexity++;
        Debug.Log((complexity));
        Debug.Log((complexity == complexityBorder) + "  " + (complexityOne == 0));
        if ((complexity == complexityBorder) && (complexityOne == 0)) { oldest.getMessage(databank.increasedComplexityOne); complexityOne = 1; }
        else if (complexity == complexityBorder && complexityOne == 2){ oldest.getMessage(databank.increasedComplexityTwo); complexityOne = 3; }
    } //informed by feldPalette
    public void decreaseComplexity() 
    { 
        complexity--; 
        if (complexity < complexityBorder && complexityOne == 1) { oldest.getMessage(databank.decreasedComplexityOne); complexityOne = 2; }
    } //informed by feldPalette
    public void sendState(oldest_state newState, List<Board> boards = null)  //informed by Boardbuilder (Playing, Building), by Winstate(Win), by LossState (Loss)
    {
        currentState = newState;
        if(boards != null) { currentBoards = boards; complexityBorder = currentBoards.Count * 8;}
        if (newState == oldest_state.Building) { oldest.getMessage(databank.buildingTip); }
        if (newState == oldest_state.Playing)  { Debug.Log("Play"); }
        if (newState == oldest_state.Testing)  { Debug.Log("Testing"); }
        if (newState == oldest_state.TestingAgain) { Debug.Log("TestingAgain"); }
        if (newState == oldest_state.PlayingTutorial) { Debug.Log("PlayingTut"); }
        if (newState == oldest_state.PlayingAgain) { Debug.Log("PlayingAgain"); }
    }
    public void lemmingGrabbed()        // informed by dragablelemming
    {
        oldest.getMessage(databank.lemmingGrabbed);
    }
    public void fireGrabbed()           // informed by feldPalette
    {
        oldest.getMessage(databank.fireGrabbed);
    }
    public void targetGrabbed()         // informed by Feldpalette
    {
        oldest.getMessage(databank.targetGrabbed);
    }
    public void sendLoss(string reason) // informed by Movemanager
    {
        oldest.getMessage(databank.loss);
    }
    public void sendWin()               // informed by Movemanager
    {
        oldest.getMessage(databank.win);
    }
    public void retrieveHeat(List<int> heat) // informed by Movemanager
    {
        int result = 0;
        foreach(int i in heat)
        {
            result += i;
        }
        Debug.Log("current heat is " + result);
    }
    public void retrieveWinDistances(List <int> winDistances)// informed by Movemanager
    {
        int result = 0;
        foreach (int i in winDistances)
        {
            result += i;
        }
        Debug.Log("current distance to win is " + result);
    }

    public void hoveringOverButton(string button)
    {
        switch (button)
        {
            case "create": oldest.getMessage(databank.createExpl); break;
            case "exit": oldest.getMessage(databank.closeExpl); break;
            case "scrollView": oldest.getMessage(databank.tutExpl); break;
            case "test": oldest.getMessage(databank.testExpl); break;
            case "save": oldest.getMessage(databank.saveExpl); break;
            case "change": oldest.getMessage(databank.changeExpl); break;
            case "tryAgain": oldest.getMessage(databank.tryAgainExpl); break;
            case "start": oldest.getMessage(databank.startExpl); break;

        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
