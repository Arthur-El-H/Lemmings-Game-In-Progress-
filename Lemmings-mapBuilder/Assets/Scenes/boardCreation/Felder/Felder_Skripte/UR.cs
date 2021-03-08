using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UR : MonoBehaviour, IFeld
{
    public twoLists BeAt(dragableLemming lemming, twoLists twoLists)
    {
        switch (lemming.savedStep)
        {
            case Direction.Up:
                twoLists.lemmings.Add(lemming); twoLists.directions.Add(Direction.Up);
                lemming.savedStep = Direction.None; return twoLists;

            case Direction.Down:
                twoLists.lemmings.Add(lemming); twoLists.directions.Add(Direction.Down);
                lemming.savedStep = Direction.None; return twoLists;

            case Direction.Right:
                twoLists.lemmings.Add(lemming); twoLists.directions.Add(Direction.Right);
                lemming.savedStep = Direction.None; return twoLists;

            case Direction.Left:
                twoLists.lemmings.Add(lemming); twoLists.directions.Add(Direction.Left);
                lemming.savedStep = Direction.None; return twoLists;
        }
        return twoLists;
    }

    public Vector2 GetAnchorPoint()
    {
        return transform.position;
    }
    public string GetTag()
    {
        return gameObject.tag;
    }
    public bool TryFrom(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return true;
            case Direction.Down: return false;
            case Direction.Left: return true;
            case Direction.Right: return false;
        }
        return false;
    }

    public bool TryTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return true;
            case Direction.Down: return false;
            case Direction.Left: return true;
            case Direction.Right: return false;
        }
        return false;
    }
}
