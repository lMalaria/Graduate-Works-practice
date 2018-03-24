using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private List<Item> inventory = new List<Item>();

    private bool inventoryIsOnScreen;

    private Canvas inventoryCanvas;

	void Start ()
    {
        inventoryIsOnScreen = false;

        inventory.Add( new Weapon("Pistol", "basic Pistol", false, 5, 1000, gameObject));
        //inventory.Add( new Weapon("M4", "low Recoil and good RPM(Round Per Minute) but weak", false, 10, 3000, gameObject));
        //inventory.Add( new Weapon("AK", "high Recoil and good RPM(Round Per Minute) and very powerful", false, 15, 3500, gameObject));
        
        inventory.Add( new Potion("HealthPotion", "This is a Health Potion", true, 20, 200, gameObject));
    }
	
	void Update ()
    {
        if (Input.GetKeyDown("i") || Input.GetKeyDown("tab"))
            inventoryIsOnScreen = !inventoryIsOnScreen;

        if (inventoryIsOnScreen)
            inventoryCanvas.enabled = true;
        else
            inventoryCanvas.enabled = false;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "slot")
            {
                print(hit.transform.name);
            }
        }

    }
}
