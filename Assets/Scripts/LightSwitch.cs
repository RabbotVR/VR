using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSwitch : MonoBehaviour
{
    private Light myLight;

    void Start() { 
    myLight = GetComponent<Light>();
}
    public void ToggleLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}
