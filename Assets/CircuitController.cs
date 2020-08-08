using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;
    private int circuitOrder;

    private DataService dataService;

    private int circuitCount;
    
    // Start is called before the first frame update
    void Start()
    {
        dataService = StartupScript.ds;
        circuitOrder = 1;
        circuitCount = dataService.GetCircuitCount();
        Debug.Log("circuit count = " + circuitCount);
        CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
      
    }

    public void DoneButton()
    {
        Debug.Log("Done button");
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
        }
        Debug.Log("circuit order = " + circuitOrder + " / " + circuitCount);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
