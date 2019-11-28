using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControls : MonoBehaviour
{

    public float mainSpeed = 50.0f;                        //Standard speed
    public float shiftAdd = 150.0f;                         //Shift speed modifier
    public float maxShift = 1000.0f;                        //Maximum speed when holdin gshift
    public float cameraSense = 1.5f;                        //Camera movement speed
    private Vector3 lastMouse = new Vector3(255, 255, 255); //Recenter on activate
    private float totalRun = 1.0f;
    private bool mouseLookActive = false;

    void Update()
    {

        //Activate manual flight controls
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ActivateMouseLook(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ActivateMouseLook(false);
        }

        //Vector from mouse angle
        if (mouseLookActive)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraSense;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * cameraSense;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        //Movement
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }
    }

    public void ActivateMouseLook(bool active)
    {
        if (active)
        {
            MovetoWaypoint.instance.target = null;          //Kill  Move to Waypoint target so that it does not take over navigation
            mouseLookActive = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            mouseLookActive = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Key definitions
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
        if (Input.GetKey(KeyCode.Q))                //Up
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.E))                //Down
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        return p_Velocity;
    }
}