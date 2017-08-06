using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : PhysicsObjects {

    CompositeCollider2D ladder;
    public bool OnLadder;

    // Use this for initialization
    void Start () {
        ladder = GetComponent<CompositeCollider2D>();
        
        OnLadder = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (ladder.isTrigger == true)
        {
            OnLadder = true;
            
        }
        else
        {
            OnLadder = false;
            
        }
    }
}
