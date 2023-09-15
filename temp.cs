Color32[] colors = new Color32[width * height];

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        int index = i * width + j;
        int y = (int)yData[index];
        int u = (int)uData[index / 4]; // U is usually subsampled
        int v = (int)vData[index / 4]; // V is usually subsampled

        // Perform YUV to RGB conversion (there are different formulas, use the one you have)
        int c = y - 16;
        int d = u - 128;
        int e = v - 128;
        int r = (298 * c + 409 * e + 128) >> 8;
        int g = (298 * c - 100 * d - 208 * e + 128) >> 8;
        int b = (298 * c + 516 * d + 128) >> 8;

        colors[index] = new Color32((byte)r, (byte)g, (byte)b, 255);
    }
}

texture.SetPixels32(colors);
texture.Apply();

Renderer renderer = // Get a reference to your renderer (e.g., SpriteRenderer, MeshRenderer, etc.)
Material material = renderer.material;
material.mainTexture = texture;
