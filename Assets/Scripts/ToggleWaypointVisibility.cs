using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWaypointVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    public void FindAllWaypoints()
    {
        var waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");

        Debug.Log("Found waypoints: " + waypointObjects.Length);

        foreach(var waypoint in waypointObjects)
        {
            Debug.Log("Waypoint found: " + waypoint.name);
        }

        Debug.Log("Done.");
    }

    public void ToggleVisibility()
    {
        var waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");

        for (int i = 0; i < waypointObjects.Length; i++)
        {
            GameObject waypoint = waypointObjects[i];
            Toggle(waypoint.GetComponent<Renderer>());

            //Deal with children
            foreach(Transform child in waypoint.transform)
            {
                Toggle(child.GetComponent<Renderer>());
            }
        }

    }

    private static void Toggle(Renderer waypointRenderer)
    {
        if (waypointRenderer.enabled == false)
        {
            waypointRenderer.enabled = true;
        }
        else
        {
            waypointRenderer.enabled = false;
        }
    }
}
