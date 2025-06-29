using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public Block stoneBlock;
    public int xWidth;
    public int yWidth;
    [Range(0, 0.3f)] public float caveFrequency;
    int seed;

    Texture2D caveTexture;

    private void Awake()
    {
        seed = Random.Range(-10000, 10000);
        GenerateCaveTexture();
        GenerateWorld();
    }

    private void SpawnBlock(Vector3 position)
    {
        Instantiate(stoneBlock.blockOBJ, position, Quaternion.identity);
    }

    private void GenerateCaveTexture()
    {
        caveTexture = new Texture2D(xWidth * 2, yWidth * 2);

        for (int x = 0; x < caveTexture.width; x++)
        {
            for (int y = 0; y < caveTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFrequency, (y + seed) * caveFrequency);
                caveTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        caveTexture.Apply();
    }

    private void GenerateWorld()
    {
        for (int x = 0; x < xWidth; x++)
        {
            for (int y = 0; y < yWidth; y++)
            {
                if(caveTexture.GetPixel(x, y).r > 0.5f)
                {
                    SpawnBlock(new Vector3(x, -y, 0));
                }
            }
        }
    }
}
