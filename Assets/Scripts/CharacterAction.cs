using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

    public Socket socket;
    public Transform hand;
    public Item hold;
    public Item touch = null;
    public const float LethalSpeed = 3.5f;
    public const float TemporaryIgnoreTime = 1;
    public List<Item> tempIgnore = new List<Item>();
    public Animator animator;
    public RuntimeAnimatorController normalController, holdingController;

    public Transform exilePosition;
    public Transform arrow;

    public void Start () {
        socket = FindObjectOfType<Socket> ();
        animator.runtimeAnimatorController = normalController;
    }


	public void Drop()
    {
        if (hold == null)
        {
            return;
        }
        StartCoroutine(TemporaryIgnore(hold));
        hold.gameObject.transform.parent = null;
        hold.GetComponent<Collider2D>().enabled = true;
        hold.isHeld = false;

        if (hold.type == Item.ItemType.Weapon)
        {
            hold.GetComponentInChildren<TrailRenderer>().enabled = true;
        }

        hold = null;
        touch = null;

        animator.runtimeAnimatorController = normalController;
    }

    public void Pick (Item item) {
        if (hold) {
            // Drop
            Drop();
        }
        item.gameObject.transform.parent = hand;
        item.gameObject.transform.localPosition = Vector3.zero;
        hold = item;

        item.GetComponent<Collider2D> ().enabled = false;
        item.isHeld = true;

		if (item.type == Item.ItemType.Weapon)
		{
            item.GetComponentInChildren<TrailRenderer>().enabled = false;
		}

        animator.runtimeAnimatorController = holdingController;
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