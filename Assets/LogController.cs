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


    [SerializeField] GameObject ExerciseDataPanelSmall;
    [SerializeField] GameObject ExerciseDataPanelLarge;
    [SerializeField] GameObject UserDataPanelSmall;
    [SerializeField] GameObject UserDataPanelLarge;
  

    private float currentWeight;
    private float currentWaist;
    private Texture2D CurrentTexture;
    private string picID;
    private GraphController graphcontroller;
    private DataService dataService;
    private string recordID;




    // Start is called before the first frame update
    void Start()
    {
        CurrentTexture = new Texture2D(150, 150);
        dataService = StartupScript.ds;

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
                if (CurrentTexture != null)
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

        if (FileAccessUtil.SavePic(CurrentTexture, picID))
        {
            Debug.Log("Pic " + picID + " saved");
        }
        else
        {
            Debug.Log("error, pic " + picID + " not saved");
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
           
            Debug.Log("recordID = " + recordID);

            CurrentTexture = image;
            Debug.Log("Current texture set: " + recordID);
        }

  


        newRecordImageTexture2D.texture = image;

        Debug.Log("loadimagecallback");
  


    }

    public void SaveRecordButton()
    {
        Debug.Log("**********************************************Save Record Button");

        currentWeight = float.Parse(weightInputField.text);
        currentWaist = float.Parse(waistInputField.text);
        recordID = DateTime.Now.ToString();
        picID = recordID.Replace(":", "").Replace("/", "").Replace(" ", "");

        if (dataService.AddUserLogEntry(DateTime.Now, currentWeight, currentWaist) == 1)
        {
            Debug.Log("*********************************************Userlog Added");
        }
        else
        {
            Debug.Log("**********************************************Error, Userlog NOT Added");
        }

        SaveTexture();
        newRecordImageObject.SetActive(false);
        CurrentTexture = null;
        recordID = "";
        picID = "";


    }

    public void CancelNewRecord()
    {
        Debug.Log("**************************************** Cancel");

        CurrentTexture = null;
        recordID = "";
        picID = "";
    }


}
