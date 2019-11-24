using UnityEngine;

public class MoveOnClick : MonoBehaviour
{

    public Transform target;

    private void OnMouseDown()
    {
        MovetoWaypoint.instance.target = this.transform;
    }
}
