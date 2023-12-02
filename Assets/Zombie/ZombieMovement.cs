using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 100000f;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        body.centerOfMass = (new Vector3(0, 0, 0));
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        body.AddForce(new Vector3(xMove, body.velocity.y, zMove) * speed);      
    }
}
