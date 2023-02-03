using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 5;
    float deadZone;

    // Start is called before the first frame update
    void Start()
    {
        float cameraZ = -Camera.main.transform.position.z;
        Vector3 screenLeftPoint = new Vector3(0, Screen.height / 2, cameraZ);
        Vector3 worldLeftPoint = Camera.main.ScreenToWorldPoint(screenLeftPoint);
        deadZone = worldLeftPoint.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position + (Vector3.left * speed * Time.deltaTime);
        transform.position = pos;
        if(transform.position.x < deadZone)
        {
            //Debug.Log("pipe dead");
            Destroy(gameObject);
        }
    }
}
