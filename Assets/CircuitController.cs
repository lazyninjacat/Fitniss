using Doozy.Engine.Nody;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;

    private GraphController graphcontroller;
    private int circuitOrder;

    private DataService dataService;

    private int circuitCount;

    private bool circuitComplete;
    
    // Start is called before the first frame update
    void Start()
    {
        circuitComplete = false;

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
            circuitOrder++;

            if (circuitOrder <= circuitCount)
            {
                CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
            }
            else
            {
                CurrentExerciseName.text = "DONE!!!";
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
