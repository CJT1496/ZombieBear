using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBear : MonoBehaviour {
    public GameObject player;
    public AudioClip[] footsounds;
    public Transform eyes;
    public AudioSource growl;

    private UnityEngine.AI.NavMeshAgent nav;
    private UnityEngine.AudioSource sound;
    private UnityEngine.Animator anim;
    private string state = "idle";
    private bool alive = true;

    
	// Use this for initialization
	void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        sound = GetComponent<UnityEngine.AudioSource>();
        anim = GetComponent<UnityEngine.Animator>();
        nav.speed = 1.2f;
        anim.speed = 1.2f;
	}
	
    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    //check if we can see player//
    public void checkSight()
    {
        if (alive)
        {
            RaycastHit rayHit;
            if(Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                print("hit " + rayHit.collider.gameObject.name);

            if(rayHit.collider.gameObject.name == "player")
                {
                    if(state != "kill")
                    {
                        state = "chase";
                        nav.speed = 3.5f;
                        anim.speed = 3.5f;
                        growl.pitch = 1.2f;
                        growl.Play();
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        Debug.DrawLine(eyes.position, player.transform.position, Color.green);

        if (alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            if(state == "idle")
            {
                //pick a random place to walk to//
                Vector3 randomPos = Random.insideUnitSphere * 20f;
                UnityEngine.AI.NavMeshHit navHit;
                UnityEngine.AI.NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, UnityEngine.AI.NavMesh.AllAreas);
                nav.SetDestination(player.transform.position);
                state = "walk";
            }
            //walk//
            if (state == "walk")
            {
                if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "idle";
                }
            }
          
            if(state == "chase")
            {
                nav.destination = player.transform.position;
            }

        }

       
      // nav.SetDestination(player.transform.position);
		
	}
}
