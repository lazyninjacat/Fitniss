using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using TMPro;
using System;

public class CircuitDetailsController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<CircuitDetailsData> _data;

    public EnhancedScroller presetScroller;
    public CircuitDetailsCellView presetCellViewPrefab;

    private DataService dataService;

    [SerializeField] TextMeshProUGUI circuitNameText;

    void Start()
    {

        dataService = StartupScript.ds;
        _data = new List<CircuitDetailsData>();


        string circuitName = circuitNameText.text;



        int count = dataService.GetCircuitCount(circuitNameText.text);

        for (int i=0; i < count; i++)
        {            
            _data.Add(new CircuitDetailsData() { orderID = i+1, exerciseName = dataService.GetExerciseName(circuitName, i+1), exerciseAmount = Int32.Parse(dataService.GetExerciseAmount(circuitName, i+1)), exerciseType = dataService.GetExerciseType(circuitName, i+1) });
        }



        presetScroller.Delegate = this;
        presetScroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 50f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        CircuitDetailsCellView cellView = scroller.GetCellView(presetCellViewPrefab) as CircuitDetailsCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
