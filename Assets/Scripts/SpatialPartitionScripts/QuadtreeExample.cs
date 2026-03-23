using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework.Internal;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;


 public class QuadtreeNode
{
    public Rect bounds;
    public List<GameObject> objects;
    public QuadtreeNode[] children;

    public void Split()
    {   float halfWidth =  (bounds.width)/2;
        float halfHeight =  (bounds.height)/2;
        children = new QuadtreeNode[4];
     
        children[0].bounds = new Rect(bounds.xMin,bounds.yMin,halfWidth,halfHeight);
        children[1].bounds = new Rect(bounds.xMin,bounds.yMin,halfWidth,halfHeight);
        children[2].bounds = new Rect(bounds.xMin,bounds.yMin,halfWidth,halfHeight);
        children[3].bounds = new Rect(bounds.xMin,bounds.yMin,halfWidth,halfHeight);

    }
}

public class Quadtree
{
     QuadtreeNode root;
     public int maxObjectsPerNode;
     public int maxDepth;
    public GameObject exampleObject;


     public void Insert()
    {
    
    }

}

public class QuadtreeExample : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
