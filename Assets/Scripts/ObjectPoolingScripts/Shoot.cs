using UnityEngine;

public class Shoot : MonoBehaviour
{

    public SimpleProjectile projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimpleProjectile instance = ObjectPooler.DequeueObject<SimpleProjectile>("Projectile");
            instance.gameObject.SetActive(true);
            instance.Initialise();
            
        }
    }
}
