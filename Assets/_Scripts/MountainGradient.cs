using UnityEngine;

public class GradientTexture : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public Color startColor = Color.green; // Top color
    public Color endColor = Color.white; // Bottom color (brown)

    void Start()
    {
        Debug.Log("Gradient script started!");
        
        // Create a new Texture2D
        Texture2D texture = new Texture2D(width, height);

        for (int y = 0; y < height; y++) // Loop over height first
        {
            Color color = Color.Lerp(startColor, endColor, (float)y / height); // Correct placement

            for (int x = 0; x < width; x++) // Now loop over width
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply(); // Apply changes to texture

        // Assign the generated texture to the object's material
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = new Material(Shader.Find("Unlit/Texture")); // Use an Unlit shader
            material.mainTexture = texture;
            renderer.material = material;
        }
    }
}
