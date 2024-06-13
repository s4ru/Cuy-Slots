using UnityEngine;
using TMPro; // TextMeshPro

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] leaderboardTexts; // Array de textos para la leaderboard
    public GameObject winnerPanel;

    // Método para actualizar el texto del ganador
    public void UpdateWinnerText(int winnerIndex)
    {
        winnerText.text = "Winner is Guinea Pig " + winnerIndex;
        winnerPanel.SetActive(true);
    }

    // Método para actualizar el leaderboard
    public void UpdateLeaderboard(string[] leaderboard)
    {
        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            if (i < leaderboard.Length)
            {
                leaderboardTexts[i].text = leaderboard[i];
            }
            else
            {
                leaderboardTexts[i].text = "";
            }
        }
    }

    // Método para ocultar el panel del ganador
    public void HideWinnerPanel()
    {
        winnerPanel.SetActive(false);
    }

    // Método para resetear la UI
    public void ResetUI()
    {
        winnerText.text = "";
        foreach (var text in leaderboardTexts)
        {
            text.text = "";
        }
        HideWinnerPanel();
    }
}
