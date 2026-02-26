using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation(NPCInfo spawnInfo)
    {
        GameObject itemToSpawn = spawnInfo.objects[spawnInfo.currentObject];
        Vector3 itemSpawnLocation = new Vector3(spawnInfo.itemSpawnX, spawnInfo.itemSpawnY, spawnInfo.itemSpawnZ);
        Instantiate(itemToSpawn, itemSpawnLocation, Quaternion.identity);
    }
}
