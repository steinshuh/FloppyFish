using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public Rigidbody2D body;
    public float thrust;
    public Logic logic;
    public bool alive = true;

    private float _worldTop;
    private float _worldBottom;

    public Animator animator;
    int flapCount = 0;

    int maxFlapCount = 8;

    private bool[] _flaps;
    private int _flapPointer;
    private int _flapSum;

    // Start is called before the first frame update
    void Start()
    {

        //get screen dimensions
        float cameraZ = -Camera.main.transform.position.z;
        Vector3 screenBottomPoint = new Vector3(0, 0, cameraZ);
        Vector3 screenTopPoint = new Vector3(0, Screen.height, cameraZ);
        //get the world point at the top and bottom of the screen
        Vector3 worldBottomPoint = Camera.main.ScreenToWorldPoint(screenBottomPoint);
        Vector3 worldTopPoint = Camera.main.ScreenToWorldPoint(screenTopPoint);
        _worldBottom = worldBottomPoint.y;
        _worldTop = worldTopPoint.y;


        animator = GetComponent<Animator>();
        animator.SetBool("flapping", false);

        body.velocity = Vector2.zero;

        _flaps = new bool[10*maxFlapCount];
        _flapPointer = 0;
        _flapSum = 0;
    }



    private bool flapPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

     private bool bottomBump()
    {
        return body.position.y < _worldBottom;
    }

    private bool topBump()
    {
        return body.position.y > _worldTop;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //circular buffer for flap tracking (many recent means greater velocity)
            if (_flaps[_flapPointer])
            {
                --_flapSum;
                _flaps[_flapPointer] = false;
            }
            if (flapPressed())
            {
                _flaps[_flapPointer] = true;
                ++_flapSum;
                Debug.Log("_flapSum: " + _flapSum.ToString());
                float flapCountWeight = 1 + (float)(_flapSum -1)*0.5f;
                body.velocity = Vector2.up * (thrust*flapCountWeight); 
                
                //animation
                flapCount = maxFlapCount;
                animator.SetBool("flapping", true);
            }
            else if (bottomBump())
            {
                body.position.Set(0, _worldBottom);
                body.velocity = Vector2.up * thrust;
            }
            else if (topBump())
            {
                body.velocity = new Vector2(0, 0);
                body.position.Set(0, _worldTop);
            }
            if (flapCount > 0)
            {
                --flapCount;
            }
            else
            {
                animator.SetBool("flapping", false);
            }
            ++_flapPointer;
            if (_flapPointer >= _flaps.Length)
            {
                _flapPointer = 0;
            }

        }
        else
        {
            animator.SetBool("flapping", true);
            body.gravityScale = -.4f;
            body.mass = .8f;
            transform.Rotate(new Vector3(0,0,1f));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        alive = false;
        logic.gameOver();
    }
}
