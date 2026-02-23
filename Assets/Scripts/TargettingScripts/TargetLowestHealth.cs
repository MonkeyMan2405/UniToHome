using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TargetLowestHealth : MonoBehaviour
{

    [SerializeField]
    List<Enemy> EnemiesList = new List<Enemy>();
    WaitForSeconds waitFor1Second = new WaitForSeconds(1);

    void Start()
    {

        //foreach (Enemy enemy in EnemiesList)
        //{
        //    Debug.Log(enemy.EnemyHealth);
        //}
        EnemyHealthInsertionSortList(EnemiesList);
       // Debug.Log("Sorted List:");
        //foreach (Enemy enemy in EnemiesList)
        //{
        //    Debug.Log(enemy.EnemyHealth);
        //}
        StartCoroutine(LookThenDestroy());

    }

    
    IEnumerator LookThenDestroy()
    {
        foreach (Enemy enemy in EnemiesList)
        {
            yield return waitFor1Second;
            transform.LookAt(enemy.gameObject.transform);
            yield return waitFor1Second;
            Destroy(enemy.gameObject);
            //pause 1
            //look at enenmy
            //pause 1
            //destroy enemy object
        }
        Debug.Log("Game Over");
    }

        string ListToString(List<Enemy> list)
    {
        return string.Join(", ", list);
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
      
    }

}

