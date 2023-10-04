using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetPoint : MonoBehaviour
{
    [SerializeField] GameObject eyeTarget;
    [SerializeField] GameObject rightEye;
    [SerializeField] GameObject canvas;

    private Transform target;
    private int count = 0;
    Vector3[] targetVectors = {
        new Vector3(0.2519f, 1.95537f, -1.03746f),
        new Vector3(0, 1.95537f, -1.03746f),
        new Vector3(-0.2519f, 1.95537f, -1.03746f),

        new Vector3(0.2519f, 1.70347f, -1.03746f),
        new Vector3(0, 1.70347f, -1.03746f),
        new Vector3(-0.2519f, 1.70347f, -1.03746f),

        new Vector3(0.2519f, 1.45157f, -1.03746f),
        new Vector3(0, 1.45157f, -1.03746f),
        new Vector3(-0.2519f, 1.45157f, -1.03746f)
    };

    void Start()
    {
        target = eyeTarget.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            canvas.SetActive(false);
            target.position = targetVectors[count];
            Vector3 directionToTarget = target.position - rightEye.transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToTarget);
            rightEye.transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
            count++;
        }
    }
}
