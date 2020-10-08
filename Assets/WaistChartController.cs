using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using TMPro;
using System;

public class WaistChartController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<WaistChartCellData> _data;
    public EnhancedScroller waistChartScroller;
    public WaistChartCellView waistChartCellViewPrefab;
    [SerializeField] TextMeshProUGUI upperLimitText;
    [SerializeField] TextMeshProUGUI lowerLimitText;
    [SerializeField] TextMeshProUGUI lowerMidText;
    [SerializeField] TextMeshProUGUI UpperMidText;

    private DataService dataService;
    private float upperLimit;
    private float lowerLimit;
    private float lowerMid;
    private float UpperMid;

    public EnhancedScroller.TweenType vScrollerTweenType = EnhancedScroller.TweenType.immediate;
    public float vScrollerTweenTime = 0f;

    void Start()
    {
        dataService = StartupScript.ds;
        _data = new List<WaistChartCellData>();

        // Find and set the upper and lower waist limits from the userlog table data
        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Waist > upperLimit - 0.5f)
            {
                upperLimit = row.Waist + 0.5f;
            }
        }

        lowerLimit = upperLimit - 0.5f;

        float tempFloat = 0;

        tempFloat = (float)((upperLimit - lowerLimit) / 4);

        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Waist - 0.5f < lowerLimit)
            {
                lowerLimit = row.Waist - 0.5f;
            }
        }

        lowerMid = lowerLimit + tempFloat;
        UpperMid = upperLimit - tempFloat;

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
                            _data.Add(new WaistChartCellData() { fillBar = (row.Waist - lowerLimit) / (upperLimit - lowerLimit), date = row.Date.Day, month = 0, waist = (float)row.Waist });
                        }
                        else
                        {
                            _data.Add(new WaistChartCellData() { fillBar = (row.Waist - lowerLimit) / (upperLimit - lowerLimit), date = row.Date.Day, month = row.Date.Month, waist = (float)row.Waist });
                        }
                        currentMonthInt = row.Date.Month;
                    }
                }
            }
            else
            {
                if (pair.Key.Month == currentMonthInt)
                {
                    _data.Add(new WaistChartCellData() { fillBar = 0, date = pair.Key.Day, month = 0, waist = 0 });
                }
                else
                {
                    _data.Add(new WaistChartCellData() { fillBar = 0, date = pair.Key.Day, month = pair.Key.Month, waist = 0 });
                }
                currentMonthInt = pair.Key.Month;
            }
        }
        waistChartScroller.Delegate = this;
        waistChartScroller.ReloadData();
        upperLimitText.text = upperLimit.ToString();
        lowerLimitText.text = lowerLimit.ToString();
        UpperMidText.text = UpperMid.ToString();
        lowerMidText.text = lowerMid.ToString();
        waistChartScroller.JumpToDataIndex(400, 0, 0, true, vScrollerTweenType, vScrollerTweenTime, null, EnhancedScroller.LoopJumpDirectionEnum.Closest);
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
        WaistChartCellView cellView = scroller.GetCellView(waistChartCellViewPrefab) as WaistChartCellView;
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
}
