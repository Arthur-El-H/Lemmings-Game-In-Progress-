using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public interface IFeld
{
    twoLists BeAt(dragableLemming lemming, twoLists twoLists);
    bool TryFrom(Direction direction);
    bool TryTo(Direction direction);
    Vector2 GetAnchorPoint();
    string GetTag();
}
public enum Direction { Up, Right, Down, Left, None };

public class twoLists
{
    public List<dragableLemming> lemmings;
    public List<Direction> directions;
}
