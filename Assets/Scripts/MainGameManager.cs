using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spectatorObj;
    [SerializeField] private int enemyCount = 20;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float gameTime = 180f; //This is in seconds

    [SerializeField] private GameEventScriptable updateTimerEvent;
    [SerializeField] private GameEventScriptable updatePointsEvent;
    [SerializeField] private GameEventScriptable endGameEvent;
    [SerializeField] private GameEventScriptable sendItemTypeEvent;
    [SerializeField] private StringGameEvent updateTargetItem;
    [SerializeField] private StringListGameEvent updateTargetPosition;
    private List<GameObject> enemySpawns = new List<GameObject>();

    //GameVars
    private int score = 0;
    private GameObject player;
    private GameObject targetObject; //This is the object the player is assigned to get currently
    private bool gameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //Disable spectator
        spectatorObj.SetActive(false);

        //Find all the enemy spawns on the map and spawn all enemies at one of those random locations
        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn").ToList();

        for(int i = 0; i < enemyCount; i++)
        {
            int index = (int)Random.Range(0f, enemySpawns.Count - 1);
            int enemyIndex = (int)Random.Range(0f, enemyPrefabs.Length - 1);
            Instantiate(enemyPrefabs[enemyIndex], enemySpawns[index].transform.position, Quaternion.identity);
            
        }

        //Create the player
        player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);

        //Start end game coroutine
        StartCoroutine(EndGameAfterTime(gameTime));

        //Send out time update event
        updateTimerEvent.Invoke(gameTime);

        //Assign an item
        AssignNewItem();
    }

    public void AssignNewItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        int index = (int)Random.Range(0f, items.Length - 1);
        targetObject = items[index];

        updateTargetItem.Invoke(targetObject.GetComponent<ItemPickupScript>().associatedItem.pName);
        string[] args = { targetObject.transform.position.x.ToString(), targetObject.transform.position.y.ToString(), targetObject.transform.position.z.ToString() };
        updateTargetPosition.Invoke(args);
        sendItemTypeEvent.Invoke((float)targetObject.GetComponent<ItemPickupScript>().associatedItem.iType);
    }

    public void OnPlayerDeath()
    {
        EndgameStuff();
    }

    public void OnItemGrabbed()
    {
        score++;
        updatePointsEvent.Invoke(score);
        AssignNewItem();

    }

    public IEnumerator EndGameAfterTime(float t)
    {
        yield return new WaitForSeconds(t);

        EndgameStuff();
    }

    public void EndgameStuff()
    {
        gameOver = true;
        endGameEvent.Invoke(0f);
        Destroy(player);
        spectatorObj.SetActive(true);
    }
}
