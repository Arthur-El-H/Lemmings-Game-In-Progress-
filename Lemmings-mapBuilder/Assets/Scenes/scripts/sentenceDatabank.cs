using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sentenceDatabank : MonoBehaviour
{
    public string increasedComplexityOne = "Oh no this is getting a bit too complex!";
    public string decreasedComplexityOne = "Well that is a map a lemming can get through!";
    public string increasedComplexityTwo = "Why am I even talking? The map is getting complex again...";
    public string targetGrabbed = "Oh yes, put it next to one of the lemmings!";
    public string fireGrabbed = "uh, do we really need that?";
    public string lemmingGrabbed = "Be careful, lemmings dont like heights.";
    public string loss = "Oh no, poor little thing...";
    public string win  = "They did it! Gotta love those little rascals.";

    public string buildingTip = "How about a map full of food?";
    public string tutorialStart = "Well, before saving the map you have to show it is possible.";

    public string createExpl = "Click here to create your own map. You can save it afterwards and challenge friends";
    public string closeExpl = "I don't think I have to explain that...";
    public string tutExpl = "Click one of these to play them. I recommend the tutorials.";
    public string testExpl = "Play your map. If you can solve it, you can save it.";
    public string saveExpl = "You did it. You now just need a catchy name. Like 'wolf canyon'.";
    public string changeExpl = "You can change the map to make it easier. Or harder, but why would you do that?";
    public string tryAgainExpl = "Do you think you can do it next time?";
    public string startExpl = "Well this one should be obvious.";


    //mapBuilding idles
    public string idle1 = "I wonder how difficult it would be for me, to identify impossible maps...";
    public string idle2 = "Too bad you can't place more than one goals...";


}
