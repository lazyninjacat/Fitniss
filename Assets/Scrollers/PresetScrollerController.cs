using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class PresetScrollerController : MonoBehaviour, IEnhancedScrollerDelegate 
{
    private List<PresetScrollerData > _data;

    public EnhancedScroller presetScroller;
    public PresetCellView presetCellViewPrefab;

	void Start () 
    {
        _data = new List<PresetScrollerData >();

        _data.Add(new PresetScrollerData () { presetName = "Lion" });
        _data.Add(new PresetScrollerData () { presetName = "Bear" });
        _data.Add(new PresetScrollerData () { presetName = "Eagle" });
        _data.Add(new PresetScrollerData () { presetName = "Dolphin" });
        _data.Add(new PresetScrollerData () { presetName = "Ant" });
        _data.Add(new PresetScrollerData () { presetName = "Cat" });
        _data.Add(new PresetScrollerData () { presetName = "Sparrow" });
        _data.Add(new PresetScrollerData () { presetName = "Dog" });
        _data.Add(new PresetScrollerData () { presetName = "Spider" });
        _data.Add(new PresetScrollerData () { presetName = "Elephant" });
        _data.Add(new PresetScrollerData () { presetName = "Falcon" });
        _data.Add(new PresetScrollerData () { presetName = "Mouse" });

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
