using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float speed = 10;
    
    [SerializeField] bool isRunning = false;
    [Range(1f, 10f)] public float runSpeedMultiplier = 2f;

    Rigidbody2D rb;
    float horizontalValue;
    // Init early, runs when object is created  
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal"); // return -1 or 0 or 1

        // if LShift hold then isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
    }

    // Runs at fixed rate (once every 0.02s), handle physics  
    void FixedUpdate()
    {
        Move(horizontalValue);
    }

    void Move(float dir)
    {   
        float xVal = dir * speed * Time.deltaTime;

        if (isRunning) xVal *=  runSpeedMultiplier;

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        // Turn left or right
        Vector3 scale = transform.localScale;
        if (dir > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (dir < 0)
            scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
