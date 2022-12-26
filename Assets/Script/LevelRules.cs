using UnityEngine;

[CreateAssetMenu(fileName = "LevelRules", menuName = "Gameplay/LevelRules")]
public class LevelRules : ScriptableObject
{
    public int pointsPerTile;
    public float specialColorsProbability;
    public float colorsProbability;
}
