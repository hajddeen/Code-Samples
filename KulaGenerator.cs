using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class KulaGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject generatedObject;
    [SerializeField]
    public float radius;
    [SerializeField]
    public int objectCount;
    [SerializeField]
    public float rotationSpeed;

    private void Start()
    {
        //Generation
        GenerateObjects();
        //Roatation
        StartCoroutine(RotateObjects());
    }

    void GenerateObjects()
    {
        //Used to space the points uniformly
        float goldenRatio = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        for (int i = 0; i < objectCount; i++)
        {
            //Distribute along y-axis
            float y = 1.0f - ((float)i / (objectCount - 1)) * 2.0f;
            float radiusAtY = Mathf.Sqrt(1.0f - y * y);
            float theta = 2.0f * Mathf.PI * goldenRatio * i;

            //Convert to the Cartesian coordinates
            float x = Mathf.Cos(theta) * radiusAtY * radius;
            float z = Mathf.Sin(theta) * radiusAtY * radius;

            //Set the spawn position
            Vector3 spawnPosition = new Vector3(x, y * radius, z) + transform.position;
            //Spawn the object in the position
            GameObject spawnedObject = Instantiate(generatedObject, spawnPosition, Quaternion.identity);
            //Set parent
            spawnedObject.transform.parent = transform;
        }
    }

    IEnumerator RotateObjects()
    {
        //Loop will run infinitely
        while (true)
        {
            //Rotate the parent
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
