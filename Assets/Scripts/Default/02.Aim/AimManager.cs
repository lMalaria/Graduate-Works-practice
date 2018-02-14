using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimManager : MonoBehaviour {

    [SerializeField]
    private Image aim;

    [SerializeField]
    private bool isAiming;

    public bool isAutoTargetingModeOn;

    CopZombieController copZombieController;
    BombZombieController bombZombieController;

    private Vector3 zombiePositionToWorld;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject autoTargetingModeTarget;

    void Awake()
    {
        Cursor.visible = false;

        aim = GetComponent<Image>();
        aim.enabled = false;
        isAiming = false;

        isAutoTargetingModeOn = false;
    }


    void Start () 
    {

    }
	
	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            if (isAiming == false)
            {
                isAiming = true;
                aim.enabled = true;
            }
        }

        if(isAiming == true)
        {
            zombiePositionToWorld = Camera.main.WorldToScreenPoint(GameObject.Find("CopZombie").transform.position + new Vector3(0, 0.4f, 0));

            if (isAutoTargetingModeOn == false)
            {
                aim.transform.position = Input.mousePosition;

                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (!Physics.Raycast(ray, out hit, 150)) return;

                    if (hit.collider.tag == "Enemy")
                    {
                        copZombieController = hit.collider.gameObject.GetComponent<CopZombieController>();
                        bombZombieController = hit.collider.gameObject.GetComponent<BombZombieController>();
                        copZombieController.IsBeingDamaged(5);
                        bombZombieController.IsBeingDamged(10);
                        //Instantiate(bloodParticle, hit.point, Quaternion.identity);
                    }
                }
            }


            if (isAutoTargetingModeOn == true)
            {
                aim.transform.position = zombiePositionToWorld;


                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenPointToRay(zombiePositionToWorld);
                    RaycastHit hit;

                    if (!Physics.Raycast(ray, out hit, 150)) return;

                    if (hit.collider.tag == "Enemy")
                    {
                        copZombieController = hit.collider.gameObject.GetComponent<CopZombieController>();
                        copZombieController.IsBeingDamaged(5);
                        //Instantiate(bloodParticle, hit.point, Quaternion.identity);
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                isAiming = false;
                aim.enabled = false;
            }
        }

	}
}
