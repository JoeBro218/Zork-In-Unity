using UnityEngine;
using Newtonsoft.Json;
using Zork;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Script Fields")]
    [SerializeField] private InputServiceUnity InputService;
    [SerializeField] private OutputServiceUnity OutputService;

    [Header("Text Boxes")]
    [SerializeField] private TextMeshProUGUI LocationText;
    [SerializeField] private TextMeshProUGUI MovesText;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [Header("Audio")]
    [SerializeField] private AudioSource Idle;
    [SerializeField] private AudioSource WalkingGravel;
    [SerializeField] private AudioSource WalkingTallGrass;
    [SerializeField] private AudioSource WalkingRegularGrass;

    private void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>("Zork");
        Game game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);

        game.GameStop += game_GameStopped;
        game.Start(InputService, OutputService);

        game.Player.LocationChanged += (sender, Location) => LocationText.text = $"Location: {Location.ToString()}";
        game.Player.LocationChanged += (sender, Location) => MovmentSound();
        LocationText.text = $"Location: {game.StartingLocation.ToString()}";

        game.Player.ScoreChanged += (sender, Score) => ScoreText.text = $"Score: {Score.ToString()}"; 
        ScoreText.text = $"Score: {game.Player.Score.ToString()}";

        game.Player.MovesChanged += (sender, Moves) => MovesText.text = $"Moves: {Moves.ToString()}";
        MovesText.text = $"Moves: {game.Player.Moves.ToString()}";

        Game.Look(game);

        Idle.Play();
    }

    private void MovmentSound()
    {
        System.Random RanNum = new System.Random();
        int Num = RanNum.Next(1, 9);
        if (Num == 1 || Num == 2 || Num == 3)
        {
            WalkingGravel.Play();
        }
        else if (Num == 4 || Num == 5 || Num == 6)
        {
            WalkingTallGrass.Play();
        }
        else if (Num == 7 || Num == 8 || Num == 9)
        {
            WalkingRegularGrass.Play();
        }
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
