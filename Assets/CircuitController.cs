using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;
    [SerializeField] Progressor circuitProgressor;
    [SerializeField] Progressor timeProgressor;

    [SerializeField] TextMeshProUGUI confirmLoadPresetText;
    [SerializeField] TextMeshProUGUI currentPresetText;
    [SerializeField] Button doneButton;


    [SerializeField] GameObject countdownTimer;



    private GraphController graphcontroller;
    private int circuitOrder;

    private DataService dataService;

    private int circuitCount;

    private bool circuitComplete;

    private float circuitProgress;

    private string selectedCircuitPreset;



    // Start is called before the first frame update
    void Start()
    {
        circuitComplete = false;
        circuitProgress = 0;

        if (selectedCircuitPreset == null || selectedCircuitPreset == "")
        {
            currentPresetText.text = "Current Preset: Default";

        }

        graphcontroller = FindObjectOfType<GraphController>();
        dataService = StartupScript.ds;
        circuitOrder = 0;
        circuitCount = dataService.GetCircuitCount();
        CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
   
    }

    public void StartButton()
    {
        circuitComplete = false;
        circuitOrder = 1;
        CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);

        if (dataService.GetExerciseType(circuitOrder) == "time")
        {
            doneButton.interactable = false;
            StartTimer();
        }

    }

    public void ConfirmLoadPresetButton()
    {
        currentPresetText.text = "Current Preset: " + selectedCircuitPreset;
        Debug.Log("Loading preset " + selectedCircuitPreset);
    }

    public void LoadPresetButton(string presetName)
    {
        selectedCircuitPreset = presetName;
        confirmLoadPresetText.text = selectedCircuitPreset;
        Debug.Log("Preset staged: " + selectedCircuitPreset);
    }

   

    public void TestSceneButton()
    {
        SceneManager.LoadScene("MediaDemo");
    }


    private IEnumerator TimerHelper(int minutes)
    {
        Debug.Log("timer helper");
        CurrentExerciseAmount.text = minutes + " min";
        countdownTimer.SetActive(true);
        timeProgressor.SetValue(0);


        int seconds = minutes * 60;

        for (int i = 1; i <= seconds; i++)
        {            
            yield return new WaitForSeconds(1);
            timeProgressor.SetValue(i / (float)seconds);
            CurrentExerciseAmount.text = (seconds - i) + " seconds";

        }

        countdownTimer.SetActive(false);

        doneButton.interactable = true;

    }

    public void StartTimer()
    {
        StartCoroutine(TimerHelper(Int16.Parse(CurrentExerciseAmount.text)));
    }
 
    
    public void DoneButton()
    {
        Debug.Log("Done button");
        if (!circuitComplete)
        {
            circuitOrder++;

            circuitProgressor.SetValue((float)circuitOrder / (float)circuitCount);
            Debug.Log((((float)circuitOrder) / (float)circuitCount));


            if (circuitOrder <= circuitCount)
            {
                dataService.AddExerciseLogEntry(DateTime.Now.ToString(), CurrentExerciseName.text, CurrentExerciseAmount.text);
                CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);

                if (dataService.GetExerciseType(circuitOrder) == "time")
                {
                    Debug.Log("timed");
                    doneButton.interactable = false;
                    StartTimer();
                }
                else
                {
                    Debug.Log("not timed");

                    countdownTimer.SetActive(false);
                    doneButton.interactable = true;

                }

            }
            else
            {
                CurrentExerciseName.text = "Complete!!!";
                CurrentExerciseAmount.text = "";
                circuitComplete = true;
            }
        }
        else
        {
            graphcontroller.GoToNodeByName("StartMenu");
        }     
    }






    // Update is called once per frame
    void Update()
    {
        
    }




}
