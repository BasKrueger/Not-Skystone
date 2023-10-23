using System.Collections.Generic;

public class PlayerModel
{
    private List<SkystoneModel> hand;
    public int score;

    public PlayerState GetState()
    {
        List<SkystoneState> stoneStates = new List<SkystoneState>();
        foreach (SkystoneModel stone in hand)
        {
            stoneStates.Add(stone.GetState());
        }
        return new PlayerState()
        {
            score = this.score,
            handStones = stoneStates.ToArray()
        };
    }

    public SkystoneModel GetStone(int stoneIndex)
    {
        if (stoneIndex > hand.Count - 1 && stoneIndex < 0)
        {
            return null;
        }

        return hand[stoneIndex];
    }

    public void Setup(int index)
    {
        hand = SkystoneModelFactory.CreateRandomSkystones(5, index);
        score = 0;
    }

    public void RemoveStoneFromHand(int stoneIndex)
    {
        hand.RemoveAt(stoneIndex);
    }
}