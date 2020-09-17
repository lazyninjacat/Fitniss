using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

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

    public int numberOfCellsPerRow = 7;

    private DataService dataService;

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
        


 
  
    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private void LoadData()
    {
        // set up some simple data

        DateTime today = DateTime.Today.Date;

        Dictionary<string, bool> calendarMap = new Dictionary<string, bool>();

        // add all the days backward 364 days from today

        int tempInt = 0;
        if (today.DayOfWeek == DayOfWeek.Sunday)
        {
            tempInt = 6;
        }
        else if (today.DayOfWeek == DayOfWeek.Monday)
        {
            tempInt = 5;
        }
        else if (today.DayOfWeek == DayOfWeek.Tuesday)
        {
            tempInt = 4;
        }
        else if (today.DayOfWeek == DayOfWeek.Wednesday)
        {
            tempInt = 3;
        }
        else if (today.DayOfWeek == DayOfWeek.Thursday)
        {
            tempInt = 2;        
        }
        else if (today.DayOfWeek == DayOfWeek.Friday)
        {
            tempInt = 1;
        }
        else if (today.DayOfWeek == DayOfWeek.Saturday)
        {
            tempInt = 0;
        }


        for (int i = 364 - tempInt; i > 0; i--)
        {         
            if (i > 1)
            {
                calendarMap.Add(today.AddDays(i * (-1)).ToShortDateString(), false);             
            }
            else if (i == 1)
            {
                calendarMap.Add(today.Date.ToShortDateString(), false);          
            }
        }


        if (today.DayOfWeek == DayOfWeek.Sunday)
        {
            Debug.Log("Sunday");
            for (int i = 1; i < 7; i++)
            {
                calendarMap.Add(today.AddDays(i).ToShortDateString(), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Monday)
        {
            Debug.Log("Monday");
            for (int i = 1; i < 6; i++)
            {
                calendarMap.Add(today.AddDays(i).ToShortDateString(), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Tuesday)
        {
            Debug.Log("Tuesday");
            for (int i = 1; i < 5; i++)
            {
                calendarMap.Add(today.AddDays(i).ToShortDateString(), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Wednesday)
        {
            Debug.Log("Wednesday");
            for (int i = 1; i < 4; i++)
            {
                calendarMap.Add(today.AddDays(i).ToShortDateString(), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Thursday)
        {
            Debug.Log("Thursday");
            for (int i = 1; i < 3; i++)
            {
                calendarMap.Add(today.AddDays(i).ToShortDateString(), false);
            }
        }
        else if (today.DayOfWeek == DayOfWeek.Friday)
        {
            calendarMap.Add(today.AddDays(1).ToShortDateString(), false);
        }

        _data = new SmallList<DategridData>();

        foreach (var row in dataService.GetExerciseLogTable())
        {
            calendarMap[row.Timestamp] = true;
            Debug.Log("Adding " + row.Timestamp);
        }

        foreach (var pair in calendarMap)
        {
            if (pair.Key == today.AddDays(1).ToShortDateString() || pair.Key == today.AddDays(2).ToShortDateString() || pair.Key == today.AddDays(3).ToShortDateString() || pair.Key == today.AddDays(4).ToShortDateString() || pair.Key == today.AddDays(5).ToShortDateString() || pair.Key == today.AddDays(6).ToShortDateString())
            {
                _data.Add(new DategridData() { session = false, future = true, date = pair.Key });
            }
            else
            {
                _data.Add(new DategridData() { session = pair.Value, future = false, date = pair.Key });
            }
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
    }

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated.
    /// For this example, the count is the number of data elements divided by the number of cells per row (rounded up using Mathf.CeilToInt)
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 25f;
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
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
