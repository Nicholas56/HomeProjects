using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalScript : OrganismScript
{
    public float speed;
    public GameObject target;
    NavMeshAgent nav;

    public float consumeRate;

    public float hunger, hungerMod;
    public float reproduce, reproduceMod;
    public float tire, tireMod;
    public float fear, fearMod;
    public float aggression, aggressionMod;

    public float range;

    int speedCh = 10;
    int consumeCh = 10;
    int hungerCh = 5;
    int reproduceCh = 5;
    int tireCh = 5;
    int fearCh = 5;
    int aggrCh = 5;
    int rangeCh = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
        energy = 50;
        if (transform.parent.tag != "World")
        {
            float previousSpeed = transform.parent.GetComponent<AnimalScript>().speed;
            float previousConsume = transform.parent.GetComponent<AnimalScript>().consumeRate;
            float previousHunger = transform.parent.GetComponent<AnimalScript>().hungerMod;
            float previousReproduce = transform.parent.GetComponent<AnimalScript>().reproduceMod;
            float previousTire = transform.parent.GetComponent<AnimalScript>().tireMod;
            float previousFear = transform.parent.GetComponent<AnimalScript>().fearMod;
            float previousAggr = transform.parent.GetComponent<AnimalScript>().aggressionMod;
            float previousRange = transform.parent.GetComponent<AnimalScript>().range;

            if (Random.Range(0, speedCh) == 0) { speed = previousSpeed + Random.Range(-1, 2); } else { speed = previousSpeed; }
            if (Random.Range(0, consumeCh) == 0) { consumeRate = previousConsume + Random.Range(-10, 11); } else { consumeRate = previousConsume; }
            if (Random.Range(0, hungerCh) == 0) { hungerMod = previousHunger + Random.Range(-1, 2); } else { hungerMod = previousHunger; }
            if (Random.Range(0, reproduceCh) == 0) { reproduceMod = previousReproduce + Random.Range(-1, 2); } else { reproduceMod = previousReproduce; }
            if (Random.Range(0, tireCh) == 0) { tireMod = previousTire + Random.Range(-1, 2); } else { tireMod = previousTire; }
            if (Random.Range(0, fearCh) == 0) { fearMod = previousFear + Random.Range(-1, 2); } else { fearMod = previousFear; }
            if (Random.Range(0, aggrCh) == 0) { aggressionMod = previousAggr + Random.Range(-1, 2); } else { aggressionMod = previousAggr; }
            if (Random.Range(0, rangeCh) == 0) { range = previousRange + Random.Range(-1, 2); } else { range = previousRange; }
        }

        nav.speed = speed;
        if (reproduceMod == 0) { reproduceMod = float.MinValue; }
        if (tireMod == 0) { tireMod = float.MinValue; }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case state.Feeding:
                FindFood();
                break;
            case state.Reproducing:
                Reproduce();
                break;
            case state.Resting:
                Idle();
                break;
            case state.Fleeing:
                Flee();
                break;
            case state.Fighting:
                Fight();
                break;
            case state.Exploring:
                Explore();
                break;

        }
        CheckState();
        if(energy == maxEnergy) { hunger = 0; }
    }

    Collider[] Search()
    {
        int oldLayer = gameObject.layer; // This variable now stored our original layer
        gameObject.layer = 2; // The game object will now ignore all forms of raycasting        
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        gameObject.layer = oldLayer;
        return hits;
    }

    void Explore()
    {
        if (!target)
        {
            Collider[] hitColliders = Search();
            foreach (Collider targ in hitColliders)
            {
                if (targ.tag == "Plant"||targ.tag == "Animal") { target = targ.gameObject; }
                if (target)
                {
                    nav.SetDestination(target.transform.position);
                    break;
                }
            }
        }
        if (target && Vector3.Distance(target.transform.position, transform.position) < range/3)
        {
            target = null;
        }
    }

    void Fight()
    {
        if (!target || target.tag != "Animal")
        {
            Collider[] hitColliders = Search();
            foreach (Collider targ in hitColliders)
            {
                if (targ.tag == "Animal") { target = targ.gameObject; }
                if (target)
                {
                    nav.SetDestination(target.transform.position);
                    break;
                }
            }
        }
        if (Vector3.Distance(target.transform.position, transform.position) < 1)
        {
            target.GetComponent<OrganismScript>().Consume(consumeRate * aggressionMod * Time.deltaTime);
            aggression -= aggressionMod * Time.deltaTime;
        }
        else { nav.SetDestination(target.transform.position); }
    }

    void Flee()
    {
        Collider[] hitColliders = Search();
        foreach (Collider targ in hitColliders)
        {
            if (Vector3.Distance(targ.transform.position, transform.position) > Vector3.Distance(target.transform.position, transform.position)) 
            {
                target = targ.gameObject;
            }
        }
        nav.SetDestination(target.transform.position);
        fear -= fearMod * Time.deltaTime;
    }

    void Idle()
    {
        target = null;
        energy += energyLoss * Time.deltaTime / 2;
        tire -= tireMod * Time.deltaTime * 2;
    }

    protected override void Reproduce()
    {
        if (!target || target.tag != "Animal")
        {
            Collider[] hitColliders = Search();
            foreach (Collider targ in hitColliders)
            {
                if (targ.tag == "Animal") { target = targ.gameObject; }
                if (target)
                {
                    nav.SetDestination(target.transform.position);
                    break;
                }
            }
        }
        if (target&&Vector3.Distance(target.transform.position, transform.position) < 1.5)
        {
            base.Reproduce();
            energy -= reproduceCost * reproduceCount;
            reproduce -= reproduceCost * reproduceMod/2;
        }
        else { nav.SetDestination(target.transform.position); }
    }
    
    void FindFood()
    {
        if (!target)
        {
            Collider[] hitColliders = Search();
            foreach (Collider targ in hitColliders)
            {
                if (targ.tag == "Plant") { target = targ.gameObject; }else if (targ.tag == "Animal") { target = targ.gameObject; }
                if (target) 
                {
                    nav.SetDestination(target.transform.position);
                    break; }
            }
        }
        if (target&&Vector3.Distance(target.transform.position, transform.position) < 1)
        {
            target.GetComponent<OrganismScript>().Consume(consumeRate);
            energy += consumeRate  * hungerMod;
            hunger -= hungerMod * 2;
        }
        else { nav.SetDestination(target.transform.position); }
    }

    void CheckState()
    {
        if (hunger > energy)
        {
            currentState = state.Feeding;
        }else if (reproduce > reproduceCost)
        {
            currentState = state.Reproducing;
        }else if (tire > maxEnergy)
        {
            currentState = state.Resting;
        }else if (fear > maxEnergy)
        {
            currentState = state.Fleeing;
        }else if (aggression > maxEnergy)
        {
            currentState = state.Fighting;
        }
        else { currentState = state.Exploring; }

        hunger += Time.deltaTime * hungerMod;
        reproduce += Time.deltaTime * reproduceMod;
        tire += Time.deltaTime * tireMod;
    }

    public override void Consume(float damage)
    {
        base.Consume(damage);
        aggression += aggressionMod;
        fear += fearMod;
    }
}
