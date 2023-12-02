using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megamanAfraid : MonoBehaviour
{

    public Transform player;
    public Transform megaman;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Zombie1").GetComponent<Transform>();
        megaman = GameObject.Find("megaman").GetComponent<Transform>();

        //the object megaman will look at
        target = megaman;
    }

    // Update is called once per frame
    void Update()
    {
        //find the direction
        Vector3 direction =new Vector3(player.position.x - megaman.position.x, 0, player.position.z - megaman.position.z);
        //place the target on the other side of MM.
        target.position = megaman.position + direction;
        megaman.LookAt(target);
    }
}
