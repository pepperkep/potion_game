using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public bool shaken = false;
    public int shakeThreshold = 200;
    public int changeDirectionAmount = 10;
    private int currentDirectionAmount = 0;
    private int shakeAmount = 0;
    private Vector3 lastPosition;
    private Vector3 lastDisplacment;
    

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        lastDisplacment = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = transform.position - lastPosition;
        lastPosition = transform.position;
        if(currentDirectionAmount < changeDirectionAmount && displacement.magnitude > 0)
        {
            currentDirectionAmount++;
            shakeAmount++;
        }
        if(Vector3.Dot(displacement, lastDisplacment) < 0)
        {
            currentDirectionAmount = 0;
        }
        lastDisplacment = displacement;
        if(shakeAmount > shakeThreshold)
        {
            shaken = true;
        }
    }
}
