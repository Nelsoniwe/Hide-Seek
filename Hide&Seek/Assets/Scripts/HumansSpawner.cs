using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumansSpawner : MonoBehaviour
{
    public List<GameObject> Skins;

    private List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Hunter"));
        players.AddRange(GameObject.FindGameObjectsWithTag("HunterPlayer"));
        players.AddRange(GameObject.FindGameObjectsWithTag("Hider"));
        players.AddRange(GameObject.FindGameObjectsWithTag("HiderPlayer"));

        for (int i = 0; i < players.Count; i++)
        {
            GameObject stepCopy = Instantiate(Skins[Random.Range(0, Skins.Count)], new Vector3(players[i].transform.position.x, players[i].transform.position.y - 0.7f, players[i].transform.position.z), players[i].transform.rotation) as GameObject;
            stepCopy.transform.parent = players[i].transform;
        }
    }
}
