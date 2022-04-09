using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentLogic : MonoBehaviour
{
    private GameObject targetItem;
    private NavMeshAgent agent;
    [SerializeField] private AgentData aData;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SelectTargetItem();

        StartCoroutine(CheckSurroundings(aData.checkFrequency));
    }

    // Update is called once per frame
    void Update()
    {  
    }

    private void SelectTargetItem()
    {
        //Get list of items
        GameObject[] availableItems = GameObject.FindGameObjectsWithTag("Item");
        //Select one randomly
        int index = (int) Random.Range(0f, availableItems.Length - 1f);
        targetItem = availableItems[index];

        //Set the agent's destination to the location
        agent.destination = targetItem.transform.position;
    }

    private IEnumerator CheckSurroundings(float time)
    {
        yield return new WaitForSeconds(time);




        StartCoroutine(CheckSurroundings(aData.checkFrequency));
    }
}
