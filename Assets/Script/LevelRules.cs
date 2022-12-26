using UnityEngine;

[CreateAssetMenu(fileName = "LevelRules", menuName = "Gameplay/LevelRules")]
public class LevelRules : ScriptableObject
{
    public int pointsPerTile;
    public float lineCleanerProbability;
    public float othersProbability;
}
