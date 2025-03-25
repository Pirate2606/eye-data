using UnityEngine;
using System.IO;

public class WorldCoordinateScreenshot : MonoBehaviour
{
    public Camera captureCamera;  // Assign a secondary camera in Unity
    public Vector3[] worldCorners = new Vector3[4]; // 4 world coordinates

    public int screenshotWidth = 1920;
    public int screenshotHeight = 1080;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        if (captureCamera == null)
        {
            Debug.LogError("Capture Camera is not assigned!");
            return;
        }

        // 1. Calculate the bounding box
        // Vector3 minBounds = new Vector3(
        //     Mathf.Min(worldCorners[0].x, worldCorners[1].x, worldCorners[2].x, worldCorners[3].x),
        //     Mathf.Min(worldCorners[0].y, worldCorners[1].y, worldCorners[2].y, worldCorners[3].y),
        //     Mathf.Min(worldCorners[0].z, worldCorners[1].z, worldCorners[2].z, worldCorners[3].z)
        // );

        // Vector3 maxBounds = new Vector3(
        //     Mathf.Max(worldCorners[0].x, worldCorners[1].x, worldCorners[2].x, worldCorners[3].x),
        //     Mathf.Max(worldCorners[0].y, worldCorners[1].y, worldCorners[2].y, worldCorners[3].y),
        //     Mathf.Max(worldCorners[0].z, worldCorners[1].z, worldCorners[2].z, worldCorners[3].z)
        // );

        // Vector3 center = (minBounds + maxBounds) / 2;  // Center of the area
        // Vector3 size = maxBounds - minBounds;          // Size of the area
        GameObject temp = GameObject.Find("cell 4(Clone)");
        Debug.Log(temp.transform.position);
        Vector3 center = temp.transform.position;

        // 2. Position the capture camera
        // captureCamera.transform.position = center + new Vector3(0, 0, 0); // Adjust for 2D or set Z manually
        captureCamera.transform.LookAt(center); // Ensure it looks at the area

        // Adjust camera settings
        // if (captureCamera.orthographic)
        // {
        //     captureCamera.orthographicSize = Mathf.Max(size.x, size.y) / 2; // Fit the area in orthographic view
        // }
        // else
        // {
        //     captureCamera.fieldOfView = 60; // Adjust manually if needed
        // }

        // 3. Create RenderTexture
        RenderTexture rt = new RenderTexture(screenshotWidth, screenshotHeight, 24);
        captureCamera.targetTexture = rt;
        captureCamera.Render();

        // 4. Convert RenderTexture to PNG
        Texture2D screenshot = new Texture2D(screenshotWidth, screenshotHeight, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, screenshotWidth, screenshotHeight), 0, 0);
        screenshot.Apply();

        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 5. Save the image
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/screenshot.png", bytes);
        Debug.Log("Screenshot saved to: " + Application.persistentDataPath + "/screenshot.png");
    }
}
