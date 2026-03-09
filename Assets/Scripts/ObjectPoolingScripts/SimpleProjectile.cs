using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public GameObject projectileObjectPrefab;
    public float projectileSpeed = 50f;
    public Vector3 spawnVector;
    public Transform spawnTransform;

    private float lifetime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //called every time the object is dequeued/ called forth.
    public void Initialise()
    {
          rb = GetComponent<Rigidbody>();
        spawnVector = spawnTransform.position;
        projectileObjectPrefab.transform.position = spawnVector;
        projectileObjectPrefab.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
    }

  
    // Update is called once per frame
    void Update()
    {
        //Decreases by 1 every frame.
        lifetime -= Time.deltaTime;

        if (lifetime == 0.0f)
        {
            ObjectPooler.EnqueueObject(this, "Projectile");

        }
    }

   
}
