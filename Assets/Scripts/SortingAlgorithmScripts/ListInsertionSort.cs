using UnityEngine;
using System.Collections.Generic;
public class ListInsertionSort : MonoBehaviour
{
    List<int> NumberList = new List<int> { 7, 1, 9, 6, 0 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Original List: " + ListToString(NumberList));

        InsertionSortList(NumberList);

        Debug.Log("Sorted List: " + ListToString(NumberList));
    }

    string ListToString(List<int> list)
    {
        return string.Join(", ", list);
    }

    void InsertionSortList(List<int> list)
    {
       // int number[1, 7, 9, 6, 0];
        int n = list.Count;
        for (int i = 1; i < n; i++)
        {
            int key = list[i];//

            int j = i - 1;//

            //move elements of list [0. .i-1] that are greater than key to one position ahead

            while (j >= 0 && list[j] > key)
            {
                list[j+1] = list[j];         

                j = j - 1;
            }
            list[j+1] = key; 
        }
    }
}
