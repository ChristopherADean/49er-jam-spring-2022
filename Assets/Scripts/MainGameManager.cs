using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int enemyCount = 20;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float gameTime = 180f; //This is in seconds
    private List<GameObject> enemySpawns = new List<GameObject>();

    //GameVars
    private GameObject targetObject; //This is the object the player is assigned to get currently
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find all the enemy spawns on the map and spawn all enemies at one of those random locations
        enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn").ToList();

        for(int i = 0; i < enemyCount; i++)
        {
            int index = (int)Random.Range(0f, enemySpawns.Count - 1);
            int enemyIndex = (int)Random.Range(0f, enemyPrefabs.Length - 1);
            Instantiate(enemyPrefabs[enemyIndex], enemySpawns[index].transform.position, Quaternion.identity);
            
        }

        //Create the player
        Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);

        //Start end game coroutine
        StartCoroutine(EndGameAfterTime(gameTime));
    }

    public void AssignNewItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        int index = (int)Random.Range(0f, items.Length - 1);
        targetObject = items[index];
    }

    public void OnPlayerDeath()
    {
        EndgameStuff();
    }

    public void OnItemGrabbed()
    {

    }

    public IEnumerator EndGameAfterTime(float t)
    {
        yield return new WaitForSeconds(t);

        EndgameStuff();
    }

    public void EndgameStuff()
    {
        gameOver = true;
    }
}
