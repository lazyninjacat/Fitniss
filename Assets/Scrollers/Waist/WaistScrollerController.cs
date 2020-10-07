using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using System;
using System.Data;

public class WaistScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<UserDataScrollerData> _data;

    public EnhancedScroller weightScroller;

    public WaistCellView WaisttCellViewPrefab;

    private DataService dataService;





    void Start()
    {
        dataService = StartupScript.ds;

        _data = new List<UserDataScrollerData>();

        IEnumerable<UserLog> useDataLog = dataService.GetUserLog();

        foreach (var row in useDataLog)
        {
            _data.Add(new UserDataScrollerData()
            {
                timestamp = row.Date.ToString(),
                weight = row.Weight,
                waist = row.Waist
            });
        }


        weightScroller.Delegate = this;
        weightScroller.ReloadData();

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
        WaistCellView cellView = scroller.GetCellView(WaisttCellViewPrefab) as WaistCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
