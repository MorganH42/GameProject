using UnityEngine;

public class ItemSellable : MonoBehaviour
{
    [SerializeField]
    MoneyManager moneyManager;
    public ItemSO itemScriptableObject;

    private void Start()
    {
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SellDesk")
        {
            moneyManager.GainLoseMoney(itemScriptableObject.value);
            Destroy(gameObject);
        }
    }
}
