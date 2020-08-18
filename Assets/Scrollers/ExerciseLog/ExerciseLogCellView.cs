using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;
using TMPro;

/// <summary>
/// 
/// This is the view of our cell which handles how the cell looks.
/// </summary>
public class ExerciseLogCellView : EnhancedScrollerCellView
{
    /// <summary>
    /// A reference to the UI Text element to display the cell data
    /// </summary>
    public TextMeshProUGUI exerciseName;
    public TextMeshProUGUI exerciseAmount;
    public TextMeshProUGUI timestamp;

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ExerciseLogScrollerData data)
    {
        // update the UI text with the cell data
        exerciseName.text = data.exerciseName;

        if (data.exerciseType == "time")
        {
            exerciseAmount.text = data.exerciseAmount + " min";
        }
        else
        {
            exerciseAmount.text = data.exerciseAmount;
        }

        timestamp.text = data.Timestamp.ToString();
    }
}