using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] int width = 256;
    [SerializeField] int height = 256;
    [SerializeField] float scale = 20f;

    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        var texture = new Texture2D(width, height);

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Color color = CalculateColor(i, j);
                texture.SetPixel(i, j, color);
            }
        }
        texture.Apply();

        return texture;
    }

    private Color CalculateColor(int x, int y)
    {
        float coordX = (float)x / width * scale;
        float coordY = (float)y / height * scale;
        float sample = Mathf.PerlinNoise(coordX, coordY);

        return new Color(sample, sample, sample);
    }
}
