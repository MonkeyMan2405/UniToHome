using UnityEngine;

public class GameManager : MonoBehaviour
{

    public SimpleProjectile projectilePrefab;


    private void Awake()
    {
        SetupPool();
    }


    private void SetupPool()
    {
        ObjectPooler.SetupPool(projectilePrefab, 10, "Projectile");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
