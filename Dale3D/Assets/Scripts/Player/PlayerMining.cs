using System;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    Camera cam;
    Plane plane = new Plane(Vector3.forward, 0);

    GameObject hoveringBlock;
    GameObject previousBlock;

    float c;

    //Mining Stats
    public float miningSpeed;
    public float miningDamage;
    public float miningRange;

    private void Awake()
    {
        c = miningSpeed;
        cam = Camera.main;
    }

    private void Update()
    {
        SendMiningRaycast();

        //If the block can be destroyed and the mouse button is down
        if (Input.GetMouseButton(0) && hoveringBlock != null)
        {
            c = c -= Time.deltaTime;
            if(c <= 0)
            {
                hoveringBlock.GetComponent<BlockHealth>().TakeDamage(miningDamage);
                c = miningSpeed;
            }
        }
    }

    private void SendMiningRaycast()
    {
        //Detect Mouse Position
        Vector3 mousePos = MousePosition();

        //Direction to the mouse from Player
        Vector3 direction = mousePos - transform.position;

        //Detect if the block can be destroyed
        RaycastHit blockDetector;
        if (Physics.Raycast(transform.position, direction, out blockDetector, miningRange))
        {
            if (blockDetector.transform.tag == "Player") //Prevents an error when pointing at player
                return;

            if(blockDetector.transform.tag == "Block") //If the Raycast hit a Block
            {
                //If it is different to the current block
                if(blockDetector.transform.gameObject != hoveringBlock)
                {
                    previousBlock = hoveringBlock;
                    hoveringBlock = blockDetector.transform.gameObject;
                    ChangeHoverBlockStatus();
                }
            }
            else
            {
                previousBlock = hoveringBlock;
                hoveringBlock = null;
                ChangeHoverBlockStatus();
            }
        }
        else //Nothing was Hit
        {
            previousBlock = hoveringBlock;
            hoveringBlock = null;
            ChangeHoverBlockStatus();
        }

        Debug.DrawRay(transform.position, direction.normalized * miningRange, Color.red);
    }

    private Vector3 MousePosition() //This should never send back Vector3.zero - Always hits a point on the plane
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;
        if (plane.Raycast(ray, out distanceToPlane))
        {
            Vector3 hitPoint = ray.GetPoint(distanceToPlane);
            return hitPoint;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void ChangeHoverBlockStatus()
    {
        if(hoveringBlock != null) { hoveringBlock.GetComponentInChildren<Outline>().enabled = true; }
        if (previousBlock != null) { previousBlock.GetComponentInChildren<Outline>().enabled = false; }
    }
}
