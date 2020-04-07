using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class PlaneChecker : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    public Text buttonText;

    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    public void PlaneDetectionAndVisibility()
    {
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;

        if(m_ARPlaneManager.enabled)
        {
            ActivatePlane();
            GetComponent<ARPlacingAgent>().enabled = true;
            buttonText.text = "Set";
        }
        else
        {
            DeActivatePlane();
            GetComponent<ARPlacingAgent>().enabled = false;
            buttonText.text = "Detect";
        }
    }

    public void ActivatePlane()
    {
        foreach(var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
    }

    public void DeActivatePlane()
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
