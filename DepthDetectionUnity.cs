using UnityEngine;

public class DepthDetectionUnity : MonoBehaviour
{
    private AndroidJavaObject depthProcessor;
    bool notCalled = true;

    void Start()
    {
        // Initialize the DepthDetectionProcessor instance from Unity
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        Debug.Log(unityPlayer);
        AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log(context);
        // Replace `modelName` with the actual model file name, like "fused_model_uint8_256.onnx"
        string modelName = "/storage/emulated/0/Android/data/com.DefaultCompany.Parallax/files/fused_model_int8_256.onnx";

        // Get the DepthDetectionProcessor instance
        AndroidJavaClass processorClass = new AndroidJavaClass("com.ss.nativelib.NativeLib");
        Debug.Log(processorClass);
        depthProcessor = processorClass.CallStatic<AndroidJavaObject>("getInstance", modelName);
        Debug.Log(depthProcessor);
    }

    public void ProcessImage()
    {
        // Call predictDepth on the instance
        string resultImage = depthProcessor.Call<string>("predictDepth");
        Debug.Log(resultImage);
        // Handle `resultImage` as needed
    }
    void Update()
    {
        if (notCalled)
        {
            ProcessImage();
            notCalled = false;
        }
    }
}
