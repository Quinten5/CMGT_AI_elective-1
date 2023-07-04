using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MyAgent : Agent
{
    Rigidbody my_rbody;

    public Transform Target;
    public float forceMultiplier = 10f;
    public int n_steps_per_run = 3000;

    [Range(1, 50)]
    public int arena_extents = 1;
    private int epoch;


    
    // Start is called before the first frame update
    void Start()
    {
        my_rbody = GetComponent<Rigidbody>();  
        // public float arena_extents = 1f;
        GameObject floor_obj = transform.parent.Find("Floor").gameObject;
        floor_obj.transform.localScale = Vector3.one * arena_extents;


        Transform[] allWalls = transform.parent.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < allWalls.Length; i++)
        { 
            // GameObject wall_obj = transform.parent.Find("Wall").gameObject;
            // if (wall_obj)
            // {
            //     // print(wall_obj);
            //     Vector3 localpos = wall_obj.transform.localPosition;

            //     print(localpos);
            //     wall_obj.transform.localPosition = new Vector3(arena_extents * localpos.x, 
            //                                                     .08f, 
            //                                                     arena_extents * localpos.z);
            // }
            if (allWalls[i].name == "Wall")
            {
                Vector3 localpos = allWalls[i].transform.localPosition;

                allWalls[i].transform.localPosition = new Vector3(arena_extents * localpos.x, 
                                                                .08f, 
                                                                arena_extents * localpos.z);
                allWalls[i].transform.localScale = new Vector3(.15f, 
                                                                1f, 
                                                                arena_extents * 10f);
                // print(allWalls[i].)
            }
            // print(allWalls.name);
        }
    }
    

    


    public override void OnEpisodeBegin()
    {
        // var localScale = transform.parent.Find("Floor");

        



        //Array to hold all child obj
        // GameObject[] allChildren = new GameObject[transform.childCount];
        // }
        // {

        // }
        // // int i = 0;
        // // int n_walls = 4;
        // // for  
        // // foreach (var go in transform.parent.Find("Wall").gameObject)
        // {
        //     // GameObject[i] = go;
        //     print(go);
        //     float x_pos = 5 * arena_extents;
        //     float z_pos = 5 * arena_extents;
        //     //  = arena_extents;
        //     go.transform.localPosition = new Vector3(x_pos, .08f, z_pos);
        //     // print(go.transform.localPosition);
        // }

        print("OnEpisodeBegin()");
        epoch = 0;
        if (this.transform.localPosition.y < 0)
        {
            this.my_rbody.angularVelocity = Vector3.zero;
            this.my_rbody.velocity        = Vector3.zero;
            this.transform.localPosition  = new Vector3(0, 0, 0);
        }

        Target.localPosition = new Vector3(Random.value * 8 - 4, 
                                           0, 
                                           Random.value * 8 - 4) * arena_extents;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(my_rbody.velocity.x);
        sensor.AddObservation(my_rbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        epoch += 1;
        if (epoch > n_steps_per_run)
        {
            AddReward(-.5f);
            EndEpisode();
        }
        // Vector3 controlSignal = Vector3.zero;
        // controlSignal.x = actionBuffers.ContinuousActions[0] / 5;
        // controlSignal.z = actionBuffers.ContinuousActions[1] / 100;

        // Vector3 currentPos = this.transform.position;
        // currentPos += controlSignal;
        // this.transform.position += controlSignal; 
        this.transform.Rotate(0f, actionBuffers.ContinuousActions[1] * 2, 0f);
        // my_rbody.AddForce(controlSignal * forceMultiplier);

        my_rbody.velocity = transform.forward * Mathf.Max(0f, actionBuffers.ContinuousActions[0]) * forceMultiplier;

        // float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // if (distanceToTarget <= 1.42f)
        // {
        //     AddReward(10.0f);
        //     EndEpisode();
        // }
        // else if (this.transform.localPosition.y < 0)
        // {
        //     SetReward(0f);
        //     EndEpisode();
        // }
        // else
        float penalty_for_not_moving = 1 - Mathf.Max(0f, actionBuffers.ContinuousActions[0]);
        print("penalty: " + penalty_for_not_moving);
        AddReward(-.05f * penalty_for_not_moving);
        // AddReward(-0.001f);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            AddReward(-1f);
            // Vector3 spawnpos = Vector3(0f, .08f, 0f) 
            transform.localPosition = Vector3.zero;
            EndEpisode();
        }
        else if (collision.gameObject.tag == "Target")
        {
            AddReward(100f);
            EndEpisode();
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
