using UnityEngine;

public class TeleportToCave : MonoBehaviour
{
    public GameObject player;
    public Transform caveSpawnPoint;
    bool canTP;

    private void Update()
    {
        if (canTP)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                player.transform.position = caveSpawnPoint.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canTP = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canTP = false;
        }
    }
}
