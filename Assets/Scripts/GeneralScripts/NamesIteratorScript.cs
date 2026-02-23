using UnityEngine;

public class NamesIteratorScript : MonoBehaviour
{
    private string[] names = { "Charlie", "Abigail", "Blake", "James", };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {// Light blue Name is a variables that is created to store Names
        foreach (string name in names)
        {
            Debug.Log("Name: " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
