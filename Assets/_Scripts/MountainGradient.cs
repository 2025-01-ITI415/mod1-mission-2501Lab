using UnityEngine;

public class GradientTexture : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public Color startColor = Color.green;
    public Color endColor = new Color(139f / 255f, 69f / 255f, 19f / 255f); // Brown

    void Start()
    {
        Debug.Log("Gradient script started!");
        // Create a new Texture2D
        Texture2D texture = new Texture2D(width, height);
        
        // Loop through pixels to generate the gradient
        for (int x = 0; x < width; x++)
        {
            Color color = Color.Lerp(startColor, endColor, (float)x / width);
            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply(); // Apply changes to the texture
        
        // Assign the generated texture to the object's material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = new Material(Shader.Find("Unlit/Texture")); // Ensure it's visible
            material.mainTexture = texture;
            renderer.material = material;
        }
    }
}