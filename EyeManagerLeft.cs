using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManagerLeft : MonoBehaviour
{
    [SerializeField] GameObject eye;
    [SerializeField] GameObject pupil;
    [SerializeField] GameObject canvas;

    private Vector3[] rotationArray = new Vector3[5] {
        new Vector3(10 ,15, 0),
        new Vector3(-5 ,0, 0),
        new Vector3(0 ,0, 0),
        new Vector3(-5 ,-20, 0),
        new Vector3(10 ,-15, 0)
    };
    private int count = 0;
    private float[] leftCameraPosition = { 0.05f, 1.685f, -0.065f };
    private float[] leftCameraRotation = { 323.57f, 308.45f, 333.79f };
    private float[] rightCameraPosition = { -0.055f, 1.685f, -0.0705f };
    private float[] rightCameraRotation = { 323.53f, 47.67f, 387.02f };


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"Initial Eye Rotation: {eye.transform.rotation.eulerAngles.ToString("F6")}");
            Debug.Log($"Initial Eye Position: {eye.transform.position.ToString("F6")}");
            Debug.Log($"Initial Pupil Rotation: {pupil.transform.rotation.eulerAngles.ToString("F6")}");
            Debug.Log($"Initial Pupil Position: {pupil.transform.position.ToString("F6")}");
            Debug.Log($"Initial Pupil Rotation Matrix:\n {pupil.transform.localToWorldMatrix.ToString("F6")}");
            Camera.main.transform.position = new Vector3(leftCameraPosition[0], leftCameraPosition[1], leftCameraPosition[2]);
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(leftCameraRotation[0], leftCameraRotation[1], leftCameraRotation[2]));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            canvas.SetActive(false);
            eye.transform.localRotation = Quaternion.Euler(rotationArray[count]);
            count++;
            Debug.Log($"Eye Rotation: {eye.transform.rotation.eulerAngles.ToString("F6")}");
            Debug.Log($"Eye Position: {eye.transform.position.ToString("F6")}");
            Debug.Log($"Pupil Rotation: {pupil.transform.rotation.eulerAngles.ToString("F6")}");
            Debug.Log($"Pupil Position: {pupil.transform.position.ToString("F6")}");
            Debug.Log($"Pupil Rotation Matrix:\n {pupil.transform.localToWorldMatrix.ToString("F6")}");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot($"eye_{Time.time}.png");

        }
    }
}
