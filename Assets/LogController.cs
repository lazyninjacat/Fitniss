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


    void Start()
    {
        _currentTexture = new Texture2D(150, 150);
        _dataService = StartupScript.ds;

        if (!RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
        }                       
    }

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
        EasyMobile.CameraType cameraType = EasyMobile.CameraType.Front;
        Media.Camera.TakePicture(cameraType, TakePictureCallback);
    }

    private void TakePictureCallback(string error, MediaResult result)
    {
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
        FileAccessUtil.SavePic(_currentTexture, _picID);
    }

    [SerializeField] GameObject newRecordImageObject;

    private void LoadImageCallback(string error, Texture2D image)
    {
        newRecordImageObject.SetActive(true);

        if (!string.IsNullOrEmpty(error))
        {
            // TODO: There was an error, show it to users. 
            Debug.Log("Error on load image callback");
        }
        else
        {        
            _currentTexture = image;
        }

        newRecordImageTexture2D.texture = image;
    }

    public void SaveRecordButton()
    {

        _currentWeight = float.Parse(weightInputField.text);
        _currentWaist = float.Parse(waistInputField.text);
        _recordID = DateTime.Now.ToString();
        _picID = _recordID.Replace(":", "").Replace("/", "").Replace(" ", "");

        _dataService.AddUserLogEntry(DateTime.Now, _currentWeight, _currentWaist);

        SaveTexture();
        newRecordImageObject.SetActive(false);
        _currentTexture = null;
        _recordID = "";
        _picID = "";
    }

    public void CancelNewRecord()
    {
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
