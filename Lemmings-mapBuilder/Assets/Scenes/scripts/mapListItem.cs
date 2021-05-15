using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapListItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Unit owner;

    public void Start()
    {
        owner = GetComponentInParent<scrollViewList>().owner;
    }
    public void loadMe()
    {
        string name = this.GetComponent<UnityEngine.UI.Text>().text;
        owner.LoadMap(name);        
    }

}
