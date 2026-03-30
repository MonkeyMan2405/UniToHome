using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
     private PressurePlate pressurePlateRef;

     private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        pressurePlateRef.OnActivated.AddListener(AddScore);
    }

    private void OnDisable()
    {
        pressurePlateRef.OnActivated.RemoveListener(AddScore);
    }

    private void AddScore()
    {
        score += 10;
        Debug.Log("Score: " + score);
    }


}
