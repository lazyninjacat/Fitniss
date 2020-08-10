using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentExerciseName;
    [SerializeField] TextMeshProUGUI CurrentExerciseAmount;
    [SerializeField] Progressor progressor;
    [SerializeField] TextMeshProUGUI confirmLoadPresetText;
    [SerializeField] TextMeshProUGUI currentPresetText;
    [SerializeField] RawImage newRecordImageTexture2D;
    [SerializeField] TMP_InputField weightInputField;
    [SerializeField] TMP_InputField waistInputField;
    [SerializeField] Button saveButton;

    private float currentWeight;
    private float currentWaist;

    public Texture2D CurrentTexture { get; set; }

    public string PicID { get; set; }


    private GraphController graphcontroller;
    private int circuitOrder;

    private DataService dataService;

    private int circuitCount;

    private bool circuitComplete;

    private float circuitProgress;

    private string selectedCircuitPreset;
    
    // Start is called before the first frame update
    void Start()
    {
        circuitComplete = false;
        circuitProgress = 0;
 

        if (selectedCircuitPreset == null || selectedCircuitPreset == "")
        {
            currentPresetText.text = "Current Preset: Default";

        }

        graphcontroller = FindObjectOfType<GraphController>();
        dataService = StartupScript.ds;
        circuitOrder = 1;
        circuitCount = dataService.GetCircuitCount();
        CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
   
    }

    public void StartButton()
    {
        circuitComplete = false;
        circuitOrder = 1;
        CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
        CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
    }

    public void ConfirmLoadPresetButton()
    {
        currentPresetText.text = "Current Preset: " + selectedCircuitPreset;
        Debug.Log("Loading preset " + selectedCircuitPreset);
    }

    public void LoadPresetButton(string presetName)
    {
        selectedCircuitPreset = presetName;
        confirmLoadPresetText.text = selectedCircuitPreset;
        Debug.Log("Preset staged: " + selectedCircuitPreset);
    }

    public void TakePicture()
    {
        Debug.Log("Take Picture");
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

    private string recordID;

    private void SaveTexture() 
    { 
        if (FileAccessUtil.SavePic(CurrentTexture, recordID))
        {
            Debug.Log("Pic " + recordID + " saved");
        }
        else
        {
            Debug.Log("error, pic " + recordID + " not saved");
        }
    }

    [SerializeField] GameObject newRecordImageObject;

    private void LoadImageCallback(string error, Texture2D image)
    {
        if (!string.IsNullOrEmpty(error))
        {
            // TODO: There was an error, show it to users. 
            Debug.Log("Error on load image callback");
        }
        else
        {
            CurrentTexture = image;
            Debug.Log("Current texture set");
        }
        newRecordImageObject.SetActive(true);

        newRecordImageTexture2D.texture = CurrentTexture;
            
        Debug.Log("loadimagecallback");
    }

    public void SaveRecordButton()
    {
        recordID = DateTime.Now.ToString();
        currentWeight = float.Parse(weightInputField.text);
        currentWaist = float.Parse(waistInputField.text);
        
        
        if (dataService.AddUserLogEntry(recordID, currentWeight, currentWaist) == 1)
        {
            Debug.Log("Userlog Added");
        }
        else
        {
            Debug.Log("Error, Userlog NOT Added");
        }

        SaveTexture();
        newRecordImageObject.SetActive(false);


    }

    public void DoneButton()
    {
        Debug.Log("Done button");
        if (!circuitComplete)
        {
            progressor.SetValue((float)circuitOrder / (float)circuitCount);
            Debug.Log((((float)circuitOrder) / (float)circuitCount));

            circuitOrder++;

            if (circuitOrder <= circuitCount)
            {
                CurrentExerciseName.text = dataService.GetExerciseName(circuitOrder);
                CurrentExerciseAmount.text = dataService.GetExerciseAmount(circuitOrder);
            }
            else
            {
                CurrentExerciseName.text = "Complete!!!";
                CurrentExerciseAmount.text = "Tap Done to return to start menu";
                circuitComplete = true;
            }
        }
        else
        {
            graphcontroller.GoToNodeByName("StartMenu");
        }     
    }

    public void CancelNewRecord()
    {
        CurrentTexture = null;
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
}
