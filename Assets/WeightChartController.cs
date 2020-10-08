using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using TMPro;
using System;

public class WeightChartController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<WeightChartCellData> _data;
    public EnhancedScroller weightChartScroller;
    public WeightChartCellView weightChartCellViewPrefab;
    [SerializeField] TextMeshProUGUI upperLimitText;
    [SerializeField] TextMeshProUGUI lowerLimitText;
    private DataService dataService;
    private float upperWeightLimit;
    private float lowerWeightLimit;
    public EnhancedScroller.TweenType vScrollerTweenType = EnhancedScroller.TweenType.immediate;
    public float vScrollerTweenTime = 0f;

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

        Dictionary<DateTime, bool> calendarMap = new Dictionary<DateTime, bool>();

        DateTime today = new DateTime();
        today = DateTime.Today.Date;

        for (int i = 60; i > 0; i--)
        {
            calendarMap.Add(today.AddDays(-i), false);
        }

        foreach (var row in dataService.GetUserLogTable())
        {
            if (calendarMap.ContainsKey(row.Date.Date))
            {
                calendarMap[row.Date.Date] = true;
            }
        }

        int currentMonthInt = 0;

        foreach (var pair in calendarMap)
        {
            if (pair.Value == true)
            {
                foreach (var row in dataService.GetUserLogTable())
                {
                    if (pair.Key.Date == row.Date.Date)
                    {
                        if (row.Date.Month == currentMonthInt)
                        {
                            _data.Add(new WeightChartCellData() { fillBar = (row.Weight - lowerWeightLimit) / (upperWeightLimit - lowerWeightLimit), date = row.Date.Day, month = 0, weight = (float)row.Weight });
                        }
                        else
                        {
                            _data.Add(new WeightChartCellData() { fillBar = (row.Weight - lowerWeightLimit) / (upperWeightLimit - lowerWeightLimit), date = row.Date.Day, month = row.Date.Month, weight = (float)row.Weight });
                        }
                        currentMonthInt = row.Date.Month;
                    }
                }
            }
            else
            {
                if (pair.Key.Month == currentMonthInt)
                {
                    _data.Add(new WeightChartCellData() { fillBar = 0, date = pair.Key.Day, month = 0, weight = 0 });
                }
                else
                {
                    _data.Add(new WeightChartCellData() { fillBar = 0, date = pair.Key.Day, month = pair.Key.Month, weight = 0 });
                }
                currentMonthInt = pair.Key.Month;
            }
        }
        weightChartScroller.Delegate = this;
        weightChartScroller.ReloadData();
        upperLimitText.text = upperWeightLimit.ToString();
        lowerLimitText.text = lowerWeightLimit.ToString();
        weightChartScroller.JumpToDataIndex(400, 0, 0, true, vScrollerTweenType, vScrollerTweenTime, null, EnhancedScroller.LoopJumpDirectionEnum.Closest);
    }


    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 15f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        WeightChartCellView cellView = scroller.GetCellView(weightChartCellViewPrefab) as WeightChartCellView;
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
}
