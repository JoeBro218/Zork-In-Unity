using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class InputServiceUnity : MonoBehaviour, IInputService
{
    [SerializeField] private TMP_InputField InputField;

    public event EventHandler<string> InputReceived;

    public void ProcessInput()
    {
        string inputString = Console.ReadLine().Trim().ToUpper();
        InputReceived?.Invoke(this, inputString);
    }
}