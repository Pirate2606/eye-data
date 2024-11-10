using System.IO;
using UnityEngine;

public class DepthDetectionUnity : MonoBehaviour
{
    private AndroidJavaObject depthProcessor;
    bool notCalled = true;
    bool applyMouse = false;
    public Material material;
    public float power = -0.02f;
    string modelName = "/storage/emulated/0/Android/data/com.naitan.Parallax/files/Documents/fused_model_uint8_256.onnx";

    void Start()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        Debug.Log(unityPlayer);
        
        AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log(context);
        
        AndroidJavaClass processorClass = new AndroidJavaClass("com.ss.nativelib.NativeLib");
        Debug.Log(processorClass);

        depthProcessor = processorClass.CallStatic<AndroidJavaObject>("getInstance", modelName);
        Debug.Log(depthProcessor);
    }

    public void ProcessImage()
    {
        Texture2D texture = new Texture2D(2, 2);
        byte[] fileData = File.ReadAllBytes("/storage/emulated/0/Android/data/com.naitan.Parallax/files/Documents/inputImage.jpg");
        texture.LoadImage(fileData);
        byte[] imageBytes = texture.EncodeToPNG();
        bool result = depthProcessor.Call<bool>("predictDepth", imageBytes);
        Debug.Log($"RESULT: {result}" );
        SetTexturesFromBytes(File.ReadAllBytes("/storage/emulated/0/Android/data/com.naitan.Parallax/files/Documents/inputImage.jpg"), 
                            File.ReadAllBytes("/storage/emulated/0/Android/data/com.naitan.Parallax/files/Documents/depthMap.png"), 2, 2);
        // SetTexturesFromBytes(File.ReadAllBytes("D:\\Programs\\Parallax\\inputImage.jpg"), 
        //                      File.ReadAllBytes("D:\\Programs\\Parallax\\depthMap1.png"), 2, 2);
    }
    void Update()
    {
        if (notCalled)
        {
            ProcessImage();
            notCalled = false;
            applyMouse = true;
        }
        if (applyMouse)
        {
            // var mpos = Input.mousePosition;
            // mpos.x = (mpos.x / Screen.width * 2 - 1) * -power;
            // mpos.y = (mpos.y / Screen.height * 2 - 1) * -power;
            // Shader.SetGlobalVector("_MousePos", new Vector4(mpos.x, mpos.y, 0, 0));
            if (Input.touchCount > 0)
            {
                Debug.Log("TAG: touched");
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    // Get touch position and convert to normalized coordinates
                    Vector2 touchPos = touch.position;
                    Debug.Log($"TAG: touchPos {touchPos}");
                    Debug.Log($"TAG: Screen.width {Screen.width}");
                    Debug.Log($"TAG: Screen.height {Screen.height}");
                    float normalizedX = touchPos.x / Screen.width * -power;
                    float normalizedY = touchPos.y / Screen.height * -power;

                    // Create the Vector4 to pass to the shader
                    Vector4 mousePos = new Vector4(normalizedX, normalizedY, 0, 0);
                    Debug.Log($"TAG: mousePos {mousePos}");
                    // Update the shader property
                    material.SetVector("_MousePos", mousePos);
                }
            }
        }
        
    }

    public void SetTexturesFromBytes(byte[] mainTexBytes, byte[] depthTexBytes, int width, int height)
    {
        // Convert mainTexBytes to Texture2D
        Texture2D mainTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        mainTex.LoadImage(mainTexBytes);
        mainTex.Apply();

        // Convert depthTexBytes to Texture2D
        Texture2D depthTex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        depthTex.LoadImage(depthTexBytes);
        depthTex.Apply();
        // Material mat = GetComponent<Renderer>().material;
        // mat.mainTexture = depthTex;

        // Assign the textures to the shader
        material.SetTexture("_MainTex", mainTex );
        material.SetTexture("_DepthTex", depthTex);
    }
}

