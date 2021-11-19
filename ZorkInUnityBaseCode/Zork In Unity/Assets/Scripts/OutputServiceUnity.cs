using UnityEngine;
using Zork.Common;
using TMPro;

public class OutputServiceUnity : MonoBehaviour, IOutputService
{
    [SerializeField] private TextMeshProUGUI OutpufField;

    public void Write(object value)
    {
        throw new System.NotImplementedException();
    }

    public void WriteLine(object value)
    {
        throw new System.NotImplementedException();
    }
}
