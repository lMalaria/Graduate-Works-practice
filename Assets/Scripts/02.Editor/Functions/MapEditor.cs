using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapEditor : MonoBehaviour
{

    //땅
    [SerializeField] private GameObject Plane;

    //장애물
    [SerializeField] private GameObject[] prefabOnMouse;
    [SerializeField] private GameObject[] prefabPlaced;

    private bool[] isPrefabOnMouse;
    //세이브 패널
    [SerializeField] private GameObject SavePanel;
    
    //땅 사이즈
    private float planeSizeX;
    private float planeSizeZ;

    //땅 시작점
    private Vector3 startPoint;

    //만들어질 셀 사이즈들
    private int gridSizeX;
    private int gridSizeZ;

    //셀 번호
    private int[] cell;

    //셀 번호들의 위치
    private Vector3[] cellPos;

    //장애물이 있는지 없는지
    private bool[] isPlaced;

    [SerializeField] private EditorCamera editCam;


    void Awake()
    {
        gridSizeX = 100;
        gridSizeZ = 100;

        //prefabOnMouse = new GameObject[10];
        //prefabPlaced = new GameObject[10];
        isPrefabOnMouse = new bool[10];

        //땅 사이즈 설정
        planeSizeX = Plane.GetComponent<Renderer>().bounds.size.x;
        planeSizeZ = Plane.GetComponent<Renderer>().bounds.size.z;

        //시작점 설정
        startPoint = new Vector3(Plane.transform.position.x - planeSizeX / 2, Plane.transform.position.y, Plane.transform.position.z - planeSizeZ / 2);

        //셀 번호 생성
        cell = new int[gridSizeX * gridSizeZ];

        //셀 위치 생성
        cellPos = new Vector3[gridSizeX * gridSizeZ];

        //셀에 장애물이 있는지 없는지
        isPlaced = new bool[gridSizeX * gridSizeZ];

    }

    void Start()
    {

        //1~100 까지 프리팹이 마우스 포지션에 있는지 없는지
        for (int i = 0; i < 10; i++)
        {
            isPrefabOnMouse[i] = false;
        }

        //셀 넘버 설정
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            cell[i] = i;
        }

        // 셀 넘버의 포지션 설정
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            cellPos[cell[i]] = SetCellnumtoPos(cell[i]);
        }

        //처음엔 모두 장애물 없음
        for (int i = 0; i < gridSizeX * gridSizeZ; i++)
        {
            isPlaced[i] = false;
        }

    }

    void Update()
    {

        bool isESC = false;
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.green);

        for (int i = 0; i < gridSizeX + 1; i++)
        {
            Debug.DrawLine(new Vector3(startPoint.x, startPoint.y, startPoint.z + i), new Vector3(startPoint.x + planeSizeX, startPoint.y, startPoint.z + i), new Color(0, 0, 0));
        }

        for (int j = 0; j < gridSizeZ + 1; j++)
        {
            Debug.DrawLine(new Vector3(startPoint.x + j, startPoint.y, startPoint.z), new Vector3(startPoint.x + j, startPoint.y, startPoint.z + planeSizeZ), new Color(0, 0, 0));
        }

        if (Input.GetKey("escape"))
        {
            for (int i = 0; i < 10; i++)
            {
                isPrefabOnMouse[i] = false;
                prefabOnMouse[i].SetActive(false);
            }
        }
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

            if (isPrefabOnMouse[0] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[0], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }
            }

            if(isPrefabOnMouse[1] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[1], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }
            }

            if (isPrefabOnMouse[2] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[2], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }

            }

            if (isPrefabOnMouse[3] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                   Instantiate(prefabPlaced[3], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }

            }

            if (isPrefabOnMouse[4] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[4], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }

            }

            if (isPrefabOnMouse[5] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[5], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }

            }

            if (isPrefabOnMouse[6] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[6], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true){

                  }

            }

            if (isPrefabOnMouse[7] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[7], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true)
                  {

                  }

            }

            if (isPrefabOnMouse[8] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[8], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                } else if (isPlaced[SetgridcellNum(hit.point)] == true)
                  {

                  }

            }

            if (isPrefabOnMouse[9] == true)
            {
                if (isPlaced[SetgridcellNum(hit.point)] == false)
                {
                    Instantiate(prefabPlaced[9], SetCellnumtoPos(SetgridcellNum(hit.point)), Quaternion.identity);
                    isPlaced[SetgridcellNum(hit.point)] = true;
                }
                else if (isPlaced[SetgridcellNum(hit.point)] == true)
                {

                }

            }
            //print(SetCellnumtoPos(SetgridcellNum(hit.point)));
            //print(hit.point);
            //print(SetgridcellNum(hit.point));
            //Instantiate(Obstacle, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        }
        

    }

    public int SetgridcellNum(Vector3 Point)
    {
        return (int)Point.x + (int)Point.z * gridSizeZ;
    }

    //셀 넘버를 포지션으로 변환하는 메소드
    public Vector3 SetCellnumtoPos(int cellNum)
    {
        //열 추출
        int X = cellNum % 100;
        //행 추출
        int Z = cellNum / 100;
        //그리드의 위치를 셀의 중점으로 잡아서 셀 넘버의 포지션을 그 위치로 설정
        cellPos[cellNum] = new Vector3(startPoint.x + 1.0f * X + 0.5f, 0.5f, startPoint.z + 1.0f * Z + 0.5f);

        return cellPos[cellNum];
    }

    int[] FindNeighborGrid(Vector3 pos)
    {

        int gridNumber = SetgridcellNum(pos);

        List<int> Neighbor = new List<int>() {gridNumber+1, gridNumber-1, gridNumber + gridSizeX, gridNumber + gridSizeX - 1, gridNumber + gridSizeX + 1, gridNumber - gridSizeX, gridNumber - gridSizeX - 1, gridNumber - gridSizeX + 1};
        // 열                                 // 행
        if (gridNumber / gridSizeX == 0 && gridNumber % gridSizeX == 0)  //왼쪽 끝 아래
        {
                 //delete gridNumber + gridSizeX - 1, gridNumber - 1, girdNumber - gridSizeX - 1, gridNumber - gridSizeX, gridNumber - gridSizeX + 1
        } else if (gridNumber / gridSizeX == 0 && gridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 끝 아래
          {
                 //delete gridNumber + gridSizeX + 1, gridNumber + 1, girdNumber - gridSizeX - 1, gridNumber - gridSizeX, gridNumber - gridSizeX + 1
          } else if (gridNumber / gridSizeX == gridSizeZ - 1 && gridNumber % gridSizeX == 0) // 왼쪽 맨 위
            {
                 //delete gridNumber + gridSizeX - 1, gridNumber + gridSizeX, gridNumber + gridSizeX + 1, girdNumber - 1, gridNumber - girdSizeX - 1
            } else if (gridNumber / gridSizeX == gridSizeZ - 1 && gridNumber % gridSizeX == gridSizeX - 1)  // 오른쪽 맨 위
              {
                 //delete gridNumber + gridSizeX - 1, gridNumber + gridSizeX, gridNumber + gridSizeX + 1, girdNumber + 1, gridNumber - girdSizeX + 1
              }

        return Neighbor.ToArray();

    }

    public void SaveButton()
    {
        for (int i = 0; i < 10; i++)
        {
            isPrefabOnMouse[i] = false;
            prefabOnMouse[i].SetActive(false);
        }

        Destroy(editCam);
        SavePanel.SetActive(true);
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


    //public void PrefabisOn()
    //{
    //    for(int i = 0; i < 10; i++)
    //    {
    //        prefabOnMouse[i].SetActive(false);  
    //        isPrefabOnMouse[i]= false;
    //    }

    //}

    public void BarrelOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[0].SetActive(true);
        isPrefabOnMouse[0] = true;
    }

    public void FenceOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[1].SetActive(true);
        isPrefabOnMouse[1] = true;
    }

    public void Wall1On()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[2].SetActive(true);
        isPrefabOnMouse[2] = true;
    }

    public void Wall2On()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[3].SetActive(true);
        isPrefabOnMouse[3] = true;
    }

    public void CivilianZombie1On()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[4].SetActive(true);
        isPrefabOnMouse[4] = true;
    }

    public void CivilianZombie2On()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[5].SetActive(true);
        isPrefabOnMouse[5] = true;
    }

    public void HoundZombieOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[6].SetActive(true);
        isPrefabOnMouse[6] = true;
    }

    public void SawZombieOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[7].SetActive(true);
        isPrefabOnMouse[7] = true;
    }

    public void LeonOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[8].SetActive(true);
        isPrefabOnMouse[8] = true;
    }

    public void AshleyOn()
    {
        for (int i = 0; i < 10; i++)
        {
            prefabOnMouse[i].SetActive(false);
            isPrefabOnMouse[i] = false;
        }

        prefabOnMouse[9].SetActive(true);
        isPrefabOnMouse[9] = true;
    }





}

[System.Serializable]
public class PlacedObject
{
    public int[] index = new int[100];
    public GameObject ObjSaved;
    public Vector3 ObjPos;
}
