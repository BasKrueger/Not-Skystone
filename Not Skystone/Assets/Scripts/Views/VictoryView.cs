using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryView : MonoBehaviour, IGameView
{
    [SerializeField]
    private TextMeshProUGUI victoryText;

    public void OnGameStateUpdate(GameState state)
    {
        this.gameObject.SetActive(state.winningPlayerIndex != -1);
        victoryText.text = "Player " + (state.winningPlayerIndex + 1).ToString() + " Won";
    }

    public void OnResetPressed()
    {
        Game.model.Reset();
    }
}
