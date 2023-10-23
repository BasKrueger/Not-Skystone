using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BoardModel
{
    private SkystoneModel[,] tiles = new SkystoneModel[3,3];

    public void SetUp()
    {
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                tiles[x, y] = null;
            }
        }
    }

    public BoardState GetState()
    {
        SkystoneState[,] states = new SkystoneState[3,3];
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (tiles[x,y] == null)
                {
                    states[x, y] = null;
                    continue;
                }
                states[x, y] = tiles[x, y].GetState();
            }
        }

        return new BoardState()
        {
            tiles = states
        };
    }

    public int TryToPlace(SkystoneModel toPlace, int x, int y)
    {
        int score = 0;
        if (x < 0) return score;
        if (y < 0) return score;
        if(x >= 3) return score;
        if (y >= 3) return score;

        if (tiles[x,y] != null)
        {
            return score;
        }

        tiles[x, y] = toPlace;
        score++;

        score += TryToOvertakeNeighbours(toPlace, x, y);
        return score;
    }

    public bool IsFull()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (tiles[x,y] == null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private int TryToOvertakeNeighbours(SkystoneModel lastPlaced, int x, int y)
    {
        int score = 0;
        SkystoneModel target;
        if(x > 0)
        {
            target = tiles[x - 1, y];
            if (target != null && lastPlaced.westSpikes > target.eastSpikes && lastPlaced.TryToOvertake(target))
            {
                score++;
            }
        }

        if(x < 2)
        {
            target = tiles[x + 1, y];
            if(target != null && lastPlaced.eastSpikes > target.westSpikes && lastPlaced.TryToOvertake(target))
            {
                score++;
            }
        }

        if (y > 0)
        {
            target = tiles[x, y - 1];
            if (target != null && lastPlaced.northSpikes > target.southSpikes && lastPlaced.TryToOvertake(target))
            {
                score++;
            }
        }

        if (y < 2)
        {
            target = tiles[x, y +1];

            if (target != null && lastPlaced.southSpikes > target.northSpikes && lastPlaced.TryToOvertake(target))
            {
                score++;
            }
        }

        return score;
    }
}
