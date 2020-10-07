using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadPreset : MonoBehaviour
{
    private CircuitController circuitController;
    [SerializeField] TextMeshProUGUI presetNameText;
    
    void Start()
    {
        circuitController = FindObjectOfType<CircuitController>();
    }

    public void LoadPresetButton()
    {
        circuitController.LoadPresetButton(presetNameText.text);            
    }

}
