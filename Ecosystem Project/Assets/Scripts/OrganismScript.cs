using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismScript : MonoBehaviour
{
    public float age;
    public float lifeSpan;
    public float lifeVariance;

    public enum state { Feeding, Resting, Reproducing, Fighting, Fleeing, Exploring}
    public state currentState;

    public float energy;
    public float maxEnergy;
    public float energyLoss;

    public float identifier;
    public int generation;
    public float floorSense;

    public float reproduceThreshhold;
    public float reproduceCost;
    public int reproduceCount;
    public float reproduceDistance;

    //one in __ chance of mutation
    int spanCh = 5;
    int varianceCh = 20;
    int maxCh = 50;
    int lossCh = 50;
    int threshCh = 20;
    int costCh = 20;
    int distCh = 10;
    int countCh = 50;

    protected virtual void Start()
    {
        age = 0;
        if (transform.parent.tag!="World")
        {
            float previousSpan = transform.parent.GetComponent<OrganismScript>().lifeSpan;
            float previousVariance = transform.parent.GetComponent<OrganismScript>().lifeVariance;
            float previousMax = transform.parent.GetComponent<OrganismScript>().maxEnergy;
            float previousLoss = transform.parent.GetComponent<OrganismScript>().energyLoss;
            float previousID = transform.parent.GetComponent<OrganismScript>().identifier;
            float previousThreshhold = transform.parent.GetComponent<OrganismScript>().reproduceThreshhold;
            float previousCost = transform.parent.GetComponent<OrganismScript>().reproduceCost;
            int previousCount = transform.parent.GetComponent<OrganismScript>().reproduceCount;
            float previousDistance = transform.parent.GetComponent<OrganismScript>().reproduceDistance;

            if (Random.Range(0, spanCh) == 0) { lifeSpan = previousSpan + Random.Range(-5, 6); } else { lifeSpan = previousSpan; }
            if (Random.Range(0, varianceCh) == 0) { lifeVariance = previousVariance + Random.Range(-5, 6); } else { lifeVariance = previousVariance; }
            if (Random.Range(0, maxCh) == 0) { maxEnergy = previousMax + Random.Range(-5, 6); } else { maxEnergy = previousMax; }
            if (Random.Range(0, lossCh) == 0) { energyLoss = previousLoss + Random.Range(-1, 2); } else { energyLoss = previousLoss; }
            if (Random.Range(0, threshCh) == 0) { reproduceThreshhold = previousThreshhold + Random.Range(-5, 6); } else { reproduceThreshhold = previousThreshhold; }
            if (Random.Range(0, costCh) == 0) { reproduceCost = previousCost + Random.Range(-5, 6); } else { reproduceCost = previousCost; }
            if (Random.Range(0, distCh) == 0) { reproduceDistance = previousDistance + Random.Range(-1, 2); } else { reproduceDistance = previousDistance; }
            if (Random.Range(0, countCh) == 0) { reproduceCount = previousCount + Random.Range(-1, 2); }

            identifier = previousID + (previousLoss + maxEnergy + lifeSpan + lifeVariance + previousThreshhold + previousCost + reproduceDistance + previousCount) -
                (previousSpan + previousVariance + previousMax + energyLoss + reproduceThreshhold + reproduceCost + previousDistance + reproduceCount);
            generation = transform.parent.GetComponent<OrganismScript>().generation+1;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, floorSense);
        bool allow=false;
        foreach (Collider search in hitColliders) { if (search.tag == "Floor") { allow = true; } }
        if (!allow) { Destroy(gameObject); }
    }
    protected virtual void Update()
    {
        energy -= energyLoss*Time.deltaTime;
        if (energy <= 0) { Destroy(gameObject); }

        age += Time.deltaTime;
        if (age > lifeSpan + lifeVariance)
        {
            Destroy(gameObject);
        }
        else if (age > 0.2f&&age<1) { transform.SetParent(GameObject.FindGameObjectWithTag("World").transform); }
        if (energy > maxEnergy) { energy = maxEnergy; }
    }

    protected virtual void Reproduce()
    {
        GameObject toSpawn = gameObject;
        for (int i = 0; i < reproduceCount; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-reproduceDistance, reproduceDistance), transform.position.y, Random.Range(-reproduceDistance, reproduceDistance));
            GameObject newSpawn = Instantiate(toSpawn, spawnPoint, Quaternion.identity, transform);
        }
    }

    public virtual void Consume(float damage)
    {
        energy -= damage;
    }
}
