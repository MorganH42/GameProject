using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Content;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    GameObject inventoryParent;

    public bool isInventoryOpened;
    int selectedHotbarSlot = 0;

    GameObject draggedObject;
    [SerializeField]
    GameObject lastItemSlot;

    [SerializeField]
    Transform handParent;

    [SerializeField]
    GameObject[] slots = new GameObject[20];
    [SerializeField] 
    GameObject itemPrefab;

    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject[] hotbarSlots = new GameObject[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HotbarItemChanged();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHotbarInput();
        inventoryParent.SetActive(isInventoryOpened);

        if(draggedObject != null)
        {
            draggedObject.transform.position = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpened)
            {
                isInventoryOpened = false; 
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                isInventoryOpened = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void CheckForHotbarInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedHotbarSlot = 0;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedHotbarSlot = 1;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedHotbarSlot = 2;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedHotbarSlot = 3;
            HotbarItemChanged();
        }
    }

    private void HotbarItemChanged()
    {
        for( int i = 0; i< handParent.childCount; i++)
        {
            handParent.GetChild(i).gameObject.SetActive(false);
        }

        foreach(GameObject slot in hotbarSlots)
        {

            Vector3 scale;

            if(slot == hotbarSlots[selectedHotbarSlot])
            {
                scale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                scale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            slot.transform.localScale = scale;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot slot = clickedObject.GetComponent<InventorySlot>();

            if (slot != null && slot.heldItem != null)
            {
                draggedObject = slot.heldItem;
                slot.heldItem = null;
                lastItemSlot = clickedObject;       
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (draggedObject != null && eventData.pointerCurrentRaycast.gameObject != null && eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot slot = clickedObject.GetComponent<InventorySlot>();

            //no item in slot - place item
            if(slot != null && slot.heldItem == null)
            {
                slot.SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(2));
            }
            
            //is item already in slot - swap items
            else if(slot != null && slot.heldItem != null)
            {
                lastItemSlot.GetComponent<InventorySlot>().SetHeldItem(slot.heldItem);
                slot.heldItem.transform.SetParent(slot.transform.parent.parent.GetChild(2));

                slot.SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(2));
            }

            //return item to last slot
            else if(clickedObject.name != "DropItem")
            {
                lastItemSlot.GetComponent<InventorySlot>().SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(2));
            }

            //drop item
            else
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Vector3 position = ray.GetPoint(3);

                GameObject newItem = Instantiate(draggedObject.GetComponent<InventoryItem>().itemScriptableObject.prefab, position, new Quaternion());
                newItem.GetComponent<ItemPickable>().itemScriptableObject = draggedObject.GetComponent<InventoryItem>().itemScriptableObject;

                lastItemSlot.GetComponent<InventorySlot>().heldItem = null;
                Destroy(draggedObject);
            }

            draggedObject = null;
        }
    }

    public void ItemPicked(GameObject pickedItem)
    {
        GameObject emptySlot = null;

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i].GetComponent<InventorySlot>();

            if (slot.heldItem == null)
            {
                emptySlot = slots[i];
                break;
            }
        }

        if (emptySlot != null)
        {
            GameObject newItem = Instantiate(itemPrefab);
            newItem.GetComponent<InventoryItem>().itemScriptableObject = pickedItem.GetComponent<ItemPickable>().itemScriptableObject;
            newItem.transform.SetParent(emptySlot.transform.parent.parent.GetChild(2));

            emptySlot.GetComponent<InventorySlot>().SetHeldItem(newItem);
            newItem.transform.localScale = new Vector3(1, 1, 1);

            Destroy(pickedItem);
        }
    }
}
