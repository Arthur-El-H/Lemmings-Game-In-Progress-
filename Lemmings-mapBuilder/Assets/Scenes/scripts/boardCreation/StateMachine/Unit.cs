using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public GameObject leeresBrett;


    public GameObject numberPalette;
    public GameObject chooseButton;
    public boardNumberChoosing Board_2;
    public boardNumberChoosing Board_4;
    public boardNumberChoosing Board_6;
    public boardNumberChoosing Board_8;
    public GameObject FeldPalette;
    public GameObject StartScreen;
    public GameObject howManyBoards;
    public GameObject Win;
    public GameObject fireLoss;
    public GameObject dropLoss;
    public GameObject tryAgain;
    public GameObject newMap;
    public GameObject bedingung;
    public GameObject closeTip;
    public GameObject trappedLemmingLoss;

    public GameObject loadMap;
    public GameObject scrollViewMaps;
    public GameObject createBtn;
    public GameObject closeBtn;
    public GameObject tutBtn;
    public GameObject changeMapBtn;
    public GameObject testBtn;

    public GameObject mapNameField;
    public GameObject saveMap;
    public GameObject saveBtn;

    public mapListSaveManager mapListSaveManager;
    public feldPalette feldPalette;
    public GameObject mapListItem;
    public GameObject scrollViewMaps_Content;
    public GameObject lossReason;

    public BoardBuilder BoardBuilder;
    public MoveManager MoveManager;

    public BoardBuilding currentBoardBuilding;

    List<Board> boards;
    public void setBoardsToSave(List<Board> newBoards)
    {
        boards = newBoards;
    } //used by winstate

    Save currentSave;
    public void setCurrentSave(Save save) { currentSave = save; }


    private void Awake()
    {
    }
    public void Start()
    {
        stateMachine.ChangeState(new StartState(this, StartScreen));
    }

    public void Update()
    {
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }    
    }

    public void goToStartScreen()
    {
        stateMachine.ChangeState(new Startscreen(this));
    }

    public void returnToBuilding()
    {
        stateMachine.ChangeState(new BoardBuilding(this, currentSave.boardTags.Count, currentSave));
    }

    public void goToTesting()
    {
        stateMachine.ChangeState(new TestingState(this, currentBoardBuilding.boards));
    }
    public void LoadMap( string name)
    {
        Debug.Log("loading " + name);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "." + name + ".save", FileMode.Open);
        Save save = (Save)bf.Deserialize(file);
        file.Close();
        stateMachine.ChangeState(new PlayState(this, save));
    }
    public Save CreateSave(string name, List<Board> boards)
    {
        Save save = new Save();
        save.boardTags = new List<List<string>> ();
        save.lemmingPos = new List<int>();
        save.mapName = name;
        for (int i = 0; i < boards.Count; i++)
        {
            save.boardTags.Add(boards[i].boardTags);
            save.lemmingPos.Add(boards[i].lemmingPos);
        }
        return save;
    }
    public void SaveGame()
    {
        string name = mapNameField.GetComponent<UnityEngine.UI.InputField>().text;
        if(name == "") { Debug.Log("map needs a name!"); }
        else
        {
            Save save = CreateSave(name, boards);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "." + name + ".save");
            bf.Serialize(file, save);
            file.Close();
            mapListSaveManager.addToList(name);
            sendToScrollView(name);
            Debug.Log("Game Saved");
            goToStartScreen();
        }
    }
    public Save boardsToSave(List<Board> boards)
    {
        Save save = new Save();
        save.boardTags = new List<List<string>>();
        save.lemmingPos = new List<int>();

        foreach(Board board in boards)
        {
            save.boardTags.Add(board.boardTags);
            save.lemmingPos.Add(board.lemmingPos);
        }

        return save;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void createMap()
    {
        this.stateMachine.ChangeState(new BoardChoosing(this));
    }
    public void fillScrollView()
    {
        mapListSaveManager.loadList();
        foreach (string mapName in mapListSaveManager.getCurrentMapList())
        {
            sendToScrollView(mapName);
        }
    }
    void sendToScrollView(string name)
    {
        mapListItem = Instantiate(mapListItem);
        mapListItem.GetComponent<UnityEngine.UI.Text>().text = name;
        mapListItem.transform.SetParent(scrollViewMaps_Content.transform);
    }
}