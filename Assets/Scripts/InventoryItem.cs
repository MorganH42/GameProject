using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{

    public ItemSO itemScriptableObject;
    [SerializeField]
    Image iconImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iconImage.sprite = itemScriptableObject.icon;   
    }
}
