using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

    public Socket socket;
    protected Item hold;
    protected Item touch;

    public string payload (Item item) => $"{{\"x\":\"{item.gameObject.transform.position.x}\",\"y\":\"{item.gameObject.transform.position.y}\",\"item\":\"{item.type.ToString()}\",\"id\":\"{item.key}\",\"who\":\"{socket.session.UserId}\"}}";

	public void Start()
	{
        socket = FindObjectOfType<Socket>();
	}

	public void Pick (Item item) {
        if (hold) {
			// Drop
            socket.sendMessage (6, payload (hold));
            hold.gameObject.transform.parent = null;
        }
        item.gameObject.transform.parent = transform;
        item.gameObject.transform.localPosition = Vector3.zero;
        hold = item;

        item.GetComponent<Collider2D>().enabled = false;
        item.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        item.GetComponent<Rigidbody2D>().simulated = false;
    }

    void OnTriggerStay2D (Collider2D other) {
        var item = other.GetComponent<Item> ();
        if (!item) {
            return;
        }
        touch = item;
    }

	private void OnTriggerExit2D(Collider2D other)
	{
        var item = other.GetComponent<Item>();
        if (item == touch)
        {
            touch = null;
        }
    }

}