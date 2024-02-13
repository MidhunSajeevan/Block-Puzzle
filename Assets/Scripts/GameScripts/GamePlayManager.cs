using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{
    private int levelSize = 5;
    public static GamePlayManager instance;

    private bool isGameFinished = false;


    #region PROPERTIES 
    public bool ISGameFinished
    {
        get {
            return isGameFinished;
            }
        set {
            isGameFinished = value;
            }

    }
    #endregion

    #region STARTING METHODS
    private void Awake()
    {
        instance = this; 
        SpawnBoard();
        SpawnNodes();
    }

    #endregion


    #region BOARD_SPAWN

    [SerializeField] private SpriteRenderer boardPrefab, bgCellPrefab;
    [SerializeField] private SpriteRenderer ClickHighLighter;
    private void SpawnBoard()
    {
        var board = Instantiate(boardPrefab,new Vector3(levelSize / 2f, levelSize / 2f,0f),Quaternion.identity);

        board.size = new Vector2(levelSize + 0.08f, levelSize + 0.08f);

        for(int i = 0; i < levelSize; i++)
        {
            for(int j = 0; j < levelSize; j++)
            {
                Instantiate(bgCellPrefab,new Vector3(i+0.5f,j+0.5f,0f),Quaternion.identity); 
            }
        }

       Camera.main.orthographicSize = levelSize / 2f;
       Camera.main.transform.position = new Vector3(levelSize/2f,levelSize/2f,-10f);

        ClickHighLighter.size = new Vector2 (levelSize/4,levelSize/4);
        ClickHighLighter.transform.position = Vector3.zero;
        ClickHighLighter.gameObject.SetActive(false);   
    }

    #endregion

    #region NODESPAWN
    [SerializeField]
    private LevelData CurrentLevelData;
    [SerializeField] private Node nodePrefab;
    private List<Node> nodes;

    public Dictionary<Vector2Int, Node> nodeGrid;  

    private void SpawnNodes()
    {
        nodes = new List<Node>();
        nodeGrid = new Dictionary<Vector2Int, Node>();

        Node spawnNode;
        Vector3 spawnPos;

        for(int i = 0;i < levelSize;i++)
        {
            for (int j = 0;j < levelSize;j++)
            {
                spawnPos = new Vector3(i + 0.5f, j + 0.5f, 0f);
                spawnNode = Instantiate(nodePrefab,spawnPos, Quaternion.identity);
                spawnNode.Init();

                int colorIdForSpawnNode = GetColorId(i,j);

                if(colorIdForSpawnNode != -1)
                {
                    spawnNode.SetColorForPoint(colorIdForSpawnNode);

                }
                nodes.Add(spawnNode);
                nodeGrid.Add(new Vector2Int(i,j),spawnNode);
                spawnNode.gameObject.name = i.ToString() + j.ToString();
                spawnNode.Pos2D = new Vector2Int(i,j);
            }
        }

        List<Vector2Int> offsetPos = new List<Vector2Int>(){Vector2Int.up,Vector2Int.down,Vector2Int.left,Vector2Int.right};

        foreach(var item in nodeGrid)
        {
            foreach(var offset in offsetPos)
            {
                var checkPos = item.Key + offset;
                if(nodeGrid.ContainsKey(checkPos))
                {
                    item.Value.SetEdge(offset, nodeGrid[checkPos]);
                }
            }
        }
    }

    public List<Color> nodeColors;
    public int GetColorId(int i,int j)
    {
        List<Edge> edges = CurrentLevelData.edges; 
        Vector2Int point = new Vector2Int(i,j);
        
        for(int colorId = 0; colorId < edges.Count;colorId++)
        {
            if (edges[colorId].StartPoint == point || edges[colorId].EndtPoint == point)
            {
                return colorId;
            }
        }
        return -1;
    }

    public Color GetHighLightColor(int colorId)
    {
        Color result = nodeColors[colorId];
        result.a = 0.4f;
        return result;
    }
    #endregion

    #region UPDATE MEHOD
    private Node startNode;
    private void Update()
    {
        if(isGameFinished)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            startNode = null;
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2d = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            
            if(startNode == null)
            {
                if(hit && hit.collider.TryGetComponent(out Node node) && node.IsClickable)
                {
                    startNode = node;
                    ClickHighLighter.gameObject.SetActive(true);
                    ClickHighLighter.gameObject.transform.position = (Vector3)mousePosition2d;
                    ClickHighLighter.color = GetHighLightColor(node.colorId);
                }
                return;
            }
            ClickHighLighter.gameObject.transform.position = (Vector3)mousePosition2d;

            if (hit && hit.collider.TryGetComponent(out Node tempnode) && startNode != tempnode)
            {
               if(startNode.colorId != tempnode.colorId && tempnode.IsEndNode)
                {
                    return;

                    
                }
                startNode.UpdateInput(tempnode);
                CheckWin();
                startNode = null;
            }
            return;
        }
        if(Input.GetMouseButtonUp(0))
        {
            startNode = null;
            ClickHighLighter.gameObject.SetActive(false);
        }
       }
    #endregion

    #region WINDCONDITION
    private void CheckWin()
    {
        bool isWinning = true;

        foreach(var item in nodes)
        {
            item.IsHighLight();
        }

        foreach (var item in nodes)
        {
            isWinning &= item.IsWin;

            if(!isWinning )
            {
                return;
            }
        }
        //int scene = SceneManager.GetActiveScene().buildIndex;
        //GameManager.Instance.LoadNextScene(scene);
    }
    #endregion


}
