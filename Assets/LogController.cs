using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using EasyMobile;
using UnityEngine.EventSystems;
using System;

public class LogController : MonoBehaviour
{

    [SerializeField] RawImage newRecordImageTexture2D;
    [SerializeField] TMP_InputField weightInputField;
    [SerializeField] TMP_InputField waistInputField;
    [SerializeField] Button saveButton;


    [SerializeField] GameObject weightChart;
    [SerializeField] GameObject waistChart;
  

    private float _currentWeight;
    private float _currentWaist;
    private Texture2D _currentTexture;
    private string _picID;
    private GraphController _graphcontroller;
    private DataService _dataService;
    private string _recordID;




    // Start is called before the first frame update
    void Start()
    {
        _currentTexture = new Texture2D(150, 150);
        _dataService = StartupScript.ds;

        if (!RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
        }                       
    }

    // Update is called once per frame
    void Update()
    {
        if (weightInputField.text != null || weightInputField.text != "")
        {
            if (waistInputField.text != null || waistInputField.text != "")
            {
                if (_currentTexture != null)
                {
                    saveButton.interactable = true;
                }
                else
                {
                    saveButton.interactable = false;
                }
            }
            else
            {
                saveButton.interactable = false;
            }
        }
        else
        {
            saveButton.interactable = false;
        }
    }

    public void TakePicture()
    {
        Debug.Log("Take Picture");
        EasyMobile.CameraType cameraType = EasyMobile.CameraType.Front;
        Media.Camera.TakePicture(cameraType, TakePictureCallback);
    }

    private void TakePictureCallback(string error, MediaResult result)
    {
        Debug.Log("**************************************** Take Picture Callback");

        if (!string.IsNullOrEmpty(error))
        {
            Debug.Log("Error on take picture with native camera app");
        }
        else
        {
            Media.Gallery.LoadImage(result, LoadImageCallback);
        }
    }


    private void SaveTexture()
    {
        Debug.Log("**************************************** Save Texture");

        if (FileAccessUtil.SavePic(_currentTexture, _picID))
        {
            Debug.Log("Pic " + _picID + " saved");
        }
        else
        {
            Debug.Log("error, pic " + _picID + " not saved");
        }
    }

    [SerializeField] GameObject newRecordImageObject;

    private void LoadImageCallback(string error, Texture2D image)
    {
        newRecordImageObject.SetActive(true);
        Debug.Log("**************************************** Load Image Callback");

        if (!string.IsNullOrEmpty(error))
        {
            // TODO: There was an error, show it to users. 
            Debug.Log("Error on load image callback");
        }
        else
        {
           
            Debug.Log("recordID = " + _recordID);

            _currentTexture = image;
            Debug.Log("Current texture set: " + _recordID);
        }

        newRecordImageTexture2D.texture = image;

        Debug.Log("loadimagecallback");  
    }

    public void SaveRecordButton()
    {
        Debug.Log("**********************************************Save Record Button");

        _currentWeight = float.Parse(weightInputField.text);
        _currentWaist = float.Parse(waistInputField.text);
        _recordID = DateTime.Now.ToString();
        _picID = _recordID.Replace(":", "").Replace("/", "").Replace(" ", "");

        if (_dataService.AddUserLogEntry(DateTime.Now, _currentWeight, _currentWaist) == 1)
        {
            Debug.Log("*********************************************Userlog Added");
        }
        else
        {
            Debug.Log("**********************************************Error, Userlog NOT Added");
        }

        SaveTexture();
        newRecordImageObject.SetActive(false);
        _currentTexture = null;
        _recordID = "";
        _picID = "";
    }

    public void CancelNewRecord()
    {
        Debug.Log("**************************************** Cancel");

        _currentTexture = null;
        _recordID = "";
        _picID = "";
    }

    public void ToggleWeightWaistCharts()
    {
        if (waistChart.activeSelf)
        {
            waistChart.SetActive(false);
            weightChart.SetActive(true);
        }
        else
        {
            waistChart.SetActive(true);
            weightChart.SetActive(false);
        }
    }
}
