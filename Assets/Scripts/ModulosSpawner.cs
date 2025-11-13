using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    public GameObject[] modulesPrefabs;
    Transform player;   
    public int tilesAhead = 3;
    float spawnZ = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {       
        while (player.position.z + (tilesAhead * 100) > spawnZ)
        {
            SpawnTile();
        }
    }
    void SpawnTile()
    {      
        GameObject tile = modulesPrefabs[Random.Range(0, modulesPrefabs.Length)];
        Instantiate(tile, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += 100;
    }
}
