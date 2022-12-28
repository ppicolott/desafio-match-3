using UnityEngine;

[CreateAssetMenu(fileName = "LevelRules", menuName = "Gameplay/LevelRules")]
public class LevelRules : ScriptableObject
{
    [Space(10)]
    [Header("Statics")]
    [Space(5)]
    public int pointsPerTile;
    public float specialColorsProbability;
    public float colorsProbability;

    [Space(10)]
    [Header("Common Colors")]
    [Space(5)]
    public bool yellow;
    public bool blue;
    public bool green;
    public bool orange;
    public bool pink;
    public bool purple;
    public bool red;

    [Space(10)]
    [Header("Special Colors")]
    [Space(5)]
    public bool specialYellow;
    public bool specialBlue;
    public bool specialGreen;
    public bool specialOrange;
    public bool specialPink;
    public bool specialPurple;
    public bool specialRed;

    [Space(10)]
    [Header("Special Tricks")]
    [Space(5)]
    public bool lineCleaner;
    public bool bomb;
}
