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

    private string currentCircuit;

    private int circuitCount;

    private bool circuitComplete;

    private float circuitProgress;

    private string selectedCircuitPreset;

    private int duration;

    private DateTime startTime;
    private DateTime endTime;




    // Start is called before the first frame update
    void Start()
    {
        dataService = StartupScript.ds;
        currentCircuit = dataService.GetCurrentCircuit();
        circuitComplete = false;
        circuitProgress = 0;

        currentPresetText.text = currentCircuit;

        graphcontroller = FindObjectOfType<GraphController>();
        circuitOrder = 0;
        circuitCount = dataService.GetCurrentCircuitCount(currentCircuit);
        CurrentExerciseName.text = dataService.GetExerciseName(currentCircuit, circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(currentCircuit, circuitOrder);

        startTime = new DateTime();
        endTime = new DateTime();


    }



    public void StartButton()
    {
        circuitComplete = false;
        circuitOrder = 1;
        CurrentExerciseName.text = dataService.GetExerciseName(currentCircuit, circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(currentCircuit, circuitOrder);

        startTime = new DateTime();
        startTime = DateTime.Now;

        if (dataService.GetExerciseType(currentCircuit, circuitOrder) == "time")
        {
            doneButton.interactable = false;
            StartTimer();
        }

    }

    public void ConfirmLoadPresetButton()
    {
        if (dataService.UpdateCurrentCircuit(selectedCircuitPreset) == 1)
        {
            currentPresetText.text = "Current Preset: " + selectedCircuitPreset;
            Debug.Log("Loading preset " + selectedCircuitPreset);
            currentCircuit = selectedCircuitPreset;
        }

   

    }

    public void LoadPresetButton(string presetName)
    {
        selectedCircuitPreset = presetName;
        confirmLoadPresetText.text = selectedCircuitPreset;
        Debug.Log("Preset staged: " + selectedCircuitPreset);
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
            timeProgressor.SetValue(0);

            if (circuitOrder <= circuitCount)
            {
                CurrentExerciseName.text = dataService.GetExerciseName(currentCircuit, circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(currentCircuit, circuitOrder);

                if (dataService.GetExerciseType(currentCircuit, circuitOrder) == "time")
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
                endTime = DateTime.Now;
                duration = endTime.Minute - startTime.Minute;
                Debug.Log("Duration = " + duration);
                dataService.AddExerciseLogEntry(DateTime.Now, currentCircuit, duration);
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

}
