using System;
using UnityEngine;
using VRStandardAssets.Utils;
using System.Collections;


namespace VRStandardAssets.Utils
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ObjectTrigger : MonoBehaviour
    {
      //  public VRInput m_VRInput;
      ////  public SelectionRadial m_SelectionRadial;

      //  void Start()
      //  {
      //      m_VRInput = GameObject.Find("MainCamera").GetComponent<VRInput>();
      //      m_SelectionRadial = GameObject.Find("MainCamera").GetComponent<SelectionRadial>();
      //  }
      //  private void OnEnable()
      //  {

      //      m_VRInput.OnDown += HandleDown;
      //      m_VRInput.OnUp += HandleUp;
      //    //  m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;

      //  }


      //  private void OnDisable()
      //  {
      //      m_VRInput.OnDown += HandleDown;
      //      m_VRInput.OnUp += HandleUp;
      //  //    m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
      //  }

      //  private void HandleDown()
      //  {
      //      // If the user is looking at the bar start the FillBar coroutine and store a reference to it.
      //      if (m_GazeOver)
      //          m_FillBarRoutine = StartCoroutine(FillBar());
      //      //  ScoreScript.scoreValue += 10;
      //      GetComponent<AudioSource>().Play();
      //      //  Debug.Log("Show Down state");
      //  }

      //  private void HandleUp()
      //  {
      //      // If the coroutine has been started (and thus we have a reference to it) stop it.
      //      if (m_FillBarRoutine != null)
      //          StopCoroutine(m_FillBarRoutine);

      //      // Reset the timer and bar values.
      //      m_Timer = 0f;
      //      //   SetSliderValue(0f);
      //      //   Debug.Log("Show Up state");
      //  }
    }
}