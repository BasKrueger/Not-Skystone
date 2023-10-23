public class GameModel
{
    public delegate void GameStateDelegate(GameState state);
    public GameStateDelegate StateChanged;
    private GameState latestState;

    private BoardModel board;
    private PlayerModel[] players;

    private int currentPlayerIndex = 0;

    public GameState GetState()
    {
        if (latestState == null)
        {
            UpdateGameState();
        }

        return latestState;
    }

    public void Reset() 
    {
        board = new BoardModel();
        players = new PlayerModel[2] { new PlayerModel(), new PlayerModel() };
        currentPlayerIndex = 0;

        for(int i = 0;i < 2;i++)
        {
            players[i].Setup(i);
        }

        UpdateGameState();
    }

    public void PlaceSkystone(int stoneIndex, int ownerIndex, int x, int y)
    {
        if(ownerIndex != this.currentPlayerIndex)
        {
            return;
        }

        SkystoneModel toPlace = players[currentPlayerIndex].GetStone(stoneIndex);
        if(toPlace == null)
        {
            return;
        }

        int scoreIncrease = board.TryToPlace(toPlace, x, y);
        if (scoreIncrease > 0)
        {
            players[currentPlayerIndex].score += scoreIncrease;
            players[currentPlayerIndex].RemoveStoneFromHand(stoneIndex);
            SwitchPlayers();
            players[currentPlayerIndex].score -= scoreIncrease - 1;
        }

        UpdateGameState();
    }

    private void SwitchPlayers()
    {
        currentPlayerIndex++;
        if(currentPlayerIndex > players.Length - 1)
        {
            currentPlayerIndex = 0;
        }
    }

    private void UpdateGameState()
    {
        int iteration = latestState != null ? latestState.iteration : 0;

        latestState = new GameState();

        latestState.players = new PlayerState[2] { players[0].GetState(), players[1].GetState() };
        latestState.board = board.GetState();
        latestState.currentPlayerIndex = this.currentPlayerIndex;
        latestState.iteration = iteration + 1;

        if (board.IsFull())
        {
            latestState.winningPlayerIndex = GetHighestScorePlayerIndex();
        }

        StateChanged?.Invoke(latestState);

        int GetHighestScorePlayerIndex()
        {
            int winningIndex = -1;
            int highestScore = -1;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].score > highestScore)
                {
                    highestScore = players[i].score;
                    winningIndex = i;
                }
            }

            return winningIndex;
        }
    }
}

