using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public bool enableTouch = false;
    public bool UIClicked = false;

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    public List<GameObject> points = new List<GameObject>();
    public List<GameObject> texts = new List<GameObject>();

    public GameObject text;

    // Update is called once per frame
    void Update()
    {
        foreach(var text in texts)
        {
            text.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        }
        
        if (Input.touchCount > 0 && enableTouch == true && !IsPointerOverUIObject())
        {
            Touch touch = Input.GetTouch(0);

            if (m_RaycastManager.Raycast(touch.position,raycast_hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                //Getting Pose
                Pose pose = raycast_hits[0].pose;
                enableTouch = false;
                spawnedARObject = Instantiate(aRObjectPrefab,pose.position,Quaternion.Euler(0,0,0));

                points.Add(spawnedARObject);
                if(points.Count>=2)
                {
                    spawnedARObject.GetComponent<LineRenderer>().positionCount = 2;
                    spawnedARObject.GetComponent<LineRenderer>().SetPosition(0, spawnedARObject.transform.position);
                    spawnedARObject.GetComponent<LineRenderer>().SetPosition(1, points[points.Count - 2].transform.position);

                    var temp = Instantiate(text, ((spawnedARObject.transform.position + points[points.Count - 2].transform.position) / 2), Quaternion.identity);
                    //temp.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);
                    //temp.transform.localEulerAngles = new Vector3(0, 180, 0);
                    temp.GetComponent<TextMesh>().text = ((Vector3.Distance(spawnedARObject.transform.position, points[points.Count - 2].transform.position))*100).ToString("0.00") + "cm";
                    texts.Add(temp);
                }
            }
        }
    }

    public void OnclickEnableTouch()
    {
        if(!enableTouch)
        {
            enableTouch = true;
        }
        else
        {
            enableTouch = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
