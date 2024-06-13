using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GuineaPig[] guineaPigs;
    public Button playButton;
    public Button repeatButton;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] leaderboardTexts;
    public GameObject winnerPanel;

    private bool raceStarted = false;

    void Start()
    {
        playButton.onClick.AddListener(StartRace);
        repeatButton.onClick.AddListener(ResetRace);
        winnerPanel.SetActive(false);
    }

    void Update()
    {
        if (raceStarted)
        {
            UpdateLeaderboard();
        }
    }

    public void StartRace()
    {
        raceStarted = true;
        playButton.interactable = false;
        DetermineWinner();
        foreach (var pig in guineaPigs)
        {
            pig.StartRunning();
        }
    }

    void DetermineWinner()
    {
        int winnerIndex = Random.Range(0, guineaPigs.Length); // Escoge un ganador aleatorio
        float maxSpeed = 10f; // Velocidad máxima para el ganador

        // Ajustar la velocidad del ganador al máximo
        guineaPigs[winnerIndex].SetSpeed(maxSpeed);

        // Restablecer velocidades aleatorias para los demás
        float minSpeed = 5f;
        float maxRandomSpeed = 8f; // Velocidad máxima aleatoria para los perdedores
        foreach (var pig in guineaPigs.Where((value, index) => index != winnerIndex))
        {
            float randomSpeed = Random.Range(minSpeed, maxRandomSpeed);
            pig.SetSpeed(randomSpeed);
        }

        Debug.Log("Winner is Guinea Pig: " + guineaPigs[winnerIndex].index); // Imprimir el ganador correcto
    }

    public void ResetRace()
    {
        raceStarted = false;
        playButton.interactable = true;
        foreach (var pig in guineaPigs)
        {
            pig.ResetPosition();
        }
        ResetUI();
    }

    public void FinishRace(GuineaPig pig)
    {
        if (raceStarted)
        {
            raceStarted = false;
            winnerText.text = "Winner is Guinea Pig " + pig.index;
            winnerPanel.SetActive(true);
            UpdateLeaderboard();
        }
    }

    void UpdateLeaderboard()
    {
        var sortedPigs = guineaPigs.OrderByDescending(p => p.transform.position.x).ToArray();
        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            if (i < sortedPigs.Length)
            {
                leaderboardTexts[i].text = sortedPigs[i].name;
            }
            else
            {
                leaderboardTexts[i].text = "";
            }
        }
    }

    void ResetUI()
    {
        winnerText.text = "";
        foreach (var text in leaderboardTexts)
        {
            text.text = "";
        }
        winnerPanel.SetActive(false);
    }
}
