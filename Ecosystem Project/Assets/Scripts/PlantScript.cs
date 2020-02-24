using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : OrganismScript
{
    public float energyGather;
    public float overLap;

    
    int gatherCh = 50;
    int overCh = 20;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentState = state.Feeding;
        energy = 25;
        if (transform.parent.tag != "World")
        {
            
            float previousGather = transform.parent.GetComponent<PlantScript>().energyGather;
            float previousOverlap = transform.parent.GetComponent<PlantScript>().overLap;

            
            if (Random.Range(0, gatherCh) == 0) { energyGather = previousGather + Random.Range(-1, 2); } else { energyGather = previousGather; }
            if (Random.Range(0, overCh) == 0) { overLap = previousOverlap + Random.Range(-1, 2); } else { overLap = previousOverlap; }

            if (overLap == 0) { overLap = 1; }

            identifier += (energyGather + previousOverlap) - 
                (previousGather + overLap);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case state.Feeding:
                energy += energyGather*Time.deltaTime;
                if (energy > reproduceThreshhold) { currentState = state.Reproducing; }
                break;
            case state.Reproducing:
                energy -= reproduceCost * reproduceCount;
                Reproduce();
                currentState = state.Feeding;
                break;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overLap);
        foreach(Collider target in hitColliders)
        {
            energy-=Time.deltaTime;
        }
    }

    protected override void Reproduce()
    {
        base.Reproduce();
    }
}
