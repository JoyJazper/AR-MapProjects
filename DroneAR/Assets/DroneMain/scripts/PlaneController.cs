using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;

    public Text buttonText;

    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlaneDetectionAndVisibility()
    {
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;

        if (m_ARPlaneManager.enabled)
        {

            buttonText.text = "Disable Plane Detection And Hide Existing";
            GetComponent<ARPlacer>().enabled = true;
            SetAllPlanesActiveOrDeactive(true);
        }
        else
        {
            buttonText.text = "Enable Plane Detection And Show Existing";
            GetComponent<ARPlacer>().enabled = false;
            SetAllPlanesActiveOrDeactive(false);

        }
    }


    void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
