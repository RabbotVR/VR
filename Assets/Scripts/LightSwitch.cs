using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSwitch : MonoBehaviour
{
    private Light myLight;
    public GameObject BlackOn;


    void Start() { 
    myLight = GetComponent<Light>();
    BlackOn = GameObject.Find("Black");
    }
    public void ToggleLight()
    {

        //myLight.enabled = !myLight.enabled;
       StartCoroutine(TimeLimit());
    }
    IEnumerator TimeLimit()
    {
        myLight.enabled = false;
        BlackOn.SetActive(true);

        yield return new WaitForSeconds(10.0f);
        myLight.enabled = true;
        BlackOn.SetActive(false);
    }
}
