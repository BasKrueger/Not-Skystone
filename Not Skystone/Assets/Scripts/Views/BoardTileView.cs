using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardTileView : MonoBehaviour, IGameView
{
    [field: SerializeField]
    public int x { get; private set; } = -1;
    [field:SerializeField]
    public int y { get; private set; } = -1;

    [SerializeField]
    private SkyStone SkyStoneTemplate;
    private SkyStone activeSkyStone;
    private MeshRenderer mesh;
    private SkystoneState previousStoneState = null;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void OnGameStateUpdate(GameState state)
    {
        if (x == -1 || y == -1) return;

        SkystoneState stoneState = state.board.tiles[x, y];
        if (stoneState == null)
        {
            if(activeSkyStone != null) Destroy(activeSkyStone.gameObject);
            mesh.material.color = GetCurrentColor(stoneState);
            previousStoneState = null;
            return;
        }
        
        if(activeSkyStone == null)
        {
            TryToSpawnStone(state.board.tiles[x, y]);
            mesh.material.color = GetCurrentColor(stoneState);
        }

        if (previousStoneState != null && previousStoneState.OwnerIndex != stoneState.OwnerIndex)
        {
            StartCoroutine(delay());
        }

        previousStoneState = stoneState;

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.75f);
            activeSkyStone?.PlaySwitchAnimation();
            yield return new WaitForSeconds(0.75f);
            mesh.material.color = GetCurrentColor(stoneState);
        }
    }

    private Color GetCurrentColor(SkystoneState state)
    {
        if(state == null)
        {
            return Color.gray;
        }

        return state.OwnerIndex == 0 ? Color.blue : Color.red;
    }

    private bool TryToSpawnStone(SkystoneState state)
    {
        if(state == null || activeSkyStone != null)
        {
            return false;
        }

        activeSkyStone = Instantiate(SkyStoneTemplate);
        activeSkyStone.SetSpikes(state.northSpikes, state.eastSpikes, state.southSpikes, state.westSpikes);
        activeSkyStone.PlayEntryAnimation();

        activeSkyStone.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        activeSkyStone.targetPosition = transform.position + new Vector3(0, 0.5f, 0);
        activeSkyStone.transform.SetParent(transform);

        activeSkyStone.GetComponentInChildren<RandomMovement>().enabled = false;
        return true;
    }

    
}
