using System;
using UnityEngine;
using UnityEngine.Events;

public class MouseController : MonoBehaviour
{

    //public static Action<RaycastHit> OnLeftMouseClick;
    //public static Action<RaycastHit> OnRightMouseClick;
    //public static Action<RaycastHit> OnMiddleMouseClick;

    public static UnityEvent<RaycastHit> OnLeftMouseClick;
    public static UnityEvent<RaycastHit> OnRightMouseClick;
    public static UnityEvent<RaycastHit> OnMiddleMouseClick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            CheckMouseClick(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CheckMouseClick(1);
        }

        if (Input.GetMouseButtonDown(2))
        {
            CheckMouseClick(2);
        }

    }


    void CheckMouseClick(int mouseButton)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseRayHit;

        //invoke the appropriate event based on the mouse button that was clicked, passing the raycast hit information as an argument
        if (Physics.Raycast(mouseRay, out mouseRayHit, Mathf.Infinity))
        {
            if(mouseButton == 0)
            {
                OnLeftMouseClick?.Invoke(mouseRayHit);
            }
            else if (mouseButton == 1)
            {
                OnRightMouseClick?.Invoke(mouseRayHit);
            }
            else if (mouseButton == 2)
            {
                OnMiddleMouseClick?.Invoke(mouseRayHit);
            }
        }
    }

}
