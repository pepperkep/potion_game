using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianReturn : MonoBehaviour
{

    [SerializeField] private float returnTime = 10f;
    private Vector3 origionalPosition;
    private Transform originalParent;
    private IEnumerator returnCoroutine;

    void Start()
    {
        origionalPosition = transform.position;
    }

    void OnEnable()
    {
        originalParent = transform.parent.parent;
        transform.parent.parent = null;
        returnCoroutine = ReturnToOriginal();
        StartCoroutine(returnCoroutine);
    }

    public IEnumerator ReturnToOriginal()
    {
        yield return new WaitForSeconds(returnTime);
        transform.position = new Vector3(origionalPosition.x, origionalPosition.y, transform.position.z);
        transform.parent.parent = originalParent;
    }
}
