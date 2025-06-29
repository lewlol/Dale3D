using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject
{
    [Header("Block Info")]
    public string blockName;
    public GameObject blockOBJ;

    [Header("Block Stats")]
    public float blockHealth;

    [Header("Block Drops")]
    public Item[] blockDrops;
}
