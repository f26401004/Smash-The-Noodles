using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public CharacterMovement p1, p2;
    private PlayerAction p1a, p2a;
    public Transform spawnPosition;
    public Transform[] itemSpawns;
    public Item[] items;

	private void Start()
	{
        p1a = p1.GetComponent<PlayerAction>();
        p2a = p2.GetComponent<PlayerAction>();

        StartCoroutine(ItemSpawn());
    }

	public void LetDie(PlayerAction player)
	{
        // Out for 5 secs
        StartCoroutine(DieAndRespawn(player.gameObject));

	}

	private IEnumerator ItemSpawn()
	{
		while (true)
		{
			if (FindObjectsOfType<Item>().Length <= 10)
			{
                Vector3 spawn = itemSpawns[Random.Range(0, itemSpawns.Length)].position;
                Item item = items[Random.Range(0, items.Length)];

                spawn += Random.insideUnitSphere * 0.6f;

                Item newItem = Instantiate(item);
                newItem.transform.position = spawn;
            }
            

            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
		}
	}

	private IEnumerator DieAndRespawn(GameObject player)
	{
		if (player.GetComponent<PlayerAction>().hold)
		{
            player.GetComponent<PlayerAction>().Drop();
        }
        player.SetActive(false);
        player.transform.position = spawnPosition.position;
        yield return new WaitForSeconds(3);
        player.transform.position = spawnPosition.position;
        player.SetActive(true);
    }

	void Update()
    {
        Vector2 p1dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 p2dir = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        p1.WalkTowards(p1dir);
        p2.WalkTowards(p2dir);

        if (Input.GetKeyDown(KeyCode.W)) p1.Jump();
        if (Input.GetKeyDown(KeyCode.UpArrow)) p2.Jump();

        if (Input.GetKeyDown(KeyCode.E) && p1a.touch)
        {
            p1a.Pick(p1a.touch);
        }
        if (Input.GetKeyDown(KeyCode.R) && p1a.hold)
        {
            p1a.Drop();
        }
		if (Input.GetKeyDown(KeyCode.F) && p1a.hold)
		{
            p1a.Throw();
		}

        // p2
        if (Input.GetKeyDown(KeyCode.P) && p2a.touch)
        {
            p2a.Pick(p2a.touch);
        }
        if (Input.GetKeyDown(KeyCode.O) && p2a.hold)
        {
            p2a.Drop();
        }
        if (Input.GetKeyDown(KeyCode.L) && p2a.hold)
        {
            p2a.Throw();
        }
    }
}
