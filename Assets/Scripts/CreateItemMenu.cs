using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CreateItemMenu : MonoBehaviour
{
    [System.Serializable]
    public struct Purchasable
    {
        public GameObject ItemToPurchasable;
        public string Name;
        public int PiecePrice;
        public int WaveToBuy;
    }

    private HorizontalLayoutGroup stuffToPurchase;
    [SerializeField] private List<Purchasable> purchasableItems = new List<Purchasable>();
    [SerializeField] private GameObject basePurchasableUI;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        stuffToPurchase = GetComponent<HorizontalLayoutGroup>();
        mainCamera = Camera.main;
        AddNewPurchasables(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewPurchasables(int waveNumber)
    {
        for(int i = 0; i < purchasableItems.Count; i++)
        {
            if(purchasableItems[i].WaveToBuy == waveNumber){
                GameObject PurchasableItemUI = Instantiate(basePurchasableUI, transform);

                EventTrigger trigger = PurchasableItemUI.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.BeginDrag;
                int tempInt = i;
                entry.callback.AddListener((data) => { BuyItem(tempInt); });
                trigger.triggers.Add(entry);

                TextMeshProUGUI textChild = PurchasableItemUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                textChild.SetText(purchasableItems[i].Name + "\n{0} Pieces", purchasableItems[i].PiecePrice);
            }
        }
    }

    public void BuyItem(int indexItem)
    {
        if(GameStatusManager.Instance.Parts >= purchasableItems[indexItem].PiecePrice)
        {
            GameStatusManager.Instance.Parts -= purchasableItems[indexItem].PiecePrice;
            Vector3 mousePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            GameObject boughtItem = Instantiate(purchasableItems[indexItem].ItemToPurchasable, new Vector3(mousePoint.x, mousePoint.y, transform.position.z), Quaternion.identity);
            DragDrop dragComponent = boughtItem.GetComponent<DragDrop>();
            dragComponent.dragCamera = mainCamera;
            dragComponent.StartDragging();
        }
    }
}
