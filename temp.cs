int width = 0;
int height = 0;

for (int i = 0; i < byteArray.Length - 1; i++)
{
    if (byteArray[i] == 0xFF && byteArray[i + 1] == 0xC0)
    {
        // Found the SOF marker (FF C0)
        // The width and height are stored in the next two bytes (in big-endian order)
        width = (byteArray[i + 7] << 8) | byteArray[i + 8];
        height = (byteArray[i + 5] << 8) | byteArray[i + 6];
        break;
    }
}

if (width > 0 && height > 0)
{
    // Width and height have been successfully determined.
    // You can proceed with image processing.
}
else
{
    // Dimensions couldn't be determined from the byte array.
    // Handle this case accordingly.
}
