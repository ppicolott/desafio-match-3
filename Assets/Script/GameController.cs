using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController
{
    private List<List<Tile>> _boardTiles;
    private List<int> _tilesTypes;
    private int _tileCount;

    private static int colorNumber;
    private static int specialColorNumber;

    private static int yellowNumber = 0;
    private static int blueNumber = 1;
    private static int greenNumber = 2;
    private static int orangeNumber = 3;
    private static int pinkNumber = 4;
    private static int purpleNumber = 5;
    private static int redNumber = 6;
            
    private static int specialYellowNumber = 7;
    private static int specialBlueNumber = 8;
    private static int specialGreenNumber = 9;
    private static int specialOrangeNumber = 10;
    private static int specialPinkNumber = 11;
    private static int specialPurpleNumber = 12;
    private static int specialRedNumber = 13;

    private static int lineCleaner = 14;
    private static int bomb = 15;

    public List<List<Tile>> StartGame(int boardWidth, int boardHeight)
    {
        List<bool> designedColors = new List<bool>()
        {
            ScoreManager.instance.levelRules.yellow,
            ScoreManager.instance.levelRules.blue,
            ScoreManager.instance.levelRules.green,
            ScoreManager.instance.levelRules.orange,
            ScoreManager.instance.levelRules.pink,
            ScoreManager.instance.levelRules.purple,
            ScoreManager.instance.levelRules.red,
            ScoreManager.instance.levelRules.specialYellow,
            ScoreManager.instance.levelRules.specialBlue,
            ScoreManager.instance.levelRules.specialGreen,
            ScoreManager.instance.levelRules.specialOrange,
            ScoreManager.instance.levelRules.specialPink,
            ScoreManager.instance.levelRules.specialPurple,
            ScoreManager.instance.levelRules.specialRed,
            ScoreManager.instance.levelRules.lineCleaner,
            ScoreManager.instance.levelRules.bomb,
        };

        List<int> levelColors = new List<int>();
        for (int i = 0; i < designedColors.Count; i++)
        {
            if (designedColors[i])
            {
                levelColors.Add(i);
            }
        }
        // foreach (int item in levelColors)
        // {
        //     Debug.Log(item);
        // }

        // Original:
        // _tilesTypes = new List<int> { 0, 1, 2, 3, 9, 10, 14 };
        _tilesTypes = levelColors;
        _boardTiles = CreateBoard(boardWidth, boardHeight, _tilesTypes);
        return _boardTiles;
    }

    public bool IsValidMovement(int fromX, int fromY, int toX, int toY)
    {
        List<List<Tile>> newBoard = CopyBoard(_boardTiles);

        Tile switchedTile = newBoard[fromY][fromX];
        newBoard[fromY][fromX] = newBoard[toY][toX];
        newBoard[toY][toX] = switchedTile;

        for (int y = 0; y < newBoard.Count; y++)
        {
            for (int x = 0; x < newBoard[y].Count; x++)
            {
                switch (newBoard[y][x].type)
                {
                    case 0:
                        colorNumber = yellowNumber;
                        specialColorNumber = specialYellowNumber;
                        break;
                    case 1:
                        colorNumber = blueNumber;
                        specialColorNumber = specialBlueNumber;
                        break;
                    case 2:
                        colorNumber = greenNumber;
                        specialColorNumber = specialGreenNumber;
                        break;
                    case 3:
                        colorNumber = orangeNumber;
                        specialColorNumber = specialOrangeNumber;
                        break;
                    case 4:
                        colorNumber = pinkNumber;
                        specialColorNumber = specialPinkNumber;
                        break;
                    case 5:
                        colorNumber = purpleNumber;
                        specialColorNumber = specialPurpleNumber;
                        break;
                    case 6:
                        colorNumber = redNumber;
                        specialColorNumber = specialRedNumber;
                        break;
                    case 7:
                        colorNumber = specialYellowNumber;
                        specialColorNumber = specialYellowNumber;
                        break;
                    case 8:
                        colorNumber = specialBlueNumber;
                        specialColorNumber = specialBlueNumber;
                        break;
                    case 9:
                        colorNumber = specialGreenNumber;
                        specialColorNumber = specialGreenNumber;
                        break;
                    case 10:
                        colorNumber = specialOrangeNumber;
                        specialColorNumber = specialOrangeNumber;
                        break;
                    case 11:
                        colorNumber = specialPinkNumber;
                        specialColorNumber = specialPinkNumber;
                        break;
                    case 12:
                        colorNumber = specialPurpleNumber;
                        specialColorNumber = specialPurpleNumber;
                        break;
                    case 13:
                        colorNumber = specialRedNumber;
                        specialColorNumber = specialRedNumber;
                        break;
                    case 14:
                        colorNumber = 14;
                        specialColorNumber = -1;
                        break;
                    case 15:
                        colorNumber = 15;
                        specialColorNumber = -1;
                        break;
                }

                // X Axis:

                if (x > 2)
                {
                    if (newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 2].type == 14
                        && newBoard[y][x].type == newBoard[y][x - 3].type)
                    {
                        return true;
                    }
                    else if (newBoard[y][x].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 1].type == 14
                        && newBoard[y][x].type == newBoard[y][x - 3].type)
                    {
                        return true;
                    }

                    if (newBoard[y][x].type == specialColorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == specialColorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == specialColorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == specialColorNumber)
                    {
                        return true;
                    }
                }

                if (x > 1
                        && newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type)
                {
                    return true;
                }


                // Y Axis:


                if (y > 2)
                {
                    if (newBoard[y][x].type == newBoard[y - 1][x].type
                    && newBoard[y - 2][x].type == 14
                    && newBoard[y][x].type == newBoard[y - 3][x].type)
                    {
                        return true;
                    }
                    else if (newBoard[y][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 1][x].type == 14
                        && newBoard[y][x].type == newBoard[y - 3][x].type)
                    {
                        return true;
                    }

                    if (newBoard[y][x].type == specialColorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == specialColorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == specialColorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == specialColorNumber)
                    {
                        return true;
                    }
                }

                if (y > 1
                    && newBoard[y][x].type == newBoard[y - 1][x].type
                    && newBoard[y - 1][x].type == newBoard[y - 2][x].type)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY)
    {
        List<List<Tile>> newBoard = CopyBoard(_boardTiles);

        Tile switchedTile = newBoard[fromY][fromX];
        newBoard[fromY][fromX] = newBoard[toY][toX];
        newBoard[toY][toX] = switchedTile;

        List<BoardSequence> boardSequences = new List<BoardSequence>();
        List<List<bool>> matchedTiles;
        while (HasMatch(matchedTiles = FindMatches(newBoard)))
        {
            //Cleaning the matched tiles
            List<Vector2Int> matchedPosition = new List<Vector2Int>();
            for (int y = 0; y < newBoard.Count; y++)
            {
                for (int x = 0; x < newBoard[y].Count; x++)
                {
                    if (matchedTiles[y][x])
                    {
                        matchedPosition.Add(new Vector2Int(x, y));
                        newBoard[y][x] = new Tile { id = -1, type = -1 };
                    }
                }
            }

            // Dropping the tiles
            Dictionary<int, MovedTileInfo> movedTiles = new Dictionary<int, MovedTileInfo>();
            List<MovedTileInfo> movedTilesList = new List<MovedTileInfo>();
            for (int i = 0; i < matchedPosition.Count; i++)
            {
                int x = matchedPosition[i].x;
                int y = matchedPosition[i].y;
                if (y > 0)
                {
                    for (int j = y; j > 0; j--)
                    {
                        Tile movedTile = newBoard[j - 1][x];
                        newBoard[j][x] = movedTile;
                        if (movedTile.type > -1)
                        {
                            if (movedTiles.ContainsKey(movedTile.id))
                            {
                                movedTiles[movedTile.id].to = new Vector2Int(x, j);
                            }
                            else
                            {
                                MovedTileInfo movedTileInfo = new MovedTileInfo
                                {
                                    from = new Vector2Int(x, j - 1),
                                    to = new Vector2Int(x, j)
                                };
                                movedTiles.Add(movedTile.id, movedTileInfo);
                                movedTilesList.Add(movedTileInfo);
                            }
                        }
                    }

                    newBoard[0][x] = new Tile
                    {
                        id = -1,
                        type = -1
                    };
                }
            }

            // Filling the board
            List<AddedTileInfo> addedTiles = new List<AddedTileInfo>();
            for (int y = newBoard.Count - 1; y > -1; y--)
            {
                for (int x = newBoard[y].Count - 1; x > -1; x--)
                {
                    if (newBoard[y][x].type == -1)
                    {
                        // Original:
                        // int tileType = Random.Range(0, _tilesTypes.Count);
                        int _tileType = 0;
                        float _rand = Random.value;
                        if (_rand <= ScoreManager.instance.levelRules.specialColorsProbability)
                            _tileType = Random.Range(4, _tilesTypes.Count);
                        else if (_rand <= ScoreManager.instance.levelRules.colorsProbability)
                            _tileType = Random.Range(0, _tilesTypes.Count - 4);

                        Tile tile = newBoard[y][x];
                        tile.id = _tileCount++;
                        // Original:
                        //tile.type = _tilesTypes[tileType];
                        tile.type = _tilesTypes[_tileType];
                        addedTiles.Add(new AddedTileInfo
                        {
                            position = new Vector2Int(x, y),
                            type = tile.type
                        });
                    }
                }
            }

            BoardSequence sequence = new BoardSequence
            {
                matchedPosition = matchedPosition,
                movedTiles = movedTilesList,
                addedTiles = addedTiles
            };
            boardSequences.Add(sequence);
        }

        _boardTiles = newBoard;
        return boardSequences;
        //return _boardTiles;
    }

    private static bool HasMatch(List<List<bool>> list)
    {
        for (int y = 0; y < list.Count; y++)
            for (int x = 0; x < list[y].Count; x++)
                if (list[y][x])
                    return true;
        return false;
    }

    private static List<List<bool>> FindMatches(List<List<Tile>> newBoard)
    {
        List<List<bool>> matchedTiles = new List<List<bool>>();
        for (int y = 0; y < newBoard.Count; y++)
        {
            matchedTiles.Add(new List<bool>(newBoard[y].Count));
            for (int x = 0; x < newBoard.Count; x++)
            {
                matchedTiles[y].Add(false);
            }
        }

        for (int y = 0; y < newBoard.Count; y++)
        {
            for (int x = 0; x < newBoard[y].Count; x++)
            {
                switch (newBoard[y][x].type)
                {
                    case 0:
                        colorNumber = yellowNumber;
                        specialColorNumber = specialYellowNumber;
                        break;
                    case 1:
                        colorNumber = blueNumber;
                        specialColorNumber = specialBlueNumber;
                        break;
                    case 2:
                        colorNumber = greenNumber;
                        specialColorNumber = specialGreenNumber;
                        break;
                    case 3:
                        colorNumber = orangeNumber;
                        specialColorNumber = specialOrangeNumber;
                        break;
                    case 4:
                        colorNumber = pinkNumber;
                        specialColorNumber = specialPinkNumber;
                        break;
                    case 5:
                        colorNumber = purpleNumber;
                        specialColorNumber = specialPurpleNumber;
                        break;
                    case 6:
                        colorNumber = redNumber;
                        specialColorNumber = specialRedNumber;
                        break;
                    case 7:
                        colorNumber = specialYellowNumber;
                        specialColorNumber = specialYellowNumber;
                        break;
                    case 8:
                        colorNumber = specialBlueNumber;
                        specialColorNumber = specialBlueNumber;
                        break;
                    case 9:
                        colorNumber = specialGreenNumber;
                        specialColorNumber = specialGreenNumber;
                        break;
                    case 10:
                        colorNumber = specialOrangeNumber;
                        specialColorNumber = specialOrangeNumber;
                        break;
                    case 11:
                        colorNumber = specialPinkNumber;
                        specialColorNumber = specialPinkNumber;
                        break;
                    case 12:
                        colorNumber = specialPurpleNumber;
                        specialColorNumber = specialPurpleNumber;
                        break;
                    case 13:
                        colorNumber = specialRedNumber;
                        specialColorNumber = specialRedNumber;
                        break;
                    case 14:
                        colorNumber = 14;
                        specialColorNumber = -1;
                        break;
                    case 15:
                        colorNumber = 15;
                        specialColorNumber = -1;
                        break;
                }

                // X Axis:

                if (x > 2)
                {
                    if (newBoard[y][x].type == 14
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 2].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 1].type == 14
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 2].type == 14
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 3].type == 14)
                    {
                        for (int t = 0; t < newBoard.Count; t++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                matchedTiles[y][i] = true;
                            }
                        }
                    }

                    if (newBoard[y][x].type == specialColorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == specialColorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == specialColorNumber
                        && newBoard[y][x - 3].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y][x - 1].type == colorNumber
                        && newBoard[y][x - 2].type == colorNumber
                        && newBoard[y][x - 3].type == specialColorNumber)
                    {
                        for (int j = 0; j < newBoard.Count; j++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                if (newBoard[j][i].type == colorNumber || newBoard[j][i].type == specialColorNumber)
                                {
                                    matchedTiles[j][i] = true;
                                }
                            }
                        }
                    }
                }

                if (x > 1
                        && newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type)
                {
                    matchedTiles[y][x] = true;
                    matchedTiles[y][x - 1] = true;
                    matchedTiles[y][x - 2] = true;
                }

                // Y Axis:

                if (y > 2)
                {
                    if (newBoard[y][x].type == 14
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 2][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 1][x].type == 14
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 2][x].type == 14
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 3][x].type == 14)
                    {
                        for (int t = 0; t < newBoard.Count; t++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                matchedTiles[i][x] = true;
                            }
                        }
                    }

                    if (newBoard[y][x].type == specialColorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == specialColorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == specialColorNumber
                        && newBoard[y - 3][x].type == colorNumber
                        ||
                        newBoard[y][x].type == colorNumber
                        && newBoard[y - 1][x].type == colorNumber
                        && newBoard[y - 2][x].type == colorNumber
                        && newBoard[y - 3][x].type == specialColorNumber)
                    {
                        for (int j = 0; j < newBoard.Count; j++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                if (newBoard[j][i].type == colorNumber || newBoard[j][i].type == specialColorNumber)
                                {
                                    matchedTiles[j][i] = true;
                                }
                            }
                        }
                    }
                }

                if (y > 1
                    && newBoard[y][x].type == newBoard[y - 1][x].type
                    && newBoard[y - 1][x].type == newBoard[y - 2][x].type)
                {
                    matchedTiles[y][x] = true;
                    matchedTiles[y - 1][x] = true;
                    matchedTiles[y - 2][x] = true;
                }
            }
        }

        return matchedTiles;
    }

    private static List<List<Tile>> CopyBoard(List<List<Tile>> boardToCopy)
    {
        List<List<Tile>> newBoard = new List<List<Tile>>(boardToCopy.Count);
        for (int y = 0; y < boardToCopy.Count; y++)
        {
            newBoard.Add(new List<Tile>(boardToCopy[y].Count));
            for (int x = 0; x < boardToCopy[y].Count; x++)
            {
                Tile tile = boardToCopy[y][x];
                newBoard[y].Add(new Tile { id = tile.id, type = tile.type });
            }
        }

        return newBoard;
    }

    private List<List<Tile>> CreateBoard(int width, int height, List<int> tileTypes)
    {
        List<List<Tile>> board = new List<List<Tile>>(height);
        _tileCount = 0;
        for (int y = 0; y < height; y++)
        {
            board.Add(new List<Tile>(width));
            for (int x = 0; x < width; x++)
            {
                board[y].Add(new Tile { id = -1, type = -1 });
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                List<int> noMatchTypes = new List<int>(tileTypes.Count);
                for (int i = 0; i < tileTypes.Count; i++)
                {
                    noMatchTypes.Add(_tilesTypes[i]);
                }

                if (x > 1
                    && board[y][x - 1].type == board[y][x - 2].type)
                {
                    noMatchTypes.Remove(board[y][x - 1].type);
                }
                if (y > 1
                    && board[y - 1][x].type == board[y - 2][x].type)
                {
                    noMatchTypes.Remove(board[y - 1][x].type);
                }

                if (x > 2
                   && board[y][x - 2].type == specialColorNumber
                   ||
                   x > 2
                   && board[y][x - 3].type == specialColorNumber)
                {
                    noMatchTypes.Remove(board[y][x - 2].type);
                }

                if (y > 2
                    && board[y - 2][x].type == 14
                    ||
                    y > 2
                    && board[y - 3][x].type == 14)
                {
                    noMatchTypes.Remove(board[y - 2][x].type);
                }

                board[y][x].id = _tileCount++;
                // Original:
                //board[y][x].type = noMatchTypes[Random.Range(0, noMatchTypes.Count)];
                float _rand = Random.value;
                if (_rand <= ScoreManager.instance.levelRules.specialColorsProbability)
                {
                    board[y][x].type = noMatchTypes[Random.Range(4, noMatchTypes.Count)];
                }
                else if (_rand <= ScoreManager.instance.levelRules.colorsProbability)
                {
                    board[y][x].type = noMatchTypes[Random.Range(0, noMatchTypes.Count - 4)];
                }

            }
        }

        return board;
    }
}
