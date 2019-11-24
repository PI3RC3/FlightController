using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
