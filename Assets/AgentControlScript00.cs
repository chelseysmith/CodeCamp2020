using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AgentControlScript00 : MonoBehaviour
{
    public NavMeshAgent Agent;
    public int BoardWidth;

    void Update()
    {
        var burnables = GameObject.FindGameObjectsWithTag("Burnable");
        var burnableEntityStats = new List<EntityStats>();
        var random = new System.Random();

        foreach (var burnable in burnables)
            burnableEntityStats.Add(burnable.GetComponent<EntityStats>());

        var thoseOnFire = burnableEntityStats?
                .Where(b => b.Fire != Enums.Fire.na && b.enabled && !b.Dead)
                .ToList();

        var availableSpaces = burnableEntityStats
            .Where(b => b.Fire == Enums.Fire.na || b.Dead);

        if (thoseOnFire
                .Any(b => Mathf.Abs((float)(b.transform.position.x - transform.position.x)) <= 2) && Mathf.Abs(transform.position.x + 1) <= BoardWidth)
        {
            Agent.SetDestination(availableSpaces.ElementAt(random.Next(availableSpaces.Count() - 1)).transform.position);
        }

        if (thoseOnFire
               .Any(b => Mathf.Abs((float)(transform.position.x - b.transform.position.x)) <= 2) && Mathf.Abs(transform.position.x - 1) <= BoardWidth)
        {
            Agent.SetDestination(availableSpaces.ElementAt(random.Next(availableSpaces.Count() - 1)).transform.position);
        }

        if (thoseOnFire
               .Any(b => Mathf.Abs((float)(b.transform.position.z - transform.position.z)) <= 2) && Mathf.Abs(transform.position.z + 1) <= BoardWidth)
        {
            Agent.SetDestination(availableSpaces.ElementAt(random.Next(availableSpaces.Count() - 1)).transform.position);
        }

        if (thoseOnFire
               .Any(b => Mathf.Abs((float)(transform.position.z - b.transform.position.z)) <= 2) && Mathf.Abs(transform.position.z - 1) <= BoardWidth)
        {
            Agent.SetDestination(availableSpaces.ElementAt(random.Next(availableSpaces.Count() - 1)).transform.position);
        }
    }
}
