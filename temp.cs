// Get the byte array of the YUV image.
byte[] yuvBytes = ...;

// Get the width and height of the image.
int width = ...;
int height = ...;

// Initialize the RGB image.
byte[] rgbBytes = new byte[width * height * 3];

// Convert the YUV image to RGB.
for (int i = 0; i < height; i++) {
  for (int j = 0; j < width; j++) {
    // Get the Y, U, and V values from the YUV image.
    int y = yuvBytes[i * width + j];
    int u = yuvBytes[width * height + (i >> 1) * width + (j & ~1) + 0];
    int v = yuvBytes[width * height + (i >> 1) * width + (j & ~1) + 1];

    // Convert the Y, U, and V values to RGB values.
    int r = y + 1.4075 * (v - 128);
    int g = y - 0.3456 * (u - 128) - 0.7169 * (v - 128);
    int b = y + 1.7790 * (u - 128);

    // Store the RGB values in the RGB image.
    rgbBytes[i * width * 3 + j * 3 + 0] = r;
    rgbBytes[i * width * 3 + j * 3 + 1] = g;
    rgbBytes[i * width * 3 + j * 3 + 2] = b;
  }
}

// Create a Texture2D from the RGB image.
Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
texture.SetPixels(rgbBytes);
texture.Apply();

// Display the texture.
// ...
