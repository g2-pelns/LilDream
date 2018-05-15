using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float flySpeed = 10.0f; //regular speed
    public float shiftFly = 20.0f; //multiplied by how long shift is held.  Basically running
    public float maxShift = 25.0f; //Maximum speed when holdin gshift
    public float camSens = 1.5f; //How sensitive it with mouse

    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //When clicked lock again the mouse
        if (Input.GetMouseButtonDown(0) )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-Input.GetAxis("Mouse Y") * camSens, Input.GetAxis("Mouse X") * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftFly;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * flySpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.Space))
        { 
            //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPos.x = transform.position.x;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        else
        {
            transform.Translate(p);
        }
    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }

        return p_Velocity;
    }

}

//    {
//        private Vector3 p_Velocity;
//       
//} //returns the basic values, if it's 0 than it's not active.
