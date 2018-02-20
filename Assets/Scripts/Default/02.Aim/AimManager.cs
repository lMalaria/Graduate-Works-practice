using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimManager : MonoBehaviour {

    [SerializeField]
    private Image aim;

    public bool isAiming;

    public bool isAutoTargetingModeOn;

    CopZombieController copZombieController;
    BombZombieController bombZombieController;

    private Vector3 zombiePositionToWorld;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject autoTargetingModeTarget;

    private GameObject muzzle;

    private ParticleSystem muzzleFlash;

    [SerializeField]
    private GameObject bloodEffectOnEnemy;

    void Awake()
    {
        Cursor.visible = false;

        aim = GetComponent<Image>();
        aim.enabled = false;
        isAiming = false;

        isAutoTargetingModeOn = false;

        muzzle = GameObject.Find("Muzzle Flash");
        muzzleFlash = muzzle.GetComponent<ParticleSystem>();
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

            if (isAutoTargetingModeOn == false)
            {
                aim.transform.position = Input.mousePosition;

                if (Input.GetMouseButtonDown(0))
                {
                    muzzleFlash.Play();

                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (!Physics.Raycast(ray, out hit, 150)) return;

                    string targetName = hit.transform.name;
                    //Debug.Log(targetName);

                    if (hit.collider.name == "CopZombie")
                    {
                        copZombieController = hit.collider.gameObject.GetComponent<CopZombieController>();
                        //bombZombieController = hit.collider.gameObject.GetComponent<BombZombieController>();
                        copZombieController.IsBeingDamaged(5);
                        CreateBloodEffectOnEnemy(hit.point);
                        //bombZombieController.IsBeingDamged(10);
                    }

                    if (hit.collider.name == "BombZombie")
                    {
                        bombZombieController = hit.collider.gameObject.GetComponent<BombZombieController>();
                        CreateBloodEffectOnEnemy(hit.point);
                        bombZombieController.IsBeingDamged(10);
                    }

                    //if(hit.collider)
                    //{
                    //    Debug.Log(targetName);
                    //}
                }
            }


            if (isAutoTargetingModeOn == true)
            {
                zombiePositionToWorld = Camera.main.WorldToScreenPoint(GameObject.Find("CopZombie").transform.position + new Vector3(0, 0.4f, 0));

                aim.transform.position = zombiePositionToWorld;

                if (Input.GetMouseButtonDown(0))
                { 
                    var ray = Camera.main.ScreenPointToRay(zombiePositionToWorld);
                    RaycastHit hit;

                    if (!Physics.Raycast(ray, out hit, 150)) return;
                    
                    if (hit.collider.name == "CopZombie")
                    {
                        copZombieController = hit.collider.gameObject.GetComponent<CopZombieController>();
                        Instantiate(bloodEffectOnEnemy, hit.point, Quaternion.identity);
                        //bombZombieController = hit.collider.gameObject.GetComponent<BombZombieController>();
                        copZombieController.IsBeingDamaged(5);
                        //copZombieController.CreateBloodEffectOnEnemy(Camera.main.ScreenToWorldPoint(hit.point));
                        //bombZombieController.IsBeingDamged(10);
                        //Instantiate(bloodParticle, hit.point, Quaternion.identity);
                    }

                    if (hit.collider.name == "BombZombie")
                    {
                        bombZombieController = hit.collider.gameObject.GetComponent<BombZombieController>();
                        bombZombieController.IsBeingDamged(10);
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

    void CreateBloodEffectOnEnemy(Vector3 position)
    {
        GameObject bloodEffect = Instantiate(bloodEffectOnEnemy, position, Quaternion.identity);
        Destroy(bloodEffect, 2.0f);
    }
}
