using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class InputServiceUnity : MonoBehaviour, IInputService
{
    [SerializeField] private TMP_InputField InputField;

    public event EventHandler<string> InputReceived;

    //eep
    private void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            if(string.IsNullOrWhiteSpace(InputField.text) == false)
            {
                string InputString = InputField.text.Trim().ToUpper();
                InputReceived?.Invoke(this, InputString);
            }

            InputField.text = string.Empty;
        }
    }
}