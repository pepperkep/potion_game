using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    // Whether the player is dragging the object
    private bool isDragged = false;
    // Offset between the mouse position and the object position
    private Vector3 offset = Vector3.zero;
    // Camera mouse coordinates are in reference to
    [SerializeField] private Camera dragCamera;
    private Rigidbody2D dragBody;

    // Start is called before the first frame update
    void Start()
    {
        dragBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set object position to mouse position when being dragged
        if(isDragged)
        {
            Vector3 worldMousePosition = dragCamera.ScreenToWorldPoint(Input.mousePosition);
            dragBody.MovePosition(worldMousePosition + offset);
        }
    }

    // Set values when player drags offset
    void OnMouseDown()
    {
        isDragged = true;
        Vector3 worldMousePosition = dragCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - worldMousePosition;
    }

    // Release object when player lets up mouse
    void OnMouseUp()
    {
        isDragged = false;
    }

    public bool GetIsDragging(){
        return this.isDragged;
    }
}
