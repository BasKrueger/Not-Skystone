using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int index = -1;

    private HandController handcontroller;
    private HandView handView;
    private ScoreView scoreView;

    private void Awake()
    {
        handcontroller = GetComponentInChildren<HandController>();
        handView = GetComponentInChildren<HandView>();
        scoreView = GetComponentInChildren<ScoreView>();

        if (index == -1)
        {
            Debug.LogError("Error: Playerindex not set");
        }
    }

    private void Start()
    {
        handcontroller.SetUp(index);
        handView.SetUp(index);
        scoreView.SetUp(index);

        Game.ForceUpdateState();
    }
}
