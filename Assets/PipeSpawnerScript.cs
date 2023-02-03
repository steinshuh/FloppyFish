using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 1;
    private float timer = 0;
    public float heightOffset;

    private float bubbleTop = .93f;
    private float bubbleBottom = -.93f;

    // Start is called before the first frame update
    void Start()
    {
        float cameraZ = -Camera.main.transform.position.z;
        Vector3 screenRightPoint = new Vector3(Screen.width, Screen.height/2, cameraZ) ;
        Vector3 worldRightPoint = Camera.main.ScreenToWorldPoint(screenRightPoint);
        transform.position= worldRightPoint;

        //should loop through to make several pipes
        int ticks = 60;
        for(int i = 0; i < ticks; ++i)
        {
            Update();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            float bubbleY = Random.Range(0f, 1f) > 0.5f ? bubbleTop : bubbleBottom;
            spawnPipe(bubbleY);
        }
    }

    void spawnPipe(float bubbleY)
    {
        timer = 0;
        float lowestY = transform.position.y - heightOffset;
        float highestY = transform.position.y + heightOffset;
        float y = Random.Range(lowestY, highestY);
        Vector3 startPosition = new Vector3(transform.position.x, y, 0);
        GameObject newPipe = Instantiate(pipe, startPosition, transform.rotation);
        ParticleSystem particleSystem = newPipe.GetComponentInChildren<ParticleSystem>();
        Vector3 parentPosition = particleSystem.gameObject.transform.position;
        Vector3 newPosition = new Vector3(parentPosition.x, parentPosition.y+bubbleY, parentPosition.z);
        particleSystem.gameObject.transform.position = newPosition;
    }
}
