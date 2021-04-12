using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class mapListSaveManager : MonoBehaviour
{
    List<string> currentMapList = new List<string>();
    public List<string> getCurrentMapList()
    {
        return currentMapList;
    }

    public void Awake()
    {
        loadList();
    }

    public void loadList()
    {
        if (!File.Exists(Application.persistentDataPath + "listOfMaps_saveFile.save")) { return; }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "listOfMaps_saveFile.save", FileMode.Open);
        listOfMapsSave save = (listOfMapsSave)bf.Deserialize(file);
        currentMapList = save.listOfMaps;
        file.Close();
    }

    public void addToList(string mapName)
    {
        currentMapList.Add(mapName);
        listOfMapsSave mapSave = new listOfMapsSave(currentMapList);
        saveList(mapSave);
    }

    public void saveList (listOfMapsSave save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "listOfMaps_saveFile.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game Saved");
    }
}
