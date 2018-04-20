using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //public Dictionary<string, int> items = new Dictionary<string, int>();

    public bool inventoryIsOnScreen;

    [SerializeField]
    private Canvas inventoryCanvas;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject characterUI;

    private Vector3 characterUIScreenPosition;

    private Quaternion characterUIScreenRotation;

    private GameObject[] weaponsUI;



    void Start ()
    {
        inventoryCanvas.enabled = false;
        characterUI.SetActive(false);
        inventoryIsOnScreen = false;

        characterUI.transform.localEulerAngles = new Vector3(0, 180, 0);
        characterUIScreenRotation = characterUI.transform.localRotation;



        weaponsUI = GameObject.FindGameObjectsWithTag("Weapon");
    }
	
	void Update ()
    {
        if (Input.GetKeyDown("i") || Input.GetKeyDown("tab"))
            inventoryIsOnScreen = !inventoryIsOnScreen;
        


        if (inventoryIsOnScreen)
        {
            inventoryCanvas.enabled = true;
            characterUI.SetActive(true);
        }
        else
        {
            inventoryCanvas.enabled = false;
            characterUI.SetActive(false);
        }


        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //if(Physics.Raycast(ray, out hit))
        //{
        //    if(hit.transform.tag == "slot")
        //    {
        //        print(hit.transform.name);
        //    }
        //}
        
    }

    void LateUpdate()
    {
        characterUIScreenPosition = Camera.main.ScreenToWorldPoint(inventoryCanvas.transform.position + new Vector3(0, -160f, 0.4f)); //+ new Vector3(0, -0.3f, 0.7f);
        characterUI.transform.position = characterUIScreenPosition;

        characterUI.transform.localEulerAngles = Camera.main.transform.localEulerAngles - new Vector3(0, 180, 0);
    }

    public void SaveInventory()
    {
        //Save Data here
    }

    public void LoadInventory()
    {
        //Load Data here
    }

    //public void AddItem(string id, int count)
    //{
    //    // 해당 이름의 아이템이 없다면 추가
    //    if (items.ContainsKey(id) == false)
    //        items.Add(id, 0);

    //    // 아이템의 갯수를 증가시킨다.
    //    items[id] += count;
    //}
    
    //public void RemoveItem(string id, int count)
    //{
    //    //해당 이름의 아이템이 없다면 빠져나온다
    //    if (items.ContainsKey(id) == false)
    //        return;

    //    //이름의 아이템이 있으면 갯수를 감소
    //    items[id] -= count;

    //    //갯수가 0개 이하가 되면 지운다.
    //    if (items[id] < 0)
    //        items.Remove(id);
    //}
}
