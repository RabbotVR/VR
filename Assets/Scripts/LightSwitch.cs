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

        //myLight.enabled = !myLight.enabled;
       StartCoroutine(TimeLimit());
    }
    IEnumerator TimeLimit()
    {
        myLight.enabled = false;
       
        yield return new WaitForSeconds(10.0f);
        myLight.enabled = true;
    }
}
