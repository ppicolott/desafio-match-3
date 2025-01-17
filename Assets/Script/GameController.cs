using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
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

    private int commonListIndex;
    private int specialListIndex;
    private List<bool> designedColors;
    private List<int> levelColors;
    private List<int> specialLevelColors;
    private List<bool> specialColors;

    private int _tileCount;
    private List<List<Tile>> _boardTiles;
    private List<int> _tilesTypes;

    public List<List<Tile>> StartGame(int boardWidth, int boardHeight)
    {
        // Adding color index to _tilesTypes, in order to display only selected colors on scriptable objects
        designedColors = new List<bool>()
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

        levelColors = new List<int>();
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

        // This list will help define probability of special colors being displayed
        specialLevelColors = new List<int>();
        specialColors = new List<bool>()
        {
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
        for (int i = 0; i < specialColors.Count; i++)
        {
            if (specialColors[i])
            {
                specialLevelColors.Add(i);
            }
        }

        // Original:
        // _tilesTypes = new List<int> { 0, 1, 2, 3, };
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
                CheckCorrespondent(newBoard[y][x].type);

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

                    if (newBoard[y][x].type == 15
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 2].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 1].type == 15
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 2].type == 15
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 3].type == 15)
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

                    if (newBoard[y][x].type == 15
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 2][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 1][x].type == 15
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 2][x].type == 15
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 3][x].type == 15)
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

                        int _maxCommonColors = levelColors.Count - specialLevelColors.Count;

                        int _randomCommonColor = levelColors[Random.Range(0, _maxCommonColors - 1)];
                        int _randomSpecialColor = levelColors[Random.Range(_maxCommonColors, levelColors.Count - 1)];

                        float _rand = Random.value;
                        if (_rand <= ScoreManager.instance.levelRules.specialColorsProbability)
                        {
                            _tileType = _randomSpecialColor;
                        }
                        else if (_rand <= ScoreManager.instance.levelRules.colorsProbability)
                        {
                            _tileType = _randomCommonColor;
                        }

                        Tile tile = newBoard[y][x];
                        tile.id = _tileCount++;
                        // Original:
                        // tile.type = _tilesTypes[_tileType];
                        tile.type = _tileType;
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
                CheckCorrespondent(newBoard[y][x].type);

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
                        for (int j = 0; j < newBoard.Count; j++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                matchedTiles[y][i] = true;
                            }
                        }
                    }

                    if (newBoard[y][x].type == 15
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 2].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 1].type == 15
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 2].type == 15
                        && newBoard[y][x].type == newBoard[y][x - 3].type
                        ||
                        newBoard[y][x].type == newBoard[y][x - 1].type
                        && newBoard[y][x - 1].type == newBoard[y][x - 2].type
                        && newBoard[y][x - 3].type == 15)
                    {

                        for (int j = y - ScoreManager.instance.levelRules.bombRange; j < y + ScoreManager.instance.levelRules.bombRange; j++)
                        {
                            for (int i = x - ScoreManager.instance.levelRules.bombRange; i < x + ScoreManager.instance.levelRules.bombRange; i++)
                            {
                                if (j > -1 && j < 10 && i > -1 && i < 10)
                                {
                                    matchedTiles[j][i] = true;
                                }
                            }
                        }

                        for (int n = 0; n < 4; n++)
                        {
                            if (newBoard[y][x - n].type == 15)
                            {
                                matchedTiles[y][x - n] = true;
                            }
                        }

                        GameHandler.instance.ExplosionAnimation();
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
                        for (int j = 0; j < newBoard.Count; j++)
                        {
                            for (int i = 0; i < newBoard.Count; i++)
                            {
                                matchedTiles[i][x] = true;
                            }
                        }
                    }

                    if (newBoard[y][x].type == 15
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 2][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 1][x].type == 15
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 2][x].type == 15
                        && newBoard[y][x].type == newBoard[y - 3][x].type
                        ||
                        newBoard[y][x].type == newBoard[y - 1][x].type
                        && newBoard[y - 1][x].type == newBoard[y - 2][x].type
                        && newBoard[y - 3][x].type == 15)
                    {

                        for (int j = y - ScoreManager.instance.levelRules.bombRange; j < y + ScoreManager.instance.levelRules.bombRange; j++)
                        {
                            for (int i = x - ScoreManager.instance.levelRules.bombRange; i < x + ScoreManager.instance.levelRules.bombRange; i++)
                            {
                                if (j > -1 && j < 10 && i > -1 && i < 10)
                                {
                                    matchedTiles[j][i] = true;
                                }
                            }
                        }

                        for (int n = 0; n < 4; n++)
                        {
                            if (newBoard[y - n][x].type == 15)
                            {
                                matchedTiles[y - n][x] = true;
                            }
                        }

                        GameHandler.instance.ExplosionAnimation();
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

                // Removes special color tiles
                int _startingColor = levelColors.Count - specialLevelColors.Count;
                int _scn = 0;

                for (int i = _startingColor; i < levelColors.Count; i++)
                {
                    _scn = levelColors[i];

                    if (x > 2
                        && board[y][x].type == board[y][x - 2].type
                        && board[y][x - 2].type == board[y][x - 3].type
                        && board[y][x - 1].type == _scn)
                    {
                        noMatchTypes.Remove(board[y][x - 1].type);
                    }
                    if (x > 2
                        && board[y][x].type == board[y][x - 1].type
                        && board[y][x - 1].type == board[y][x - 3].type
                        && board[y][x - 2].type == _scn)
                    {
                        noMatchTypes.Remove(board[y][x - 2].type);
                    }
                    if (y > 2
                        && board[y][x].type == board[y - 2][x].type
                        && board[y - 2][x].type == board[y - 3][x].type
                        && board[y - 1][x].type == _scn)
                    {
                        noMatchTypes.Remove(board[y - 1][x].type);
                    }
                    if (y > 2
                        && board[y][x].type == board[y - 1][x].type
                        && board[y - 1][x].type == board[y - 3][x].type
                        && board[y - 2][x].type == _scn)
                    {
                        noMatchTypes.Remove(board[y - 2][x].type);
                    }
                }

                // Removes 4 tiles of same color in a row/column
                if (x > 2
                && board[y][x].type == board[y][x - 1].type
                && board[y][x - 1].type == board[y][x - 2].type
                && board[y][x - 2].type == board[y][x - 3].type)
                {
                    noMatchTypes.Remove(board[y][x - 1].type);
                    noMatchTypes.Remove(board[y][x - 2].type);
                }
                if (y > 2
                    && board[y][x].type == board[y - 1][x].type
                    && board[y - 1][x].type == board[y - 2][x].type
                    && board[y - 2][x].type == board[y - 3][x].type)
                {
                    noMatchTypes.Remove(board[y - 1][x].type);
                    noMatchTypes.Remove(board[y - 2][x].type);
                }


                // Match 3 - Original:
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

                board[y][x].id = _tileCount++;

                // Original:
                // board[y][x].type = noMatchTypes[Random.Range(0, noMatchTypes.Count)];

                int _maxCommonColors = levelColors.Count - specialLevelColors.Count;

                int _randomCommonColor = levelColors[Random.Range(0, _maxCommonColors - 1)];
                int _randomSpecialColor = levelColors[Random.Range(_maxCommonColors, levelColors.Count - 1)];

                foreach (int item in levelColors)
                {
                    if (item == _randomSpecialColor)
                    {
                        specialListIndex = levelColors.IndexOf(item);
                    }
                    if (item == _randomCommonColor)
                    {
                        commonListIndex = levelColors.IndexOf(item);
                    }
                }
                float _rand = Random.value;
                if (_rand <= ScoreManager.instance.levelRules.specialColorsProbability)
                {
                    board[y][x].type = noMatchTypes[specialListIndex];
                }
                else if (_rand <= ScoreManager.instance.levelRules.colorsProbability)
                {
                    board[y][x].type = noMatchTypes[commonListIndex];
                }
            }
        }

        return board;
    }

    private static void CheckCorrespondent(int _type)
    {
        switch (_type)
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
                specialColorNumber = 14;
                break;
            case 15:
                colorNumber = 15;
                specialColorNumber = 15;
                break;
        }
    }
}
