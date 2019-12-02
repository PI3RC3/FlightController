using UnityEngine;

public class FlightControls : MonoBehaviour
{

    public float mainSpeed = 50.0f;                         //Standard speed
    public float shiftAdd = 150.0f;                         //Shift speed modifier
    public float maxShift = 1000.0f;                        //Maximum speed when holdin gshift
    public float cameraSense = 1.5f;                        //Camera movement speed
    private Vector3 lastMouse = new Vector3(255, 255, 255); //Recenter mouse on activate
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

        #region Pitch & Yaw

        //Pitch and Yaw from Mouse Input
        if (mouseLookActive)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraSense;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * cameraSense;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        //Pitch from Keyboard input
        if (Input.GetKey(KeyCode.F))
        {
            float newRotationX = transform.localEulerAngles.x + 1f * (Time.deltaTime * mainSpeed);
            transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0.0f);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            float newRotationX = transform.localEulerAngles.x - 1f * (Time.deltaTime * mainSpeed);
            transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0.0f);
        }

        //Yaw from keyboard input
        if (Input.GetKey(KeyCode.Z))
        {
            float newRotationY = transform.localEulerAngles.y + 1f * (Time.deltaTime * mainSpeed);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newRotationY, 0.0f);
        }
        else if(Input.GetKey(KeyCode.C))
        {
            float newRotationY = transform.localEulerAngles.y -1f * (Time.deltaTime * mainSpeed);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newRotationY, 0.0f);
        }

        #endregion

        #region Direction & Speed
        //Movement Vector
        Vector3 newVector = GetBaseInput();

        //Speed boost
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            newVector = newVector * totalRun * shiftAdd;
            newVector.x = Mathf.Clamp(newVector.x, -maxShift, maxShift);
            newVector.y = Mathf.Clamp(newVector.y, -maxShift, maxShift);
            newVector.z = Mathf.Clamp(newVector.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            newVector = newVector * mainSpeed;
        }

        newVector = newVector * Time.deltaTime;

        #endregion

        transform.Translate(newVector);

        //Vector3 newPosition = transform.position;
    }

    //Standard WASD + QE
    private Vector3 GetBaseInput()
    {
        Vector3 newVector = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            newVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            newVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            newVector += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            newVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.Q))                //Up
        {
            newVector += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.E))                //Down
        {
            newVector += new Vector3(0, -1, 0);
        }
        return newVector;
    }

    public void ActivateMouseLook(bool active)
    {
        if (active)
        {
            MovetoWaypoint.instance.target = null;          //Kill move to Waypoint target so that it does not take over navigation
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
}