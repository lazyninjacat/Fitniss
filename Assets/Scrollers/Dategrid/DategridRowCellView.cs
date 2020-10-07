using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;


/// <summary>
/// This is the sub cell of the row cell
/// </summary>
public class DategridRowCellView : MonoBehaviour
{
    public GameObject container;
    public Image dateBackgroundImage;
    public Text cellText;

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(DategridData data)
    {
        // this cell was outside the range of the data, so we disable the container.
        // Note: We could have disable the cell gameobject instead of a child container,
        // but that can cause problems if you are trying to get components (disabled objects are ignored).
        container.SetActive(data != null);

        if (data != null)
        {
            if (data.session == true)
            {
                dateBackgroundImage.color = new Color(0, 255, 0);
                //int position = data.date.IndexOf("/");
                //cellText.text = data.date.Substring(0, position);
                cellText.text = data.date.Day.ToString();
            }
            else
            {
                dateBackgroundImage.color = new Color(10, 10, 10, 100);
                //int position = data.date.IndexOf("/");
                //cellText.text = data.date.Substring(0, position);
                cellText.text = data.date.Day.ToString();
            }

            if (data.future == true)
            {
                dateBackgroundImage.color = new Color(1, 1, 1, 0);
                //int position = data.date.IndexOf("/");
                //cellText.text = data.date.Substring(0, position);
                cellText.text = "";
            }

            if (data.isMonthCell == true)
            {
                dateBackgroundImage.color = new Color(1, 1, 1, 0);
                if (data.isFirst == "1")
                {
                    cellText.text = GetMonthString(data.date.Month.ToString());
                }
                else
                {
                    cellText.text = "";
                }
               
                //cellText.text = data.score;

            }

            //if (data.date == DateTime.Today.ToShortDateString() && !data.isMonthCell)
            //{
            //    dateBackgroundImage.color = new Color(1, 0, 0, 1);
            //    cellText.text = "";

            //}
        }
    }

    private string GetMonthString(string monthInt)
    {
        string tempStr = "";
        if (monthInt == "1")
        {
            tempStr = "Jan";
        }
        else if (monthInt == "2")
        {
            tempStr = "Feb";
        }
        else if (monthInt == "3")
        {
            tempStr = "Mar";
        }
        else if (monthInt == "4")
        {
            tempStr = "Apr";
        }
        else if (monthInt == "5")
        {
            tempStr = "May";
        }
        else if (monthInt == "6")
        {
            tempStr = "Jun";
        }
        else if (monthInt == "7")
        {
            tempStr = "Jul";
        }
        else if (monthInt == "8")
        {
            tempStr = "Aug";
        }
        else if (monthInt == "9")
        {
            tempStr = "Sep";
        }
        else if (monthInt == "10")
        {
            tempStr = "Oct";
        }
        else if (monthInt == "11")
        {
            tempStr = "Nov";
        }
        else if (monthInt == "12")
        {
            tempStr = "Dec";
        }
        return tempStr;
    }

}
