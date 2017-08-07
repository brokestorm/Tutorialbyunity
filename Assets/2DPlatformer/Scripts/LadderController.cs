using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
	private Player2Controller player;

	void OnEnable()
	{
		player = FindObjectOfType<Player2Controller> ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
        {
			player.isOnLadder = true;
        }
    }
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			player.isOnLadder = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			player.isOnLadder = false;
		}
	}
}
