using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour, IGameView
{
    private int playerIndex = -1;

    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private Image turnHighlight;

    public void SetUp(int index)
    {
        playerIndex = index;
    }

    public void OnGameStateUpdate(GameState state)
    {
        if (playerIndex == -1) return;

        score.text = state.players[playerIndex].score.ToString();
        turnHighlight.gameObject.SetActive(state.currentPlayerIndex == playerIndex);
    }
}
