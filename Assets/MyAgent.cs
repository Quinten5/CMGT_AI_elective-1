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

    // [Range(1, 50)]
    // public int arena_extents = 1;
    private int epoch;

    void Start()
    {
        my_rbody = GetComponent<Rigidbody>();  
       
    }



    
    public override void OnEpisodeBegin()
    {
        epoch = 0;
        if (this.transform.localPosition.y < 0)
        {
            this.my_rbody.angularVelocity = Vector3.zero;
            this.my_rbody.velocity        = Vector3.zero;
            this.transform.localPosition  = new Vector3(0, 0, 0);
        }

        Target.localPosition = new Vector3((Random.value * 8 - 4) * GameManager.Instance.arena_extents, 
                                           0.54f, 
                                           (Random.value * 8 - 4) * GameManager.Instance.arena_extents);
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
        this.transform.Rotate(0f, actionBuffers.ContinuousActions[1] * 2, 0f);

        my_rbody.velocity = transform.forward * Mathf.Max(0f, actionBuffers.ContinuousActions[0]) * forceMultiplier;

        float penalty_for_not_moving = 1 - Mathf.Max(0f, actionBuffers.ContinuousActions[0]);
        // print("penalty: " + penalty_for_not_moving);
        AddReward(-.05f * penalty_for_not_moving);
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "GoodPellet":
                AddReward(20f);
                Destroy(collision.gameObject);
                break;
            case "BadPellet":
                AddReward(-20f);
                Destroy(collision.gameObject);
                break;
            case "Wall":
                AddReward(-50f);
                transform.localPosition = Vector3.zero;
                GameManager.Instance.spawn_pellets();
                EndEpisode();
                break;
            case "Target":
                AddReward(100f);
                GameManager.Instance.spawn_pellets();
                EndEpisode();
                break;
        }        
    }
}
