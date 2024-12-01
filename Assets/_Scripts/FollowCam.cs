using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;   // The static point of interest

    [Header("Inscribed")]
    public Vector2 minXY = Vector2.zero;

    [Header("Dynamic")]
    public float camDistance;  // The desired Z pos of the camera

    void Awake()
    {
        if (camDistance == 0)
        {
            camDistance = this.transform.position.z;
        }

        camDistance = Mathf.Abs(camDistance) * -1;
        print(camDistance);
        
    }

    void FixedUpdate()
    {
        //if (POI == null) return;

        // Get the position of the POI
        // Vector3 destination = POI.transform.position;

        Vector3 destination = Vector3.zero;

        if (POI != null)
        {
            // If the POI has a RigidBody, check to see if it is sleeping
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping())
            {
                POI = null;
            }
        }

        if (POI != null)
        {
            destination = POI.transform.position;
        }

        // Limit the minimum values of destination.x and desination.y
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        // Force destination.z to be camZ to keep the camera far enough away
        destination.z = camDistance;
        // Set the camera to the destination
        transform.position = destination;
        // Set the orthographicSize of the Camera to keep the ground in view
        //Camera.main.orthographicSize = destination.y + 10;
    }
}
