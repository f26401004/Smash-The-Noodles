using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

    public Socket socket;
    public Item hold;
    public Item touch;
    public const float LethalSpeed = 3.5f;
    public const float TemporaryIgnoreTime = 1;
    public List<Item> tempIgnore = new List<Item>();

    public void Start () {
        socket = FindObjectOfType<Socket> ();
    }

    public void Pick (Item item) {
        if (hold) {
            // Drop
            hold.gameObject.transform.parent = null;
        }
        item.gameObject.transform.parent = transform;
        item.gameObject.transform.localPosition = Vector3.zero;
        hold = item;

        item.GetComponent<Collider2D> ().enabled = false;
        item.isHeld = true;
    }

	protected IEnumerator TemporaryIgnore(Item item)
	{
        tempIgnore.Add(item);
        yield return new WaitForSeconds(TemporaryIgnoreTime);
        tempIgnore.Remove(item);
    }

    void OnTriggerStay2D (Collider2D other) {
        var item = other.GetComponent<Item> ();
        if (!item) {
            return;
        }
		// A too-fast weapon?
		if (item.type == Item.ItemType.Weapon && item.velocity.magnitude > LethalSpeed && !tempIgnore.Contains(item))
		{
            FindObjectOfType<Control>().LetDie(this as PlayerAction);
		}
        touch = item;
    }

    void OnTriggerExit2D (Collider2D other) {
        var item = other.GetComponent<Item> ();
        if (item && item == touch) {
            touch = null;
        }
    }

}