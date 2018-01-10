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

    [SerializeField]
    private Button[] buttonPrefabs;

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

    public void OnClick()
    {
        for (int i = 0; i < (int)ObjectType.Ashley; i++)
            Destroy(GameObject.Find(prefabsOnMouse[i].name + "(Clone)"));

        var currentButton = EventSystem.current.currentSelectedGameObject;
        var name = currentButton.name;

        if (GameObject.Find(name + "OnMouse(Clone)")) return;
            Instantiate(prefabsOnMouse[ConvertString2ObjectType(name)], new Vector3(0, 0, 0), Quaternion.identity);
    }

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

    }

    void Update()
    {
        for (int i = 0; i < gridSizeX + 1; i++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x, startingPoint.y, startingPoint.z + i), new Vector3(startingPoint.x + planeSizeX, startingPoint.y, startingPoint.z + i), new Color(0, 0, 0));
        }
        for (int j = 0; j < gridSizeZ + 1; j++)
        {
            Debug.DrawLine(new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z), new Vector3(startingPoint.x + j, startingPoint.y, startingPoint.z + planeSizeZ), new Color(0, 0, 0));
        }
    }

    void FixedUpdate()
    {

    }

    void OnMouseDown()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 150))
        {
            //for(int i = 0; i < 100; i++)
            //{
            //    if (isPrefabOnMouse[i] == true)
            //    {
            //        if (isPlaced[SetgridcellNum(hit.point)] == false)
            //        {
            //            Instantiate(prefabPlaced[i], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
            //            isPlaced[SetgridcellNum(hit.point)] = true;
            //        }
            //        else if (isPlaced[SetgridcellNum(hit.point)] == true)
            //        {

            //        }
            //    }
            //}

            //var prefabSelected = GameObject.FindWithTag("Barrel");

            int cellNum = ChangePos2CellNum(hit.point);

            for (int i = 0; i < (int)ObjectType.Ashley; i++)
            {
                string tagName = ConvertObjectType2String((ObjectType)i);

                if (GameObject.FindWithTag(tagName))
                    Instantiate(prefabsOccupied[i], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            }

            //if (GameObject.FindWithTag("Barrel"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Barrel], ChangeCellNum2Pos(cellNum), Quaternion.identity);

            //else if(GameObject.FindWithTag("Fence"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Fence], ChangeCellNum2Pos(cellNum), Quaternion.identity);


            //if (prefabSelected == null /*|| isPlaced[cellno] == true*/) return;
            //isPlaced[cellno] = true;
            //if(prefabSelected == GameObject.FindGameObjectWithTag("Barrel"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Barrel], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            //else if (prefabSelected == GameObject.FindGameObjectWithTag("Fence"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Fence], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            //else if (prefabSelected == GameObject.FindGameObjectWithTag("Wall1"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Wall1], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            //else if (prefabSelected == GameObject.FindGameObjectWithTag("Wall2"))
            //    Instantiate(prefabsOccupied[(int)ObjectType.Wall2], ChangeCellNum2Pos(cellNum), Quaternion.identity);
            
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

    public int ChangePos2CellNum(Vector3 Point)
    {
        return (int)Point.x + (int)Point.z * gridSizeZ;
    }

    //셀 넘버를 포지션으로 변환하는 메소드
    public Vector3 ChangeCellNum2Pos(int cellNum)
    {
        int X = cellNum % 100;
        int Z = cellNum / 100;

        return new Vector3(startingPoint.x + 1.0f * X + 0.5f, 0.5f, startingPoint.z + 1.0f * Z + 0.5f);    
    }

    int[] FindNeighborGrid(Vector3 pos)
    {
        int gridNumber = ChangePos2CellNum(pos);

        List<int> neighbors = new List<int>() {1, -1, -gridSizeX, -gridSizeX - 1, -gridSizeX + 1, gridSizeX - 1, gridSizeX, gridSizeX + 1};
                                       
        if (gridNumber / gridSizeX == 0 && gridNumber % gridSizeX == 0)  //왼쪽 Edge
        {
            //neighbors.RemoveAll( (nb) => { return nb =  });
                 //delete gridNumber + gridSizeX - 1, gridNumber - 1, girdNumber - gridSizeX - 1, gridNumber - gridSizeX, gridNumber - gridSizeX + 1
        }
        else if (gridNumber / gridSizeX == 0 && gridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 끝 아래
        {
                 //delete gridNumber + gridSizeX + 1, gridNumber + 1, girdNumber - gridSizeX - 1, gridNumber - gridSizeX, gridNumber - gridSizeX + 1
        }
        else if (gridNumber / gridSizeX == gridSizeZ - 1 && gridNumber % gridSizeX == 0) // 왼쪽 맨 위
        {
                 //delete gridNumber + gridSizeX - 1, gridNumber + gridSizeX, gridNumber + gridSizeX + 1, girdNumber - 1, gridNumber - girdSizeX - 1
        }
        else if (gridNumber / gridSizeX == gridSizeZ - 1 && gridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 맨 위
        {
                 //delete gridNumber + gridSizeX - 1, gridNumber + gridSizeX, gridNumber + gridSizeX + 1, girdNumber + 1, gridNumber - girdSizeX + 1
        }

        for (int i = 0; i < neighbors.Count; i++)
            neighbors[i] += gridNumber;

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
