using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] public int boardWidth = 10;

    [SerializeField] public int boardHeight = 10;

    [SerializeField] public BoardView boardView;


    public static GameHandler instance;

    public GameObject _boardContainer;


    private void Awake()
    {
        gameController = new GameController();
        boardView.onTileClick += OnTileClick;

        instance = this;
        _boardContainer = GameObject.Find("BoardContainer");
    }

    private void Start()
    {
        List<List<Tile>> board = gameController.StartGame(boardWidth, boardHeight);
        boardView.CreateBoard(board);
    }

    private int selectedX, selectedY = -1;

    private bool isAnimating;

    private void OnTileClick(int x, int y)
    {
        if (isAnimating) return;

        if (selectedX > -1 && selectedY > -1)
        {
            if (Mathf.Abs(selectedX - x) + Mathf.Abs(selectedY - y) > 1)
            {
                selectedX = -1;
                selectedY = -1;
            }
            else
            {
                isAnimating = true;
                boardView.SwapTiles(selectedX, selectedY, x, y).onComplete += () =>
                {
                    bool isValid = gameController.IsValidMovement(selectedX, selectedY, x, y);
                    if (!isValid)
                    {
                        boardView.SwapTiles(x, y, selectedX, selectedY)
                        .onComplete += () => isAnimating = false;
                    }
                    else
                    {
                        List<BoardSequence> swapResult = gameController.SwapTile(selectedX, selectedY, x, y);

                        AnimateBoard(swapResult, 0, () => isAnimating = false);
                    }

                    selectedX = -1;
                    selectedY = -1;
                };
            }
        }
        else
        {
            selectedX = x;
            selectedY = y;
        }
    }

    private void AnimateBoard(List<BoardSequence> boardSequences, int i, Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();

        BoardSequence boardSequence = boardSequences[i];
        sequence.Append(boardView.DestroyTiles(boardSequence.matchedPosition));
        sequence.Append(boardView.MoveTiles(boardSequence.movedTiles));
        sequence.Append(boardView.CreateTile(boardSequence.addedTiles));

        i++;
        if (i < boardSequences.Count)
        {
            sequence.onComplete += () => AnimateBoard(boardSequences, i, onComplete);
        }
        else
        {
            sequence.onComplete += () => onComplete();
        }

        // Add points, after animation, to the score based on how many tiles were cleared/destroyed each sequence
        ScoreManager.instance.AddScore(boardSequence.matchedPosition.Count);
    }

    public void ExplosionAnimation()
    {
        _boardContainer.transform.DOShakePosition(.75f, 20f, 10, 90, false, true)
            .OnComplete(() => _boardContainer.transform.position = Vector3.zero);
    }
}
