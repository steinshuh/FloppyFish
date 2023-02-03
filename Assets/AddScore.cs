using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public Logic logic;

    // Start is called before the first frame update
    void Start()
    {
        //this is necessary because this is made from prefabs, so you can't connect them in the unity gui
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {//look up layer number in any pulldown
            logic.addScore(1);
        }
     }
}