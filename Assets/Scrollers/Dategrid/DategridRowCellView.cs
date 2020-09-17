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
                cellText.text = data.date;


            }
            else
            {
                dateBackgroundImage.color = new Color(10, 10, 10, 100);
                cellText.text = data.date;
            }

            if (data.future == true)
            {
                dateBackgroundImage.color = new Color(1, 1, 1, 0);
                cellText.text = data.date;
            }
        }
    }
}
