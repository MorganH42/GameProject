using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    public void NextScene()
    {
        SceneManager.LoadScene("Josh");
    }
}
