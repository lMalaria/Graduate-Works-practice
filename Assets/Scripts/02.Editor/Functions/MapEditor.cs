using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject plane;

    private float planeSizeX;
    private float planeSizeZ;

    private Vector3 startingPoint;

    private int gridSizeX;
    private int gridSizeZ;

    [SerializeField]
    private GameObject[] prefabsOnMouse;

    [SerializeField]
    private GameObject[] prefabsOccupied;

    private GameObject prefabSelected;

    private GameObject instantiatedPrefab;

    List<string> uniquePrefabs;

    List<List<SavingObject>> savingObjs;

    [SerializeField]
    private GameObject rotationUI;

    private GameObject instantiatedUI;

    [SerializeField]
    private GameObject[] panels;

    [SerializeField]
    private Transform canvas;

    private float screenWidth;

    private float durationOfLine;

    private Camera currentCamera;

    [SerializeField]
    private Canvas[] canvases;

    //private GameObject highlightPoint;

    enum TileType
    {
        Empty = 0,
        RegularTile,
        Water,
        Mountain
    }

    private TileType[] world;

    enum ObjectType
    {
        Barrel = 0,
        Fence,
        Wall1,
        Wall2,
        CivilianZombie1,
        CivilianZombie2,
        HoundZombie,
        SawZombie,
        Leon,
        Ashley,
        Merchant,
        Aida
    }

    enum PanelType
    {
        SlidingPanel = 0,
        SavingPanel
    }

    enum CanvasType
    {
        EditorCanvas = 0,
        MenuCanvas
    }

    void Awake()
    {
        gridSizeX = 100;
        gridSizeZ = 100;

        planeSizeX = plane.GetComponent<Renderer>().bounds.size.x;
        planeSizeZ = plane.GetComponent<Renderer>().bounds.size.z;

        startingPoint = new Vector3(plane.transform.position.x - planeSizeX / 2, plane.transform.position.y, plane.transform.position.z - planeSizeZ / 2);

        prefabSelected = null;

        instantiatedPrefab = null;

        uniquePrefabs = new List<string>() {"Leon", "Ashley"};
        savingObjs = new List< List<SavingObject> >();

        screenWidth = Screen.width;
        durationOfLine = 10000.0f;

        currentCamera = Camera.main;

        //메뉴 캔버스는 에디터 모드 시작시 꺼져 있어야 한다.(켜져있으면 겹친다)
        canvases[(int)CanvasType.MenuCanvas].enabled = false;

        //마우스 포지션에 따른 셀 넘버의 위치를 표시 할 녹색 네모
        //highlightPoint = GameObject.CreatePrimitive(PrimitiveType.Quad);
    }

    void Start()
    {
        for (int i = 0; i < gridSizeX + 1; i++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x, startingPoint.y, startingPoint.z + i), new Vector3(startingPoint.x + planeSizeX, startingPoint.y, startingPoint.z + i), new Color(0, 0, 0), durationOfLine);
        }
        for (int j = 0; j < gridSizeZ + 1; j++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z), new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z + planeSizeZ), new Color(0, 0, 0), durationOfLine);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Destroy(prefabSelected);

        if (Input.GetKeyDown("escape"))
        {
            //마우스를 따라다니는 UI 프리팹으로 메뉴창으로 넘어 갈 시 마우스를 따라오는 스크립트로 인해서 메뉴에 나타 남으로 파괴
            if (prefabSelected)
                Destroy(prefabSelected);
            
            // 현재 카메라가 메인 카메라가 아니라면 리턴
            if (currentCamera != Camera.main) return;

            //현재 카메라(메인 카메라)를 안보이게
            currentCamera.enabled = false;

            //에디터시 필요한 UI 캔버스 사라지고 메뉴 UI를 보여주는 캔버스 나타나게
            canvases[(int)CanvasType.EditorCanvas].enabled = false;
            canvases[(int)CanvasType.MenuCanvas].enabled = true;
        }
    }

    public void OnClick()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        var name = currentButton.name;
        var comparedString = "OnMouse(Clone)";

        if (prefabSelected != null)
            if(name.Insert(name.Length,comparedString) != prefabSelected.name)
                   Destroy(prefabSelected);

            prefabSelected = Instantiate(prefabsOnMouse[String2ObjectType(name)], Vector3.zero, Quaternion.identity);
    }

    public void MoveUISlider()
    {
        var comparedPanel = GameObject.Find("SliderShowed");

        if(comparedPanel.transform.position.x < canvas.transform.position.x - screenWidth / 2 )
            panels[(int)PanelType.SlidingPanel].transform.Translate(new Vector2(127.0f, 0.0f));
        else
            panels[(int)PanelType.SlidingPanel].transform.Translate(new Vector2(-127.0f, 0.0f));
    }

    void OnMouseDown()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 150)) return;
        
        int cellNum = Pos2CellNum(hit.point);
        Vector3 cellPos = CellNum2Pos(cellNum);

        if (prefabSelected == null) return;

        string manipulateString = "OnMouse(Clone)";
        string instantiatedObjectName = prefabSelected.name.Remove(prefabSelected.name.Length - manipulateString.Length);

        if (GameObject.Find(prefabSelected.name))

        //Destroy(prefabSelected);

        //if (GameObject.FindWithTag("UI")) return;
        
        instantiatedPrefab = Instantiate(prefabsOccupied[String2ObjectType(instantiatedObjectName)], cellPos, Quaternion.identity);
        //instantiatedUI = Instantiate(rotationUI, cellPos + new Vector3(0, 1, 0), Quaternion.identity);

    }

    public void ResumeButton()
    {
        currentCamera.enabled = true;
        canvases[(int)CanvasType.EditorCanvas].enabled = true;
        canvases[(int)CanvasType.MenuCanvas].enabled = false;
    }

    static int String2ObjectType(string name)
    {
        return (int)Enum.Parse(typeof(ObjectType), name);
    }

    static string ObjectType2String(ObjectType objType)
    {
        return objType.ToString();
    }

    public int Pos2CellNum(Vector3 point)
    {
        return (int)point.x + (int)point.z * gridSizeZ;
    }

    Vector3 LocateToCenter(Vector3 position)
    {
        return position + Vector3.one * 0.5f;
    }

    public Vector3 CellNum2Pos(int cellNum)
    {
        int X = cellNum % 100;
        int Z = cellNum / 100;

        return new Vector3(startingPoint.x + 1.0f * X, 0, startingPoint.z + 1.0f * Z ) + Vector3.one * 0.5f;
    }

    int[] FindNeighborsGrid(Vector3 pos, TileType[] world)
    {
        int cellNum = Pos2CellNum(pos);

        List<int> neighbors = new List<int>() {1, -1, -gridSizeX - 1, -gridSizeX, -gridSizeX + 1, gridSizeX - 1, gridSizeX, gridSizeX + 1};

        if (cellNum % gridSizeX == 0)
            neighbors.RemoveAll((cn) => { return cn == -1 || cn == gridSizeX - 1 || cn == -gridSizeX - 1;});
        if (cellNum % gridSizeX == gridSizeX - 1)
            neighbors.RemoveAll((cn) => { return cn == 1 || cn == gridSizeX + 1 || cn == -gridSizeX + 1;});
        if (cellNum / gridSizeX == 0)
            neighbors.RemoveAll((cn) => { return cn == -gridSizeX - 1 || cn == -gridSizeX || cn == -gridSizeX + 1;});
        if (cellNum / gridSizeX == gridSizeZ - 1)
            neighbors.RemoveAll((cn) => { return cn == gridSizeX - 1  || cn == gridSizeX || cn == gridSizeX + 1 ;});

        for (int i = 0; i < neighbors.Count; i++)
        {
            neighbors[i] += cellNum;
            if (neighbors[i] < 0 || neighbors[i] > gridSizeX * gridSizeZ) neighbors.RemoveAt(i);
        }

        return neighbors.ToArray();
    }

    public void SavaData()
    {
        string jsonToFile = JsonUtility.ToJson(savingObjs, true);
        string filePath = Path.Combine(Application.persistentDataPath, "SaveFile.json");
        File.WriteAllText(filePath, jsonToFile);

        //TreeObstacle treeobstacle = new TreeObstacle(Barrel, SetCellnumtoPos(SetgridcellNum(hit.point)), true);
        //string jsonToFile = JsonUtility.ToJson(treeobstacle, true);
        //string filePath = Path.Combine(Application.dataPath, "Resources/JsonText.json");
        //File.WriteAllText(filePath, jsonToFile);
    }

    public void LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "SaveFile.json");
        string jsonFromFile = File.ReadAllText(filePath);

        //PlacedObject PlacedObj = JsonUtility.FromJson<PlacedObject>(jsonFromFile);
        //Instantiate(PlacedObj.ObjSaved, PlacedObj.ObjPos, Quaternion.identity);
    }

    public void OptionButton()
    {
        
    }

    public void ExitButtion()
    {
        SceneManager.LoadScene("01.Title");
    }
}

[System.Serializable]
struct SavingObject
{
    public GameObject savingObject;
    public Vector3 objectPosition;
    public Quaternion objectRotation;

    public SavingObject(GameObject savingObject, Vector3 objectPosition, Quaternion objectRotation)
    {
        this.savingObject = savingObject;
        this.objectPosition = objectPosition;
        this.objectRotation = objectRotation;
    }
}
