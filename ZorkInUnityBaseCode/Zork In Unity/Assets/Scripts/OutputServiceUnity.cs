using UnityEngine;
using Zork.Common;
using TMPro;
using System.Collections.Generic;

public class OutputServiceUnity : MonoBehaviour, IOutputService
{
    [SerializeField] private int MaxEntries = 60;
    [SerializeField] private Transform OutputField;
    [SerializeField] private TextMeshProUGUI TextAssetPrefab;

    public void Write(object value)
    {
        throw new System.NotImplementedException();
    }

    public void WriteLine(object value)
    {
        throw new System.NotImplementedException();
    }
}
