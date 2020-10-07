using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using System;
using System.Data;

public class UserDataScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<UserDataScrollerData> _data;

    public EnhancedScroller useDataScroller;

    public UserDataCellView UserDataCellViewPrefab;

    private DataService dataService;

    public UserDataCellView userDataCellViewPrefab;





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
            }); ;
        }


        useDataScroller.Delegate = this;
        useDataScroller.ReloadData();

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
        UserDataCellView useDataCellView = scroller.GetCellView(userDataCellViewPrefab) as UserDataCellView;

        useDataCellView.SetData(_data[dataIndex]);

        return useDataCellView;
    }

}
