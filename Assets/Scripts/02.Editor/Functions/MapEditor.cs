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

    private float timer;

    [SerializeField]
    private GameObject[] prefabsOnMouse;

    [SerializeField]
    private GameObject[] prefabsOccupied;

    private GameObject prefabSelected;

    private GameObject instantiatedPrefab;

    List<string> uniquePrefabs;

    Dictionary<string, SavingObject> savingObj;

    private int countOfMouseClick;

    [SerializeField]
    private GameObject[] panels;

    private bool[] panelsBehaviorsAreDone;

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
        MenuPanel,
        SavingPanel
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

        prefabSelected = null;

        instantiatedPrefab = null;

        uniquePrefabs = new List<string>() {"Leon", "Ashley"};
        countOfMouseClick = 0;
        savingObj = new Dictionary<string, SavingObject>();

        timer = 3.0f;

        //for (int i = 0; i < (int)PanelType.SavingPanel; i++)
        //    panelsBehaviorsAreDone[i] = false;

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
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
            Destroy(prefabSelected);

        //if (Input.GetKey("escape"))
        //{
        //    //if(panelsBehaviorsAreDone[(int)PanelType.MenuPanel] == false && timer > 2.0f)
        //    //{
        //    //    panels[(int)PanelType.MenuPanel].SetActive(true);
        //    //    panelsBehaviorsAreDone[(int)PanelType.MenuPanel] = true;
        //    //    timer = 0.0f;
        //    //}
        //    //else if (panelsBehaviorsAreDone[(int)PanelType.MenuPanel] == true && timer > 2.0f)
        //    //{
        //    //    panels[(int)PanelType.MenuPanel].SetActive(false);
        //    //    panelsBehaviorsAreDone[(int)PanelType.MenuPanel] = false;
        //    //    timer = 0.0f;
        //    //}
        //}
    }

    public void MoveUiSlider()
    {
        //if (panelIsShown == true)
        //{
        //    slidingPanel.transform.Translate(new Vector2(-127.0f, 0.0f));
        //    panelIsShown = false;
        //}
        //else if (panelIsShown == false)
        //{
        //    slidingPanel.transform.Translate(new Vector2(127.0f, 0.0f));
        //    panelIsShown = true;
        //}

    }

    public void OnClick()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        var name = currentButton.name;

        if (GameObject.FindWithTag(name)) return;

        if (prefabSelected != null)
            if (prefabSelected.tag != name)
                Destroy(prefabSelected);
       
        prefabSelected = Instantiate(prefabsOnMouse[String2ObjectType(name)], new Vector3(0, 0, 0), Quaternion.identity);
    }

    void OnMouseDown()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 150))
        {
            int cellNum = Pos2CellNum(hit.point);
            Vector3 cellPos = CellNum2Pos(cellNum);

            if (prefabSelected == null) return;

            string tagName = prefabSelected.transform.tag;

            if (GameObject.Find(tagName))
            {
                instantiatedPrefab = Instantiate(prefabsOccupied[String2ObjectType(tagName)], cellPos, Quaternion.identity);

                savingObj.Add(tagName + countOfMouseClick, new SavingObject(countOfMouseClick, instantiatedPrefab, cellPos, Quaternion.identity));
                countOfMouseClick++;
            }
        }
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

    public Vector3 CellNum2Pos(int cellNum)
    {
        int X = cellNum % 100;
        int Z = cellNum / 100;

        return new Vector3(startingPoint.x + 1.0f * X + 0.5f, 0.5f, startingPoint.z + 1.0f * Z + 0.5f);    
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
        string jsonToFile = JsonUtility.ToJson(savingObj, true);
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
}

[System.Serializable]
public class SavingObject
{
    int index;
    public GameObject savingObject;
    public Vector3 objectPosition;
    public Quaternion objectRotation;

    public SavingObject(int index, GameObject savingObject, Vector3 objectPosition, Quaternion objectRotation)
    {
        this.index = index;
        this.savingObject = savingObject;
        this.objectPosition = objectPosition;
        this.objectRotation = objectRotation;
    }
}
