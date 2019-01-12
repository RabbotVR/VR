using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour {
    #region variables
    #endregion

    #region accessors
    #endregion

    #region functions

    // Use this for initialization
    void Start () {
        transform.Rotate(-15, 0, 15);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime * 10, 0);
	} 
    #endregion 
}
   

