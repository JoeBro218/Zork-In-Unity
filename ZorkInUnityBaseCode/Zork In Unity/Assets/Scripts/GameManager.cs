using UnityEngine;
using Newtonsoft.Json;
using Zork;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputServiceUnity InputService;

    [SerializeField] private OutputServiceUnity OutputService;

    void Awake()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);

        game.Start(InputService, OutputService);
    }

}
