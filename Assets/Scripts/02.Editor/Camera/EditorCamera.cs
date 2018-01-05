using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour {

    private float panSpeed;
    private float panBoarderThickness;
    //private Vector2 panLimit;
    private float scrollSpeed;
    //private float minY;
    //private float maxY;
    private bool isMouseDown;

    void Awake()
    {
        panSpeed = 10.0f;
        panBoarderThickness = 2.0f;
        //panLimit = new Vector2 (40, 50);
        scrollSpeed = 100.0f;
        //minY = 0.5f;
        //maxY = 120.0f;
        isMouseDown = false;
    }

    void Start()
    {

    }

    void Update()
    {

        Vector3 pos = transform.position;


        if (isMouseDown == false)

        {

            if (Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - panBoarderThickness*/)

            {

                pos.z += panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("s") /*|| Input.mousePosition.y <= panBoarderThickness*/)

            {

                pos.z -= panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - panBoarderThickness*/)

            {

                pos.x += panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("a") /*|| Input.mousePosition.x <= panBoarderThickness*/)

            {

                pos.x -= panSpeed * Time.deltaTime;

            }

        }



        float scroll = Input.GetAxis("Mouse ScrollWheel");


        pos.y -= scroll * scrollSpeed * Time.deltaTime;



        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);

        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        //pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);



        if (Input.GetMouseButton(1))

        {

            isMouseDown = true;

            float cameraUpDown = Input.GetAxis("Mouse Y");

            float cameraLeftRight = Input.GetAxis("Mouse X");

            transform.Rotate(-cameraUpDown, cameraLeftRight, 0);



            if (Input.GetKey("w"))

            {

                pos.z += panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("s"))

            {

                pos.z -= panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("d"))

            {

                pos.x += panSpeed * Time.deltaTime;

            }



            if (Input.GetKey("a"))

            {

                pos.x -= panSpeed * Time.deltaTime;

            }



        }



        if (Input.GetMouseButtonUp(1))
        {

            isMouseDown = false;

        }



        transform.position = pos;

    }
}
