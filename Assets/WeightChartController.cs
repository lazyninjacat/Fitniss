using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using TMPro;

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
            _data.Add(new WeightChartCellData() { fillBar = (row.Weight - lowerWeightLimit) / (upperWeightLimit - lowerWeightLimit), date = row.Date.Day, month = row.Date.Month, weight = (float)row.Weight });
            Debug.Log("Timestamp = " + row.Date.ToString() + "Day = " + row.Date.Day.ToString() + ". Month = " + row.Date.Month.ToString() + ". Weight = " + row.Weight.ToString());
        }


        weightChartScroller.Delegate = this;
        weightChartScroller.ReloadData();

        upperLimitText.text = upperWeightLimit.ToString();
        lowerLimitText.text = lowerWeightLimit.ToString();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 20f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        WeightChartCellView cellView = scroller.GetCellView(weightChartCellViewPrefab) as WeightChartCellView;

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }

}
