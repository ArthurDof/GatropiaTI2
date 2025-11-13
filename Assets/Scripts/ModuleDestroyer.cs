using UnityEngine;

public class ModuleDestroyer : MonoBehaviour
{
    public float destroyDistance = 150f;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {    
        if (player.position.z - transform.position.z > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}

