using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    public Block block;
    public float health;

    private void Awake()
    {
        health = block.blockHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        GenerateLoot();
        Destroy(gameObject);
    }

    private void GenerateLoot()
    {
        int randomDrop = Random.Range(0, block.blockDrops.Length);
        Instantiate(block.blockDrops[randomDrop].itemOBJ, transform.position, Quaternion.identity);
    }
}
