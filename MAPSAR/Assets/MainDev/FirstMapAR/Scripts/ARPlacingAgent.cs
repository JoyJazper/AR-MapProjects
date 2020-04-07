using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

public class ARPlacingAgent : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;

    List<ARRaycastHit> raycast_hits = new List<ARRaycastHit>();

    public GameObject mapPrefab;


    void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(m_ARRaycastManager.Raycast(touch.position, raycast_hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose pose = raycast_hits[0].pose;
                mapPrefab.transform.position = pose.position;
            }
        }
    }
}
