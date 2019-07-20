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
    public Camera dragCamera;
    private Rigidbody2D dragBody;

    public Camera DragCamera
    {
        get => dragCamera;
        set => dragCamera = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        dragBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isDragged && Input.GetKeyUp("mouse 0"))
        {
            EndDrag();
        }
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
        StartDragging();
    }

    public bool GetIsDragging(){
        return this.isDragged;
    }

    public void StartDragging()
    {
        isDragged = true;
        Vector3 worldMousePosition = dragCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - worldMousePosition;
    }

    public void EndDrag()
    {
        isDragged = false;
    }
}
