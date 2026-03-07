using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    MoneyManager moneyManager;
    [SerializeField]
    TextMeshProUGUI deathText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfDay(int dayNumber)
    {
        if (moneyManager.money >= 10)
        {
            moneyManager.money -= 10;
        }
        else
        {
            Die("You didn't have enough money for food. You were found the next morning in your box.");
        }
    }

    void Die(string deathMessage)
    {
        SceneManager.LoadScene("Death");

    }
}
