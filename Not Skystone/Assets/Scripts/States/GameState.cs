public class GameState 
{
    public PlayerState[] players;
    public BoardState board;
    public int currentPlayerIndex;
    public int winningPlayerIndex = -1;
    public int iteration = -1;
}
