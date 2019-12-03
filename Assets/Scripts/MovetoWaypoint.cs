using UnityEngine;

public class MovetoWaypoint : MonoBehaviour
{
    public static MovetoWaypoint instance;      //Targets will be set via UI buttons.  Script usage:  MovetoWaypoint.instance.target = targetObject;
    public Transform target;                    //Game Object that camera will be matching position and rotation of
    public float moveSpeed = 30.0f;             //Movement to target object
    public float panSpeed = 0.8f;               //Turn to face target

    private Vector3 relativePosition;
    private Quaternion targetRotation;

    void Start()
    {
        instance = this;
        SetCameraTarget(target);
    }

    private void Update()
    {
        if (target != null)
        {
            MoveToTarget();
        }
        else
        {
            //Debug.Log("Waiting for target...");
        }
    }

    void SetCameraTarget(Transform cameraTarget)
    {
        target = cameraTarget;

        if (target != null)
        {
            Debug.Log("Targeting: " + cameraTarget.name);
        }
    }

    void MoveToTarget()
    {
        relativePosition = target.position - transform.position;

        if (transform.position != target.position)
        {
            if (relativePosition != Vector3.zero)                               //Avoid "Look location viewing vector is zero" error
            {
                targetRotation = Quaternion.LookRotation(relativePosition);
            }

            //Turn to face target
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, panSpeed * Time.deltaTime);

            //Move toward target
            Debug.Log("Move Speed to Target: " + moveSpeed.ToString());
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //If you try and lerp to match a target's Quaternion, and you are both at the same position, you 
            //end up with a vector zero error (see above).
            
            //Project Ghost Target  
            GameObject ghost = new GameObject();
            ghost.name = "Space Ghost";
            ghost.transform.position = target.transform.position;
            ghost.transform.rotation = target.transform.rotation;
            ghost.transform.position += Vector3.forward * 10;   //Launch target

            var ghostRelativePosition = ghost.transform.position - transform.position;

            //Turn to face same angle as waypoint target
            transform.rotation = Quaternion.Lerp(transform.rotation, ghost.transform.rotation, panSpeed * Time.deltaTime);
            Destroy(ghost);
        }
    }
}
