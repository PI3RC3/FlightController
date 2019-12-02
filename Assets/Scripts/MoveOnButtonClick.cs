using UnityEngine;

public class MoveOnButtonClick : MonoBehaviour
{
    public void SetCameraTarget(Transform targetObject)
    {
        if (targetObject != null)
        {

            MovetoWaypoint.instance.target = targetObject;
        }
        else
        {
            //Debug.LogError("No transform target set");
        }
    }
}
