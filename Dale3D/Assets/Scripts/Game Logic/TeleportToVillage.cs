using UnityEngine;

public class TeleportToVillage : MonoBehaviour
{
    public GameObject player;
    public Transform villageSpawnPoint;
    bool canTP;

    private void Update()
    {
        if (canTP)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                player.transform.position = villageSpawnPoint.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canTP = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canTP = false;
        }
    }
}
