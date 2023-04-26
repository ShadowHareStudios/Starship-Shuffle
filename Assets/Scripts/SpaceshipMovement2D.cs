using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement2D : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField]
    float maxSpeed = 5f;
    

    private float horizontalDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(maxSpeed * horizontalDir, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalDir = context.ReadValue<Vector2>().x;
    }
}
