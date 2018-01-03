using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapEditor : MonoBehaviour
{

    //땅
    public GameObject Plane;

    public GameObject Barrel;

    private bool isBarrel;
    //땅 사이즈
    private float planeSizeX;
    private float planeSizeZ;

    //땅 시작점
    static private Vector3 startPoint;



    //만들어질 셀 사이즈들
    static private int gridSizeX = 100;
    static private int gridSizeZ = 100;


    //셀 번호
    static private int[] Cell;

    //셀 번호들의 위치
    static private Vector3[] CellPos;

    //장애물이 있는지 없는지
    private bool[] CellsObstacle;




    Ray ray;

    RaycastHit hit;
    //public List<TreeObstacle> treeobstacle = new List<TreeObstacle>();
    public static MapEditor me;

    void Awake()
    {
        isBarrel = false;
 
        //땅 사이즈 설정
        planeSizeX = Plane.GetComponent<Renderer>().bounds.size.x;
        planeSizeZ = Plane.GetComponent<Renderer>().bounds.size.z;




        //시작점 설정
        startPoint = new Vector3(Plane.transform.position.x - planeSizeX / 2, Plane.transform.position.y, Plane.transform.position.z - planeSizeZ / 2);

        //셀 번호 생성
        Cell = new int[gridSizeX * gridSizeZ];

        //셀 위치 생성
        CellPos = new Vector3[gridSizeX * gridSizeZ];

        //셀에 장애물이 있는지 없는지
        CellsObstacle = new bool[gridSizeX * gridSizeZ];

    }

    void Start()
    {
        if (me == null)
        {
            DontDestroyOnLoad(gameObject);
            me = this;
        }

        else if (me != this)
        {
            Destroy(gameObject);
        }


        //셀 넘버 설정
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            Cell[i] = i;
        }

        // 셀 넘버의 포지션 설정
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            CellPos[Cell[i]] = SetCellnumtoPos(Cell[i]);
        }

        //처음엔 모두 장애물 없음
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            CellsObstacle[i] = false;
        }

        ////제대로 나오는지 확인

        //for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        //{
        //    print("셀 넘버" + (Cell[i] + 1) + "의 위치 = " + CellPos[i]);
        //}

    }

    void Update()

    {
        //Obstacle.transform.position = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 그리드 생성 (10개 생설 하려면 줄은  11개 필요하다)
        for (int i = 0; i < gridSizeX + 1; i++)
        {
            Debug.DrawLine(new Vector3(startPoint.x, startPoint.y, startPoint.z + i), new Vector3(startPoint.x + planeSizeX, startPoint.y, startPoint.z + i), new Color(0, 0, 0));
        }



        for (int j = 0; j < gridSizeZ + 1; j++)
        {
            Debug.DrawLine(new Vector3(startPoint.x + j, startPoint.y, startPoint.z), new Vector3(startPoint.x + j, startPoint.y, startPoint.z + planeSizeZ), new Color(0, 0, 0));
        }


    }

    //클릭하면 좌표와 셀 넘버 표시하는 메소드
    void OnMouseDown()
    {
        //기본적인 레이케스트 사용

            if (Physics.Raycast(ray, out hit, 150))
            {
                if (isBarrel == true)
                {
                    if (CellsObstacle[SetgridcellNum(hit.point)] == false)
                    {


                        Instantiate(Barrel, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);

                        CellsObstacle[SetgridcellNum(hit.point)] = true;

                    }

                    if (CellsObstacle[SetgridcellNum(hit.point)] == true)
                    {

                    }
                }
                //print(SetCellnumtoPos(SetgridcellNum(hit.point)));
                //print(hit.point);
                //print(SetgridcellNum(hit.point));
                //Instantiate(Obstacle, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            }
        

    }



    //셀 포지션을 넘버로 추출하는 메소드
    public static int SetgridcellNum(Vector3 Point)
    {
        return (int)Point.x + (int)Point.z * gridSizeZ;
    }

    //셀 넘버를 포지션으로 변환하는 메소드
    public static Vector3 SetCellnumtoPos(int cellNum)
    {
        //열 추출
        int X = cellNum % 100;

        //행 추출
        int Z = cellNum / 100;

        //그리드의 위치를 셀의 중점으로 잡아서 셀 넘버의 포지션을 그 위치로 설정
        CellPos[cellNum] = new Vector3(startPoint.x + 1.0f * X + 0.5f, 0.1f, startPoint.z + 1.0f * Z + 0.5f);

        return CellPos[cellNum];
    }

    int[] FindNeighborGrid(Vector3 pos)
    {

        int GridNumber = SetgridcellNum(pos);

        List<int> Neighbor = new List<int>() { };

        // 열                                 // 행
        if (GridNumber / gridSizeX == 0 && GridNumber % gridSizeX == 0)  //왼쪽 끝 아래
        {

        }

        else if (GridNumber / gridSizeX == 0 && GridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 끝 아래
        {

        }

        else if (GridNumber / gridSizeX == gridSizeZ - 1 && GridNumber % gridSizeX == 0) // 왼쪽 맨 위
        {

        }

        else if (GridNumber / gridSizeX == gridSizeZ - 1 && GridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 맨 위
        {

        }

        return Neighbor.ToArray();

    }


    public void SavaData()
    {

        TreeObstacle treeobstacle = new TreeObstacle(Barrel, SetCellnumtoPos(SetgridcellNum(hit.point)), true);

        string jsonToFile = JsonUtility.ToJson(treeobstacle, true);

        string filePath = Path.Combine(Application.dataPath, "Resources/JsonText.json");

        File.WriteAllText(filePath, jsonToFile);
    }

    public void LoadData()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/JsonText.json");

        string jsonFromFile = File.ReadAllText(filePath);

        TreeObstacle treeobstacle = JsonUtility.FromJson<TreeObstacle>(jsonFromFile);

        Instantiate(treeobstacle.Tree, treeobstacle.TreePos, Quaternion.identity);
    }



}

[System.Serializable]
public class TreeObstacle
{
    public GameObject Tree;
    public Vector3 TreePos;
    public bool isObstacle;


    public TreeObstacle(GameObject Tree, Vector3 TreePos, bool isObstacle)
    {

        this.Tree = Tree;

        this.TreePos = TreePos;
        this.isObstacle = isObstacle;
    }
}
