using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

public class ARPlacer : MonoBehaviour
{
    ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> raycast_hits = new List<ARRaycastHit>();

    //this is the prefab to be instantiated
    public GameObject aRObjectPrefab;

    //this is the gameobject that is intantiated after a successfull raycast intersection with a plane
    private GameObject spawnedARObject;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (m_RaycastManager.Raycast(touch.position,raycast_hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                //Getting Pose
                Pose pose = raycast_hits[0].pose;


                if (spawnedARObject==null)
                {
                    spawnedARObject = Instantiate(aRObjectPrefab,pose.position,Quaternion.Euler(0,0,0));

                }else
                {
                    spawnedARObject.transform.position = pose.position;
                }

            }

        }

    }
}
