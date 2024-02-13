using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "SO/AllLevels")]
public class LevelList : ScriptableObject
{
    public List<LevelData> levels;
    
}
