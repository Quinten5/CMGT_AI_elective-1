using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance is null)
                Debug.Log("Game Manager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public GameObject pickup_good;
    public GameObject pickup_bad;
   
    [Range(1, 50)]
    public int arena_extents = 1;

    [Range(0, 50)]
    public int n_pickups_good = 15;
    
    [Range(0, 50)]
    public int n_pickups_bad  = 15;

    private List<GameObject> good_pellets = new List<GameObject>();
    private List<GameObject> bad_pellets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        create_arena();
        spawn_pellets();
    }

    void create_arena()
    {
        GameObject floor_obj = transform.parent.Find("Floor").gameObject;
        floor_obj.transform.localScale = Vector3.one * arena_extents;


        Transform[] allWalls = transform.parent.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < allWalls.Length; i++)
        { 
            if (allWalls[i].name == "Wall")
            {
                Vector3 localpos = allWalls[i].transform.localPosition;

                allWalls[i].transform.localPosition = new Vector3(arena_extents * localpos.x, 
                                                                .08f, 
                                                                arena_extents * localpos.z);
                allWalls[i].transform.localScale = new Vector3(.15f, 
                                                                1f, 
                                                                arena_extents * 10f);
            }
        }
    }

    void spawn_pellet(GameObject pickup, List<GameObject> pickup_list)
    {
        float x, y, z;

        x = Random.Range(-4.5f * arena_extents, 4.5f * arena_extents);
        y = 0.5f;
        z = Random.Range(-4.5f * arena_extents, 4.5f * arena_extents);

        Vector3 new_pos = new Vector3(x, y, z);
        new_pos += transform.parent.transform.position;

        GameObject pellet = (GameObject)Instantiate(pickup, new_pos, Quaternion.identity);

        pickup_list.Add(pellet);
    }

    public void spawn_pellets()
    {
        destroy_old_pellets();

        for (int idx = 0; idx < n_pickups_good; idx++)
        {
            spawn_pellet(pickup_good, good_pellets);
        }
        for (int idx = 0; idx < n_pickups_bad; idx++)
        {
            spawn_pellet(pickup_bad, bad_pellets);
        }
    }

    void destroy_old_pellets()
    {
        foreach(GameObject obj in good_pellets)
            Destroy(obj);
        foreach(GameObject obj in bad_pellets)
            Destroy(obj);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
