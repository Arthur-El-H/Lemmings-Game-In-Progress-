using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startscreen : IState
{

    Unit owner;

    public Startscreen(Unit owner) { this.owner = owner; }

    public void Enter()
    {
        enableButtons(true);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        enableButtons(false);
    }

    void enableButtons(bool enable)
    {
        owner.createBtn.SetActive(enable);
        owner.closeBtn.SetActive(enable);
        owner.loadMap.SetActive(enable);
        owner.tutBtn.SetActive(enable);
        owner.scrollViewMaps.SetActive(enable);
    }
}