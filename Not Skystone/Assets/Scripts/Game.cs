using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameModel model;

    private static Game instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            model = new GameModel();
            model.Reset();
            model.StateChanged += UpdateStates;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void ForceUpdateState()
    {
        UpdateStates(Game.model.GetState());
    }

    private static void UpdateStates(GameState state)
    {
        foreach(IGameView view in FindObjectsOfType<MonoBehaviour>(true).OfType<IGameView>().ToArray())
        {
            view.OnGameStateUpdate(state);
        }
    }
}
