using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadPreset : MonoBehaviour
{
    private CircuitController circuitController;
    [SerializeField] TextMeshProUGUI presetNameText;

    // Start is called before the first frame update
    void Start()
    {
        circuitController = FindObjectOfType<CircuitController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadPresetButton()
    {
        circuitController.LoadPresetButton(presetNameText.text);            
    }

}
