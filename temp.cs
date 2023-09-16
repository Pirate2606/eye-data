using UnityEngine;

public class YUV420NV21Display : MonoBehaviour
{
    public int width = 640; // Width of the image
    public int height = 480; // Height of the image
    public byte[] yuvData; // Your YUV420 NV21 byte array

    private Texture2D texture;

    void Start()
    {
        // Convert YUV420 NV21 to RGB
        byte[] rgbData = ConvertYUV420ToRGB(yuvData, width, height);

        // Create a texture
        texture = new Texture2D(width, height);
        texture.LoadRawTextureData(rgbData);
        texture.Apply();

        // Attach the texture to a GameObject's material
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    byte[] ConvertYUV420ToRGB(byte[] yuvData, int width, int height)
    {
        byte[] ConvertYUV420ToRGB(byte[] yuvData, int width, int height)
{
    int size = width * height;
    byte[] rgbData = new byte[size * 3];

    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int Y = yuvData[i * width + j] & 0xFF;
            int V = yuvData[(size + (i / 2) * width) + (j / 2) * 2] & 0xFF;
            int U = yuvData[(size + (i / 2) * width) + (j / 2) * 2 + 1] & 0xFF;

            int C = Y - 16;
            int D = U - 128;
            int E = V - 128;

            int R = (298 * C + 409 * E + 128) >> 8;
            int G = (298 * C - 100 * D - 208 * E + 128) >> 8;
            int B = (298 * C + 516 * D + 128) >> 8;

            // Clamp values to the 0-255 range
            R = Mathf.Clamp(R, 0, 255);
            G = Mathf.Clamp(G, 0, 255);
            B = Mathf.Clamp(B, 0, 255);

            rgbData[(i * width + j) * 3] = (byte)R;
            rgbData[(i * width + j) * 3 + 1] = (byte)G;
            rgbData[(i * width + j) * 3 + 2] = (byte)B;
        }
    }

    return rgbData;
}

    }
}
