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
        for(int i = 0; i < objectCount; i++)
        {
            //Calculate the angle to make distance between objects equal
            float angle = i * (360f / objectCount);
            
            //Convert angles to radiants
            float radiants = angle * Mathf.Deg2Rad;

            //Setting the spawn position
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radiants) * radius, 0f, Mathf.Sin(radiants) * radius);

            //Spawning the object on the radious of the circle as a child.
            var newObject = Instantiate(generatedObject, spawnPosition, Quaternion.identity);
            newObject.transform.parent = gameObject.transform;

            //Testing
            //Debug.Log(angle);
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
