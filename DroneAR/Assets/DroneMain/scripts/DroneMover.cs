using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine.UI;

public class DroneMover : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    public float droneSpeed = 0.5f;

    bool moveEnable = false;
    bool isArrived = false;

    public Vector2d navigationPointA;
    public Vector2d navigationPointB;
    public Vector2d navigationPointC;

    Button relocateToA;
    Button relocateToB;
    Button relocateToC;

    void Start()
    {
        _map = FindObjectOfType<AbstractMap>();
        moveEnable = true;

        relocateToA = GameObject.Find("ButtonForPointA").GetComponent<Button>();
        relocateToA.onClick.AddListener(() => { RelocateToA(); });

        relocateToB = GameObject.Find("ButtonForPointB").GetComponent<Button>();
        relocateToB.onClick.AddListener(() => { RelocateToB(); });

        relocateToC = GameObject.Find("ButtonForPointC").GetComponent<Button>();
        relocateToC.onClick.AddListener(() => { RelocateToC(); });
    }

    private void Update()
    {
        if(moveEnable)
        {
            Vector3 destination = _map.GeoToWorldPosition(new Vector2d(51.5096351, -0.1354588));

            if (!isArrived)
            {
                var distanceCovered = droneSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position,destination,distanceCovered);
                if(Vector3.Distance(transform.position,destination)<0.01f)
                {
                    isArrived = true;
                    moveEnable = false;
                }
            }
        }
    }

    void RelocateToA()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(navigationPointA));
    }

    void RelocateToB()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(navigationPointB));
    }

    void RelocateToC()
    {
        StopAllCoroutines();
        StartCoroutine(FlyToPoint(navigationPointC));
    }

    IEnumerator FlyToPoint(Vector2d locationPoint)
    {
        Vector3 destination = _map.GeoToWorldPosition(new Vector2d(locationPoint.x,locationPoint.y));
        while(Vector3.Distance(transform.position, destination) > 0.01f)
        {
            var distanceCovered = droneSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, distanceCovered);
            yield return (0);
        }
    }
}
