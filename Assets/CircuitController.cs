using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;
    [SerializeField] Progressor progressor;

    private GraphController graphcontroller;
    private int circuitOrder;

    private DataService dataService;

    private int circuitCount;

    private bool circuitComplete;

    private float circuitProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        circuitComplete = false;
        circuitProgress = 0;
 

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
                CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
            }
            else
            {
                CurrentExerciseName.text = "Complete!!!";
                CurrentExerciseAmount.text = "Tap Done to return to start menu";
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
