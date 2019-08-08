using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateOnDragStop : MonoBehaviour
{
    [SerializeField] private string layerToCheck = "DragDrop";
    private DragDrop dragComponent;
    private Rigidbody2D rigidbodyComponent;
    private bool dragStart = false;

    // Start is called before the first frame update
    void Start()
    {
        dragComponent = gameObject.GetComponent<DragDrop>();
        rigidbodyComponent = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dragComponent.GetIsDragging())
        {
            dragStart = true;
        }
        else
        {
            if(dragStart)
            {
                RespawnPotions();
            }
        }
    }

    public void RespawnPotions()
    {
        List<Collider2D> resultsList = new List<Collider2D>();
        LayerMask mask = LayerMask.GetMask(layerToCheck);
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        rigidbodyComponent.OverlapCollider(filter, resultsList);
        for(int i = 0; i < resultsList.Count; i++)
        {
            RespawnPotion respawnPoint = resultsList[i].gameObject.GetComponent<RespawnPotion>();
            if(respawnPoint != null)
            {
                respawnPoint.RespawnPreviousPotion();
            }
        }
        Destroy(this.gameObject);
    }
}
