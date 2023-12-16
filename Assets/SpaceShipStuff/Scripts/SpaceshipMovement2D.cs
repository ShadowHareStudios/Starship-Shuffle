using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]


public class SpaceshipMovement2D : MonoBehaviour
{
    //Rotation Vars
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float reAlignSpeedMultiplier;
    [SerializeField] float maximumX, minimumX;
    [SerializeField] float maximumY, minimumY;
    [SerializeField] float maximumRotation, minimumRotation;
    public float newAlignment;
     float rotationInput;
     float horizontalDir;
     float verticalDir;

    
    public void Move(InputAction.CallbackContext context)
    {
        horizontalDir = context.ReadValue<Vector2>().x;
        verticalDir = context.ReadValue<Vector2>().y;
    }
    
    
   
        enum MoveType
        {
            FORCE,
            VELOCITY
        }

        delegate void Accelerate(Vector3 v);

        Accelerate accelStyle;
        public bool debugVelocityTimeScale;
        [SerializeField] float cameraZoomDelay;
        bool isMoving;

        [Header("Move Settings")]
        [Tooltip("Factor of max velocity")] public float accelFactor;
        public float maxVel;
        [SerializeField] MoveType moveType = MoveType.VELOCITY;

        Rigidbody2D rb2d;
        float axisX;
        float axisY;
        float timeSinceKeyRelease;

        public bool IsMoving { get => isMoving; }

        [SerializeField] float playerSlowDown;

        // Start is called before the first frame update
        void Start()
        {
        
            rb2d = GetComponent<Rigidbody2D>();

            if (moveType == MoveType.VELOCITY) accelStyle = AccelerateUsingVelocity;

            if (maxVel <= 0f) maxVel = 25f;
            if (cameraZoomDelay <= 0f) cameraZoomDelay = 0.25f;
        }


        // Update is called once per frames
        void Update()
        {
            if (maxVel < 1f)
            {
                maxVel = 0f;
                accelFactor = 0f;
            }

        axisX = horizontalDir;
        axisY = verticalDir;
        rotationInput += -horizontalDir * rotationSpeed * Time.deltaTime;

           Vector2 movementDirection = new Vector2(axisX, axisY);
            movementDirection.Normalize();

            float accelForce = maxVel / accelFactor;

            accelStyle(movementDirection);
        
            // If player isnt pressing any movement keys
            if (axisX == 0f && axisY == 0f)
            {
                if (timeSinceKeyRelease > cameraZoomDelay)
                {
                    isMoving = false;
                }

                accelStyle(-rb2d.velocity.normalized);

                if (rb2d.velocity.magnitude < 0.2f) rb2d.velocity = Vector2.zero;
                timeSinceKeyRelease += Time.unscaledDeltaTime;
            }
            else
            {
                if (timeSinceKeyRelease > cameraZoomDelay)
                {
                    isMoving = true;
                }
                timeSinceKeyRelease = 0f;
            }

            // Clamp velocity on player
            if (rb2d.velocity.magnitude > maxVel)
            {
                rb2d.velocity -= rb2d.velocity.normalized * maxVel * Time.deltaTime * playerSlowDown;
            }

            //Clamp position of player
            Vector3 posClamp = transform.position;
            posClamp.x = Mathf.Clamp(posClamp.x, minimumX, maximumX);
            posClamp.y = Mathf.Clamp(posClamp.y, minimumY, maximumY);
                
            transform.position = posClamp;
        
        //Screen Edge Wrapping
          if (transform.position.x == minimumX)
            {
            
            ExitScreenBoundsL();
            }

          if (transform.position.x == maximumX)
            {
            
            ExitScreenBoundsR();
            }

          // vertical restriction Velocity reset
        if(transform.position.y == minimumY)
            {
            rb2d.velocity = -rb2d.velocity.normalized;
            }
        if (transform.position.y == maximumY)
            {
            rb2d.velocity = -rb2d.velocity.normalized;
            }


        if (debugVelocityTimeScale)
                Debug.Log("Velocity: " + rb2d.velocity.magnitude + "Timescale: " + Time.timeScale);

        // clamp rotation and assign alignment
        rotationInput = Mathf.Clamp(rotationInput, minimumRotation, maximumRotation);
       
        newAlignment = rotationInput;

      

    }

        // Returns a float with reversed time scaling
        public float TimeFactoredFloat(float f)
        {
            return f + f / Time.timeScale;
        }
        void ExitScreenBoundsL()
        {
            transform.position = new Vector3((maximumX) - 1 , transform.position.y, transform.position.z);
        }
        void ExitScreenBoundsR()
        {
            transform.position = new Vector3((minimumX) + 1, transform.position.y, transform.position.z);
        }

        void AccelerateUsingVelocity(Vector3 direction_)
        {
            rb2d.velocity += (Vector2)direction_ * (accelFactor * Time.unscaledDeltaTime);
        }
    void FixedUpdate()
    {


     

        //Rotate with Movement
        if (isMoving)
        {
            newAlignment = rotationInput - transform.rotation.z;


            transform.eulerAngles = new Vector3(0f, 0f, newAlignment);
           

            
        }
        else
        {
            transform.Rotate(Vector3.forward, rotationSpeed * reAlignSpeedMultiplier * -transform.rotation.z);
            rotationInput = horizontalDir * rotationSpeed * Time.deltaTime;
        }
        

    }


}
