using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class PresetScrollerController : MonoBehaviour, IEnhancedScrollerDelegate 
{
    private List<PresetScrollerData > _data;

    public EnhancedScroller presetScroller;
    public PresetCellView presetCellViewPrefab;

    private DataService dataService;

	void Start () 
    {
        
        dataService = StartupScript.ds;
        _data = new List<PresetScrollerData >();

        foreach (var row in dataService.GetCircuitsTable())
        {
            _data.Add(new PresetScrollerData() { presetName = row.CircuitName });
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
        PresetCellView cellView = scroller.GetCellView(presetCellViewPrefab) as PresetCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
