using UnityEngine;
using Newtonsoft.Json;
using Zork;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputServiceUnity InputService;

    [SerializeField] private OutputServiceUnity OutputService;

    void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
    }

}
