using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "SO/Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<Edge> edges;

}
    [System.Serializable]
    public struct Edge
    {
        public List<Vector2Int> points;
        
        public Vector2Int StartPoint
        {
            get
            {
                if(points != null && points.Count > 0)
                {
                    return points[0];
                }
                return new Vector2Int(-1, -1);
            }
        }

        public Vector2Int EndtPoint
        {
            get
            {
                if (points != null && points.Count > 0)
                {
                    return points[points.Count-1];
                }
                return new Vector2Int(-1, -1);
            }
        }
    }

