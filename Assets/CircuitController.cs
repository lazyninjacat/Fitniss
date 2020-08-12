using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;
    [SerializeField] Progressor progressor;
    [SerializeField] TextMeshProUGUI confirmLoadPresetText;
    [SerializeField] TextMeshProUGUI currentPresetText;


 


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
        circuitOrder = 1;
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

   

    public void DoneButton()
    {
        Debug.Log("Done button");
        if (!circuitComplete)
        {
            progressor.SetValue((float)circuitOrder / (float)circuitCount);
            Debug.Log((((float)circuitOrder) / (float)circuitCount));

            circuitOrder++;

            if (circuitOrder <= circuitCount)
            {
                dataService.AddExerciseLogEntry(DateTime.Now.ToString(), CurrentExerciseName.text, CurrentExerciseAmount.text);
                CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
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
