using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class HandView : MonoBehaviour, IGameView
{
    private int playerIndex = -1;

    [SerializeField]
    private SkyStone stoneTemplate;
    private List<SkyStone> activeSkyStones = new List<SkyStone>();

    public void SetUp(int index)
    {
        playerIndex = index;
    }

    public void OnGameStateUpdate(GameState state)
    {
        if (playerIndex == -1) return;

        UpdateActiveSkystones(state.players[playerIndex].handStones);

        if (state.currentPlayerIndex == playerIndex)
        {
            ActivateStoneMovement();
        }
        else
        {
            DeActivateStoneMovement();
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeSkyStones.Count; i++)
        {
            float percent = i / (float)activeSkyStones.Count;
            activeSkyStones[i].targetPosition = transform.position + new Vector3(0, 0, percent * 10f);
        }

        HighlightHoveredStone();
    }

    private void UpdateActiveSkystones(SkystoneState[] states)
    {
        for(int i = 0;i < states.Length; i++)
        {
            SkystoneState currentState = states[i];
            if(currentState == null)
            {
                continue;
            }

            SkyStone currentStone = null;
            if (i > activeSkyStones.Count - 1)
            {
                currentStone = SpawnStone(currentState, i);
            }
            else
            {
                currentStone = ReplaceStoneIfInvalid(activeSkyStones[i], currentState, i);
            }
        }
        RemoveExcessStones();

        #region support functions
        SkyStone SpawnStone(SkystoneState stoneState, int handIndex)
        {
            SkyStone instance = Instantiate(stoneTemplate);

            instance.transform.position = transform.position;
            instance.SetSpikes(stoneState.northSpikes, stoneState.eastSpikes, stoneState.southSpikes, stoneState.westSpikes);
            instance.handIndex = handIndex;
            instance.ownerIndex = playerIndex;

            instance.transform.SetParent(transform);
            activeSkyStones.Add(instance);

            return instance;
        }

        void RemoveExcessStones()
        {
            for (int i = activeSkyStones.Count - 1; i > states.Length - 1; i--)
            {
                SkyStone currentStone = activeSkyStones[i];
                activeSkyStones.Remove(currentStone);
                Destroy(currentStone.gameObject);
            }
        }

        SkyStone ReplaceStoneIfInvalid(SkyStone currentStone, SkystoneState stateToMatch, int index)
        {
            currentStone.handIndex = index;
            if (
                currentStone.northSpikes.value != stateToMatch.northSpikes ||
                currentStone.eastSpikes.value != stateToMatch.eastSpikes ||
                currentStone.southSpikes.value != stateToMatch.southSpikes ||
                currentStone.westSpikes.value != stateToMatch.westSpikes
                )
            {
                activeSkyStones.Remove(currentStone);
                Destroy(currentStone.gameObject);

                currentStone = SpawnStone(stateToMatch, index);
            }

            return currentStone;
        }
        #endregion
    }

    private void HighlightHoveredStone()
    {
        foreach(SkyStone stone in activeSkyStones)
        {
            stone.Highlight(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, Mathf.Infinity))
        {
            SkyStone stone = hit.transform.GetComponent<SkyStone>();
            if (stone != null && stone.CanGrab(playerIndex))
            {
                stone.Highlight(true);
                return;
            }
        }
    }

    private void ActivateStoneMovement()
    {
        foreach(SkyStone stone in activeSkyStones)
        {
            stone.Animate(true);
        }
    }

    private void DeActivateStoneMovement()
    {
        foreach (SkyStone stone in activeSkyStones)
        {
            stone.Animate(false);
        }
    }
}
