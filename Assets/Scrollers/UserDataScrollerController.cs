using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class UserDataScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<UserDataScrollerData> _data;

    public EnhancedScroller myScroller;
    public UserDataCellView UserDataCellViewPrefab;

    private DataService dataService;
    private List<UserLog> userLog;


    void Start()
    {

        dataService = StartupScript.ds;

     

        _data = new List<UserDataScrollerData>();




        _data.Add(new UserDataScrollerData() { userDataTimestamp = "Lion" });
        _data.Add(new UserDataScrollerData() { userDataTimestamp = "Bear" });
        _data.Add(new UserDataScrollerData() { userDataTimestamp = "Eagle" });
 

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
        UserDataCellView cellView = scroller.GetCellView(UserDataCellViewPrefab) as UserDataCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
