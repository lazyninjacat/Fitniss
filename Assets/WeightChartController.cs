using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class WeightChartController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<WeightChartCellData> _data;

    public EnhancedScroller weightChartScroller;
    public WeightChartCellView weightChartCellViewPrefab;

    private DataService dataService;

    private float upperWeightLimit;
    private float lowerWeightLimit;

    void Start()
    {


        dataService = StartupScript.ds;
        _data = new List<WeightChartCellData>();

        // Find and set the upper and lower weight limits from the userlog table data
        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Weight > upperWeightLimit - 10)
            {
                upperWeightLimit = row.Weight + 10;
            }           
        }

        lowerWeightLimit = upperWeightLimit - 20;

        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Weight < lowerWeightLimit + 10)
            {
                lowerWeightLimit = row.Weight - 10;
            }
        }

     
        Debug.Log("upper limit = " + upperWeightLimit);
        Debug.Log("lower limit = " + lowerWeightLimit);

        foreach (var row in dataService.GetUserLogTable())
        {        
            _data.Add(new WeightChartCellData() { weightFloat = (row.Weight - lowerWeightLimit) / (upperWeightLimit - lowerWeightLimit) });
        }


        weightChartScroller.Delegate = this;
        weightChartScroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 10f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        WeightChartCellView cellView = scroller.GetCellView(weightChartCellViewPrefab) as WeightChartCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
