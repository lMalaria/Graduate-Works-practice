using UnityEngine;

public class Parachute : MonoBehaviour {

    [SerializeField]
    private GameObject cameraObject;

    Rigidbody rigidbody;

    private float dialogueEndTime;

    //[SerializeField]
    //Camera[] cameras;

	void Start ()
    {
        //cameraObject = GameObject.Find("Camera");
        rigidbody = GetComponent<Rigidbody>();

        // 연출을 위해서 3인칭 카메라와 중력적용을 꺼 놓은 채 시작 
        cameraObject.GetComponent<CameraFollow>().enabled = false;
        cameraObject.GetComponent<CutSceneCamera>().enabled = false;

        GameObject.Find("swat").GetComponent<Animator>().enabled = false;
        GameObject.Find("swat").GetComponent<Controller>().enabled = false;
        GameObject.Find("CameraHolder").GetComponent<ThirdPersonCamera>().enabled = false;

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 충돌시 물체가 돌아가지 않게 설정
    }
	
	void Update ()
    {
        dialogueEndTime += Time.deltaTime;

        if (dialogueEndTime > 13 /*dialogueEnd*/ )
        {
            cameraObject.GetComponent<CutSceneCamera>().enabled = true;
            //rigidbody.drag = 1;
            rigidbody.useGravity = true;           
        }

        if (this.gameObject.transform.position.y < 400)
            rigidbody.drag = 1;

        if (this.gameObject.transform.position.y < 20)
        {
            rigidbody.drag = 0;
            cameraObject.GetComponent<CutSceneCamera>().enabled = false;
            cameraObject.GetComponent<CameraFollow>().enabled = false;
            cameraObject.GetComponent<Camera>().depth = -2;
            //GameObject.Find("swat").GetComponent<Animator>().enabled = true;
            GameObject.Find("swat").GetComponent<Controller>().enabled = true;
            GameObject.Find("CameraHolder").GetComponent<ThirdPersonCamera>().enabled = true;
        }

        if (this.gameObject.transform.position.y == 1.3126)
        {
            GameObject.Find("swat").GetComponent<Animator>().enabled = true;
            //GameObject.Find("swat").GetComponent<Controller>().enabled = true;

            //Destroy(GameObject.Find("swat").GetComponent<Parachute>());
            //Destroy(this.gameObject.GetComponent<CutSceneCamera>());
            //Destroy(cameraObject.GetComponent<Animator>());
        }

        if (Input.GetKeyDown("escape"))
        {
            //dialogueEndTime = 14;
            //rigidbody.useGravity = true;

            Vector3 arrivalPosition = new Vector3(3.6f, 1.3f, -2.535f); 
            this.gameObject.transform.position = arrivalPosition;

            cameraObject.GetComponent<Camera>().depth = -2;

            GameObject.Find("swat").GetComponent<Animator>().enabled = true;
            GameObject.Find("swat").GetComponent<Controller>().enabled = true;
            GameObject.Find("CameraHolder").GetComponent<ThirdPersonCamera>().enabled = true;

            Vector3 aimPivot = GameObject.Find("aim Pivot").transform.position;
            aimPivot.y = aimPivot.y + 2000;
            GameObject.Find("aim Pivot").transform.position = aimPivot;



            Destroy(GameObject.Find("swat").GetComponent<Parachute>());
            Destroy(this.gameObject.GetComponent<CutSceneCamera>());
            Destroy(cameraObject.GetComponent<Animator>());
        }

	}
}
