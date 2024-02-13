using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject todEdge;
    [SerializeField] private GameObject bottomEdge;
    [SerializeField] private GameObject leftEdge;
    [SerializeField] private GameObject rightEdge;
    [SerializeField] private GameObject highLight;

    private Dictionary<Node, GameObject> ConnectedEdge;

    [HideInInspector] public int colorId;
    public Vector2Int Pos2D
        {
        get;
        set;
        }
    public bool IsWin
    {
        get
        {
            if(point.activeSelf)
            {
                return ConnectedNodes.Count == 1;
            }
            return ConnectedNodes.Count == 1;
        }
    }
    public bool IsClickable
    {
        get
        {
            if(point.activeSelf)
            {
                return true;
            }
            return ConnectedNodes.Count > 0;
        }
    }
    public bool IsEndNode
    {
        get
        {
            return point.activeSelf;
        }
    }
    public void Init()
    {
        point.SetActive(false);
        todEdge.SetActive(false);
        bottomEdge.SetActive(false);
        leftEdge.SetActive(false);
        rightEdge.SetActive(false);
        highLight.SetActive(false);

        ConnectedEdge = new Dictionary<Node, GameObject>(); 
        ConnectedNodes = new List<Node>();
    }

    public void SetColorForPoint(int colorIdForSpawnedNode)
    {
        this.colorId = colorIdForSpawnedNode;
        point.SetActive(true);
        point.GetComponent<SpriteRenderer>().color = GamePlayManager.instance.nodeColors[colorId];
    }
    public void SetEdge(Vector2Int offset , Node node)
    {
        if(offset == Vector2Int.up)
        {
            ConnectedEdge[node] = todEdge;
        }
        if(offset == Vector2Int.down)
        {
            ConnectedEdge[node] = bottomEdge;

        }
        if(offset == Vector2Int.left)
        {
            ConnectedEdge[node] = leftEdge; 
        }
        if(offset == Vector2Int.right)
        {
            ConnectedEdge[node] = rightEdge;
        }
    }

    public void UpdateInput(Node connectedNode)
    {
        if(!ConnectedEdge.ContainsKey(connectedNode))
        {
            return;
        }

        AddEdge(connectedNode);

    }
    private void AddEdge(Node connectedNode)
    {
        connectedNode.colorId = colorId;
        connectedNode.ConnectedNodes.Add(this);
        ConnectedNodes.Add(connectedNode);
        GameObject connectedEdge = ConnectedEdge[connectedNode];
        connectedEdge.SetActive(false);
        connectedEdge.GetComponent<SpriteRenderer>().color = GamePlayManager.instance.nodeColors[colorId] ; 
    }

    public List<Node> ConnectedNodes;
    
    public void IsHighLight()
    {

    }



}
