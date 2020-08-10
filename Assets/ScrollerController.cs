using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate 
{
    private List<ScrollerData> _data;

    public EnhancedScroller myScroller;
    public PresetCellView presetCellViewPrefab;

	void Start () 
    {
        _data = new List<ScrollerData>();

        _data.Add(new ScrollerData() { presetName = "Lion" });
        _data.Add(new ScrollerData() { presetName = "Bear" });
        _data.Add(new ScrollerData() { presetName = "Eagle" });
        _data.Add(new ScrollerData() { presetName = "Dolphin" });
        _data.Add(new ScrollerData() { presetName = "Ant" });
        _data.Add(new ScrollerData() { presetName = "Cat" });
        _data.Add(new ScrollerData() { presetName = "Sparrow" });
        _data.Add(new ScrollerData() { presetName = "Dog" });
        _data.Add(new ScrollerData() { presetName = "Spider" });
        _data.Add(new ScrollerData() { presetName = "Elephant" });
        _data.Add(new ScrollerData() { presetName = "Falcon" });
        _data.Add(new ScrollerData() { presetName = "Mouse" });

        myScroller.Delegate = this;
        myScroller.ReloadData();
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
