using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "LevelsData", menuName = "LevelData/Levels")]

public class LevelData : ScriptableObject
{
    [Serializable]
    public struct Level
    {
        [Range(1,50)]
        public int countEnemies;
        [Range(1, 5)]
        public int countExits;
        public float speedPlayer;
        public float speedEnemies;
        public Vector2 levelSize;
    }

    [SerializeField] private List<Level> _levels;

    public List<Level> Levels => _levels;
}
