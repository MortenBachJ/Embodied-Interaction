using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform MirrorCamera;
    public Transform MainCamera;

    // Update is called once per frame
    void Update()
    {
        CalculateRotation();
    }

    public void CalculateRotation()
    {
        Vector3 dir = (MainCamera.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir);

        rotation.eulerAngles = transform.eulerAngles - rotation.eulerAngles;

        MirrorCamera.localRotation = rotation;
    }
}
