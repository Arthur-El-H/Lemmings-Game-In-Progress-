using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H : MonoBehaviour, IFeld
{
    public Vector2 GetAnchorPoint()
    {
        return transform.position;
    }
    public twoLists BeAt(dragableLemming lemming, twoLists twoLists)
    {
        twoLists.lemmings.Add(lemming);
        twoLists.directions.Add(Direction.Up);
        return twoLists;
    }

    public bool TryFrom(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return true;
            case Direction.Down: return true;
            case Direction.Left: return true;
            case Direction.Right: return true;
        }
        return true;
    }
    public string GetTag()
    {
        return gameObject.tag;
    }
    public bool TryTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return true;
            case Direction.Down: return true;
            case Direction.Left: return true;
            case Direction.Right: return true;
        }
        return true;
    }
}
