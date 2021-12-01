using UnityEngine;
using Newtonsoft.Json;
using Zork;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputServiceUnity InputService;

    [SerializeField] private OutputServiceUnity OutputService;

    [SerializeField] private TextMeshProUGUI LocationText;

    [SerializeField] private TextMeshProUGUI MovesText;

    [SerializeField] private TextMeshProUGUI ScoreText;

    //Beep
    private void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);

        game.GameStop += game_GameStopped;
        game.Start(InputService, OutputService);

        game.Player.LocationChanged += (sender, Location) => LocationText.text = $"Location: {Location.ToString()}";
        LocationText.text = $"Location: {game.StartingLocation.ToString()}";

        game.Player.ScoreChanged += (sender, Score) => ScoreText.text = $"Score: {ScoreText.ToString()}";
        ScoreText.text = $"Score: {game.Player.Score.ToString()}";

        game.Player.MovesChanged += (sender, Moves) => MovesText.text = $"Moves: {MovesText.ToString()}";
        MovesText.text = $"Moves: {game.Player.Moves.ToString()}";

        Game.Look(game);

    }

    private void game_GameStopped(object sender, EventArgs e)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
