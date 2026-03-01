using UnityEngine;

public class NPCInfo : MonoBehaviour
{
    //list of items
    [SerializeField]
    public float itemSpawnX;
    [SerializeField]
    public float itemSpawnY;
    [SerializeField]
    public float itemSpawnZ;
    [SerializeField]
    public string NPCName;
    [SerializeField]
    public GameObject[] objects = new GameObject[5];
    public int currentObject = 0;
    [SerializeField]
    public int NPCType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentObject += 1;
        }
        
    }
}
