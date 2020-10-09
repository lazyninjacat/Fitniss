using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using EnhancedUI.EnhancedScroller;
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
    [SerializeField] EnhancedScroller dategridScrollRect;
    [SerializeField] TextMeshProUGUI debugText;

    private int _duration;
    private int _circuitOrder;
    private int _circuitCount;
    private string _selectedCircuitPreset;
    private string _currentCircuit;
    private bool _circuitComplete;
    private float _circuitProgress;
    private DateTime _startTime;
    private DateTime _endTime;
    private GraphController _graphcontroller;
    private DataService _dataService;




    // Start is called before the first frame update
    void Start()
    {
        _dataService = StartupScript.ds;
        _currentCircuit = _dataService.GetCurrentCircuit();
        _circuitComplete = false;
        _circuitProgress = 0;

        currentPresetText.text = _currentCircuit;

        _graphcontroller = FindObjectOfType<GraphController>();
        _circuitOrder = 0;
        _circuitCount = _dataService.GetCurrentCircuitCount(_currentCircuit);
        CurrentExerciseName.text = _dataService.GetExerciseName(_currentCircuit, _circuitOrder);
        CurrentExerciseAmount.text = _dataService.GetExerciseAmount(_currentCircuit, _circuitOrder);

        _startTime = new DateTime();
        _endTime = new DateTime();

        dategridScrollRect.ScrollPosition = 1030;


    }



    public void StartButton()
    {
        _circuitComplete = false;
        _circuitOrder = 1;
        CurrentExerciseName.text = _dataService.GetExerciseName(_currentCircuit, _circuitOrder);
        CurrentExerciseAmount.text = _dataService.GetExerciseAmount(_currentCircuit, _circuitOrder);

        _startTime = new DateTime();
        _startTime = DateTime.Now;

        if (_dataService.GetExerciseType(_currentCircuit, _circuitOrder) == "time")
        {
            //doneButton.interactable = false;
            StartTimer();
        }
    }

    public void ConfirmLoadPresetButton()
    {
        

        if (_dataService.UpdateCurrentCircuit(_selectedCircuitPreset) == 1)
        {
            currentPresetText.text = "Current Preset: " + _selectedCircuitPreset;
            Debug.Log("Loading preset " + _selectedCircuitPreset);
            _currentCircuit = _selectedCircuitPreset;
        }        
    }

    public void LoadPresetButton(string presetName)
    {
        _selectedCircuitPreset = presetName;
        confirmLoadPresetText.text = _selectedCircuitPreset;
        Debug.Log("Preset staged: " + _selectedCircuitPreset);
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
        if (!_circuitComplete)
        {
            _circuitOrder++;

            circuitProgressor.SetValue((float)_circuitOrder / (float)_circuitCount);
            timeProgressor.SetValue(0);

            if (_circuitOrder <= _circuitCount)
            {
                CurrentExerciseName.text = _dataService.GetExerciseName(_currentCircuit, _circuitOrder);
                CurrentExerciseAmount.text = _dataService.GetExerciseAmount(_currentCircuit, _circuitOrder);

                if (_dataService.GetExerciseType(_currentCircuit, _circuitOrder) == "time")
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
                _endTime = DateTime.Now;
                _duration = _endTime.Minute - _startTime.Minute;
                Debug.Log("Duration = " + _duration);

                int tempCircuitScore = 0;

                foreach (var row in _dataService.GetCircuitsTable())
                {
                    if (row.CircuitName == _currentCircuit)
                    {
                        tempCircuitScore = row.Score;
                    }
                }



                foreach (var row in _dataService.GetExerciseLogTable())
                {
                    if (row.Date.Date == DateTime.Today.Date)
                    {
                        _dataService.UpdateExersiceLogScore(row.Date.Date, tempCircuitScore);
                    }
                    else
                    {
                        _dataService.AddExerciseLogEntry(DateTime.Today.Date, _currentCircuit, _duration, tempCircuitScore);

                    }
                }

                CurrentExerciseName.text = "Complete!!!";
                CurrentExerciseAmount.text = "";
                _circuitComplete = true;            
            }
        }
        else
        {
            _graphcontroller.GoToNodeByName("NewRecord");
        }     
    }
}
