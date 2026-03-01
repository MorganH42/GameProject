using UnityEngine;
using TMPro;
public class MoneyManager : MonoBehaviour
{

    public TextMeshProUGUI moneyText;
    int money = 0;
    InventoryManager inventoryManager;
    NPCManager NPCManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public void GainLoseMoney(int amount)
    {
        money += amount;
    }
}
