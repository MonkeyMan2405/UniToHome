using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using Unity.Mathematics;
using JetBrains.Annotations;

public class LerpTargetLowestHealth : MonoBehaviour
{

    [SerializeField]
    List<Enemy> EnemiesList = new List<Enemy>();
    WaitForSeconds waitFor1Second = new WaitForSeconds(1);
    private float rotationSpeed = 2f;
    private Transform currentTarget;
    private Vector3 targetDirection;
    private Quaternion targetRotation;

    void Start()
    {
        EnemyHealthInsertionSortList(EnemiesList);
      
        StartCoroutine(LookThenDestroy());
    }

    
    IEnumerator LookThenDestroy()
    {
        foreach (Enemy enemy in EnemiesList)
        {
            //Sets variables from enemy of where it needs to look to
            currentTarget = enemy.transform;
            targetDirection = currentTarget.position - transform.position;//itself
            targetRotation = Quaternion.LookRotation(targetDirection);
            yield return waitFor1Second;
            //rotates to look at in Update Function
       
            yield return waitFor1Second;
            Destroy(enemy.gameObject);
        }
        Debug.Log("Game Over");
    }

    
    void EnemyHealthInsertionSortList(List<Enemy> list)
    {
        int n = list.Count;
        for (int i = 1; i < n; i++)
        {
            Enemy key = list[i];
            int j = i - 1;
           
            //move elements of list [0. .i-1] that are greater than key to one position ahead

            while (j >= 0 && list[j].EnemyHealth > key.EnemyHealth)
            {
                list[j + 1] = list[j];

                j = j - 1;
            }
            list[j + 1] = key;
        }
    }

    void Update()
    {
        //Rotate towards Target
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}

