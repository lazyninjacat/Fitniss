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
        _data = new List<WeightChartCellData>();

        upperLimit = 0;
        UpperMid = 0;
        lowerMid = 0;
        lowerLimit = 0;
        
        // Find and set the upper and lower weight limits from the userlog table data
        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Weight > upperLimit - 2)
            {
                upperLimit = row.Weight + 2;
                Debug.Log("upperLimit = " + upperLimit);
            }           
        }

        lowerLimit = upperLimit - 2;

        float tempFloat = 0;


        foreach (var row in dataService.GetUserLogTable())
        {
            if (row.Weight - 2 < lowerLimit)
            {
                lowerLimit = row.Weight - 2;
                Debug.Log("lowerLimit = " + lowerLimit);

            }
        }

        tempFloat = (float)((upperLimit - lowerLimit) / 4);


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
                            _data.Add(new WeightChartCellData() { fillBar = (row.Weight - lowerLimit) / (upperLimit - lowerLimit), date = row.Date.Day, month = 0, weight = (float)row.Weight });
                        }
                        else
                        {
                            _data.Add(new WeightChartCellData() { fillBar = (row.Weight - lowerLimit) / (upperLimit - lowerLimit), date = row.Date.Day, month = row.Date.Month, weight = (float)row.Weight });
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
        upperLimitText.text = Math.Ceiling(upperLimit).ToString();
        lowerLimitText.text = Math.Ceiling(lowerLimit).ToString();
        UpperMidText.text = Math.Ceiling(UpperMid).ToString();
        lowerMidText.text = Math.Ceiling(lowerMid).ToString();
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
