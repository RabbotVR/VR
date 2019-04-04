using System;
using UnityEngine;
using VRStandardAssets.Utils;
using System.Collections;
using UnityEngine.UI;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        public event Action OnBarFilled;
        public VRInput m_VRInput;
        public SelectionRadial m_SelectionRadial;

        [SerializeField] private float m_Duration = 2f;
        [SerializeField] private Material m_NormalMaterial;
        [SerializeField] private Material m_OverMaterial;
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;
        ////////////////
        [SerializeField] private AudioSource m_Audio;                       // Reference to the audio source that will play effects when the user looks at it and when it fills.
        [SerializeField] private AudioClip m_OnOverClip;
        [SerializeField] private AudioClip m_OnFilledClip;
        [SerializeField] private GameObject m_BarCanvas;
        [SerializeField] private bool m_DisableOnBarFill;                   // Whether the bar should stop reacting once it's been filled (for single use bars).
        [SerializeField] private bool m_DisappearOnBarFill;                 // Whether the bar should disappear instantly once it's been filled.
                                                                            // [SerializeField] private SelectionRadial m_SelectionRadial;
        [SerializeField] private Slider m_Slider;
        // [SerializeField] private VRInput m_VRInput;

        private bool m_BarFilled;                                           // Whether the bar is currently filled.
        private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.

        private float m_Timer;                                              // Used to determine how much of the bar should be filled.
        private float m_ReduceScoreTimer = 10.0f;
        private Coroutine m_FillBarRoutine;


        //    private const string k_SliderMaterialPropertyName = "_SliderValue";

        void Start()
        {
            m_VRInput = GameObject.Find("MainCamera").GetComponent<VRInput>();
            m_SelectionRadial = GameObject.Find("MainCamera").GetComponent<SelectionRadial>();
        }
        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;

        }
        void Update()
        {
            ReduceScore();
           // int myScore = Mathf.RoundToInt(m_ReduceScoreTimer);
            if (m_ReduceScoreTimer < 0)
            {

                ScoreScript.scoreValue -= 10;
                m_ReduceScoreTimer = 10.0f;

            }

           // Debug.Log(m_ReduceScoreTimer);
        }

   


            void ReduceScore()
        {
            if (m_GazeOver == false)
            {
                m_ReduceScoreTimer -= Time.deltaTime;
            }
            else
            {
                m_ReduceScoreTimer = 10.0f;
            }
        }

        private void OnEnable()
        {
            m_InteractiveItem.OnDown += HandleDown;
            m_InteractiveItem.OnUp += HandleUp;
          //  m_VRInput.OnDown += HandleDown;
         //  m_VRInput.OnUp += HandleUp;
         //   m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
 
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
     
 
        }


        private void OnDisable()
        {
      //    m_VRInput.OnDown += HandleDown;
        //   m_VRInput.OnUp += HandleUp;
          m_InteractiveItem.OnDown -= HandleDown;
           m_InteractiveItem.OnUp -= HandleUp;
          //  m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;

            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
   
        }

        private IEnumerator FillBar()
        {
            // When the bar starts to fill, reset the timer.
            m_Timer = 0f;

            // The amount of time it takes to fill is either the duration set in the inspector, or the duration of the radial.
            float fillTime = m_SelectionRadial != null ? m_SelectionRadial.SelectionDuration : m_Duration;

            // Until the timer is greater than the fill time...
            while (m_Timer < fillTime)
            {
                // ... add to the timer the difference between frames.
                m_Timer += Time.deltaTime;

                // Set the value of the slider or the UV based on the normalised time.
               // SetSliderValue(m_Timer / fillTime);

                // Wait until next frame.
                yield return null;

                // If the user is still looking at the bar, go on to the next iteration of the loop.
                if (m_GazeOver)
                    continue;

                // If the user is no longer looking at the bar, reset the timer and bar and leave the function.
                m_Timer = 0f;
             //   SetSliderValue(0f);
                yield break;
            }

            // If the loop has finished the bar is now full.
            m_BarFilled = true;

            // If anything has subscribed to OnBarFilled call it now.
            if (OnBarFilled != null)
                OnBarFilled();

            // Play the clip for when the bar is filled.
            m_Audio.clip = m_OnFilledClip;
            m_Audio.Play();
            ScoreScript.scoreValue += 10;
            //  Debug.Log("Filling");



            // If the bar should be disabled once it is filled, do so now. 
            //this statement cause problem of setting radial back to normal
      //      if (m_DisableOnBarFill)
        //        enabled = false;
        }


        /////////////////////////
        //Handle the Over event

        private void HandleDown()
        {
            // If the user is looking at the bar start the FillBar coroutine and store a reference to it.
            if (m_GazeOver)
                m_FillBarRoutine = StartCoroutine(FillBar());
          //  ScoreScript.scoreValue += 10;
             GetComponent<AudioSource>().Play();
            Debug.Log("Show Down state");
        }

        private void HandleUp()
        {
            // If the coroutine has been started (and thus we have a reference to it) stop it.
            if (m_FillBarRoutine != null)
                StopCoroutine(m_FillBarRoutine);

            // Reset the timer and bar values.
            m_Timer = 0f;
         //   SetSliderValue(0f);
            Debug.Log("Show Up state");
            m_ReduceScoreTimer = 0.0f;
        }

        private void HandleOver()
        {
            Debug.Log("Show over state");
            m_GazeOver = true;
            m_SelectionRadial.Show();
            m_Renderer.material = m_OverMaterial;
            m_Audio.clip = m_OnOverClip;
            m_Audio.Play();
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_GazeOver = false;
            m_SelectionRadial.Hide();
            m_Renderer.material = m_NormalMaterial;
            // If the coroutine has been started (and thus we have a reference to it) stop it.
            if (m_FillBarRoutine != null)
                StopCoroutine(m_FillBarRoutine);

            // Reset the timer and bar values.
            m_Timer = 0f;
            //  SetSliderValue(0f);
           

        }


        //private void HandleSelectionComplete()
        //{
        //    // If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
        //    if (m_GazeOver)
        //    {
        //        // StartCoroutine(ActivateButton());
        //        Debug.Log("Complete");
        //        //  ScoreScript.scoreValue += 10;
        //    }
        //}


        //   private IEnumerator ActivateButton()
        //   {
        //     // If the camera is already fading, ignore.
        //      if (m_CameraFade.IsFading)
        //          yield break;

        //    //  If anything is subscribed to the OnButtonSelected event, call it.
        //       if (OnButtonSelected != null)
        //          OnButtonSelected(this);

        ////  Wait for the camera to fade out.
        //        yield return StartCoroutine(m_CameraFade.BeginFadeOut(false));
        //        yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

        //  //     Load the level.
        //        SceneManager.LoadScene(m_SceneToLoad, LoadSceneMode.Single);
        //  }
    }

}