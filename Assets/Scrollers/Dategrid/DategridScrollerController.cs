using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Linq;
using TMPro;
using UnityEngine.UIElements;

/// <summary>
/// This example shows how to simulate a grid with a fixed number of cells per row
/// The data is stored as normal, but the differences in this example are:
/// 
/// 1) The scroller is told the data count is the number of data elements divided by the number of cells per row
/// 2) The cell view is passed a reference to the data set with the offset index of the first cell in the row
public class DategridScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private SmallList<DategridData> _data;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. The cell view will
    /// hold references to each row sub cell
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    public int numberOfCellsPerRow = 8;

    private DataService dataService;

    public EnhancedScroller.TweenType vScrollerTweenType = EnhancedScroller.TweenType.immediate;
    public float vScrollerTweenTime = 0f;


    /// <summary>
    /// Be sure to set up your references to the scroller after the Awake function. The 
    /// scroller does some internal configuration in its own Awake function. If you need to
    /// do this in the Awake function, you can set up the script order through the Unity editor.
    /// In this case, be sure to set the EnhancedScroller's script before your delegate.
    /// 
    /// In this example, we are calling our initializations in the delegate's Start function,
    /// but it could have been done later, perhaps in the Update function.
    /// </summary>
    void Start()
    {
        //get the sqlite dataservice
        dataService = StartupScript.ds;

        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;

        // load in a large set of data
        LoadData();
    }
        
    private void LoadData()
    {
        DateTime today = DateTime.Today.Date;

        Dictionary<DateTime, bool> calendarMap = new Dictionary<DateTime, bool>();

        int tempInt = 0;
        if (today.DayOfWeek == DayOfWeek.Sunday)
        {
            tempInt = 0;
        }
        else if (today.DayOfWeek == DayOfWeek.Monday)
        {
            tempInt = 1;
        }
        else if (today.DayOfWeek == DayOfWeek.Tuesday)
        {
            tempInt = 2;
        }
        else if (today.DayOfWeek == DayOfWeek.Wednesday)
        {
            tempInt = 3;
        }
        else if (today.DayOfWeek == DayOfWeek.Thursday)
        {
            tempInt = 4;        
        }
        else if (today.DayOfWeek == DayOfWeek.Friday)
        {
            tempInt = 5;
        }
        else if (today.DayOfWeek == DayOfWeek.Saturday)
        {
            tempInt = 6;
        }


        for (int i = 364 + tempInt; i >= 0; i--)
        {         
            if (i >= 1)
            {
                calendarMap.Add(today.AddDays(i * (-1)).Date, false);             
            }
            else if (i == 0)
            {
                calendarMap.Add(today.Date, false);          
            }
        }


        if (today.DayOfWeek == DayOfWeek.Sunday)
        {
            Debug.Log("Sunday");
            for (int i = 1; i < 7; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Monday)
        {
            Debug.Log("Monday");
            for (int i = 1; i < 6; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Tuesday)
        {
            Debug.Log("Tuesday");
            for (int i = 1; i < 5; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Wednesday)
        {
            Debug.Log("Wednesday");
            for (int i = 1; i < 4; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Thursday)
        {
            Debug.Log("Thursday");
            for (int i = 1; i < 3; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Friday)
        {
            Debug.Log("Friday");
            for (int i = 1; i <2; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Saturday)
        {
            Debug.Log("Saturday");
            for (int i = 1; i < 1; i++)
            {
                calendarMap.Add(today.AddDays(i), false);
            }
        }

        foreach (var row in dataService.GetExerciseLogTable())
        {
            calendarMap[row.Date] = true;
        }

        _data = new SmallList<DategridData>();

        int currentMonth;
        int thisMonth;


        _data.Add(new DategridData() { isMonthCell = true, date = today.Date.AddDays(-364), isFirst = "1" });


        thisMonth = today.Date.AddDays(-364).Month;

        currentMonth = thisMonth;

        int counter = 0;

        foreach (var pair in calendarMap)
        {   
            if (counter == 7)
            {
                thisMonth = pair.Key.Month;
              
                if (thisMonth == currentMonth)
                {
                    _data.Add(new DategridData() { isMonthCell = true, date = pair.Key.Date, isFirst = "0" });
                }
                else
                {
                    _data.Add(new DategridData() { isMonthCell = true, date = pair.Key.Date, isFirst = "1" });
                }

                currentMonth = thisMonth;
                counter = 0;               
            }

            if (pair.Key.Date == today.AddDays(1).Date || pair.Key.Date == today.AddDays(2).Date || pair.Key.Date == today.AddDays(3).Date || pair.Key.Date == today.AddDays(4).Date || pair.Key.Date == today.AddDays(5).Date || pair.Key.Date == today.AddDays(6).Date)
            {
                _data.Add(new DategridData() { session = false, future = true, date = pair.Key.Date });
            }
            else
            {
                _data.Add(new DategridData() { session = pair.Value, future = false, date = pair.Key.Date });
            }
            counter++;
        }
        scroller.ReloadData();
        scroller.JumpToDataIndex(400, 0, 0, true, vScrollerTweenType, vScrollerTweenTime, null, EnhancedScroller.LoopJumpDirectionEnum.Closest);
    }

    #region EnhancedScroller Handlers

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 25f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
    // first, we get a cell from the scroller by passing a prefab.
    // if the scroller finds one it can recycle it will do so, otherwise
    // it will create a new cell.
    DategridCellView cellView = scroller.GetCellView(cellViewPrefab) as DategridCellView;

        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

        // pass in a reference to our data set with the offset for this cell
        cellView.SetData(ref _data, dataIndex * numberOfCellsPerRow);

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}
