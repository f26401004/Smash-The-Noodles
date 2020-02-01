using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

    public Socket socket;
    public Item hold;
    public Item touch;

    
	public void Start()
	{
        socket = FindObjectOfType<Socket>();
	}

	public void Pick (Item item) {
        if (hold) {
			// Drop
            hold.gameObject.transform.parent = null;
        }
        item.gameObject.transform.parent = transform;
        item.gameObject.transform.localPosition = Vector3.zero;
        hold = item;

        item.GetComponent<Collider2D>().enabled = false;
        item.isHeld = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if (!item)
        {
            return;
        }
        touch = item;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if (item && item == touch)
        {
            touch = null;
        }
	}

}