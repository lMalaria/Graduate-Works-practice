using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine;
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

    [SerializeField]
    private GameObject slidingPanel;

    [SerializeField]
    private GameObject savingPanel;

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

    [SerializeField]
    private EditorCamera cameraMovementControl;

    void Awake()
    {
        gridSizeX = 100;
        gridSizeZ = 100;

        planeSizeX = plane.GetComponent<Renderer>().bounds.size.x;
        planeSizeZ = plane.GetComponent<Renderer>().bounds.size.z;

        startingPoint = new Vector3(plane.transform.position.x - planeSizeX / 2, plane.transform.position.y, plane.transform.position.z - planeSizeZ / 2);
    }

    void Start()
    {
        for (int i = 0; i < gridSizeX + 1; i++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x, startingPoint.y, startingPoint.z + i), new Vector3(startingPoint.x + planeSizeX, startingPoint.y, startingPoint.z + i), new Color(0, 0, 0), 1000.0f);
        }
        for (int j = 0; j < gridSizeZ + 1; j++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z), new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z + planeSizeZ), new Color(0, 0, 0), 1000.0f);
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void UiSlider()
    {
        //GameObject panelBePopped = GameObject.Find("MovingPanel");
        bool panelIsShown = true;

        if (panelIsShown == true)
        {
            slidingPanel.transform.Translate(new Vector2(-127.0f, 0.0f));
            panelIsShown = false;
        }
        else if (panelIsShown == false)
        {
            slidingPanel.transform.Translate(new Vector2(127.0f, 0.0f));
            panelIsShown = true;
        }
    }

    public void OnClick()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        var name = currentButton.name;

        if (GameObject.FindWithTag(name)) return;

        if (prefabSelected != null)
            if (prefabSelected.tag != name)
                Destroy(prefabSelected);
       
        prefabSelected = Instantiate(prefabsOnMouse[ConvertString2ObjectType(name)], new Vector3(0, 0, 0), Quaternion.identity);
    }

    //void OnMouseOver()
    //{
    //    Ray ray;
    //    RaycastHit hit;
    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out hit, 150))
    //    {
    //        int cellNum = ChangePos2CellNum(hit.point);
    //        Vector3 cellPos = ChangeCellNum2Pos(cellNum);
    //        Rect rectDrawed = new Rect(cellPos.x - 0.5f , cellPos.z - 0.5f,planeSizeX/gridSizeX ,planeSizeZ/gridSizeZ );

    //    }
    //}

    void OnMouseDown()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 150))
        {
            int cellNum = ChangePos2CellNum(hit.point);

            for (int i = 0; i < (int)ObjectType.Ashley; i++)
            {
                string tagName = ConvertObjectType2String((ObjectType)i);

                if (GameObject.FindWithTag(tagName))
                    Instantiate(prefabsOccupied[i], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            } 
        }
    }

    static int ConvertString2ObjectType(string name)
    {
        return (int)Enum.Parse(typeof(ObjectType), name);
    }

    static string ConvertObjectType2String(ObjectType objType)
    {
        return objType.ToString();
    }

    public int ChangePos2CellNum(Vector3 point)
    {
        return (int)point.x + (int)point.z * gridSizeZ;
    }

    //셀 넘버를 포지션으로 변환하는 메소드
    public Vector3 ChangeCellNum2Pos(int cellNum)
    {
        int X = cellNum % 100;
        int Z = cellNum / 100;

        return new Vector3(startingPoint.x + 1.0f * X + 0.5f, 0.5f, startingPoint.z + 1.0f * Z + 0.5f);    
    }

    int[] FindNeighborGrid(Vector3 pos, TileType[] world)
    {
        int cellNum = ChangePos2CellNum(pos);

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

        }

        return neighbors.ToArray();
    }

    public void SavaData()
    {

        //TreeObstacle treeobstacle = new TreeObstacle(Barrel, SetCellnumtoPos(SetgridcellNum(hit.point)), true);

        //string jsonToFile = JsonUtility.ToJson(treeobstacle, true);

        //string filePath = Path.Combine(Application.dataPath, "Resources/JsonText.json");

        //File.WriteAllText(filePath, jsonToFile);
    }

    public void LoadData()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/JsonText.json");

        string jsonFromFile = File.ReadAllText(filePath);

        PlacedObject PlacedObj = JsonUtility.FromJson<PlacedObject>(jsonFromFile);

        Instantiate(PlacedObj.ObjSaved, PlacedObj.ObjPos, Quaternion.identity);
    }
}

[System.Serializable]
public class PlacedObject
{
    public int[] index = new int[100];
    public GameObject ObjSaved;
    public Vector3 ObjPos;
}
