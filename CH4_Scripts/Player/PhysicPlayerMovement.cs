//See this image for set up information: http://i.imgur.com/pzfuzLn.png
//Don't forget to freeze the rotation on your rigidbody!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicPlayerMovement : MonoBehaviour
{
    Camera cam;
    Rigidbody body;

    bool jumpPressed;

    //Collision State
    List<Collider> colliding = new List<Collider>();
    Collider groundCollider = new Collider();
    Rigidbody groundRigidbody = new Rigidbody();
    Vector3 groundNormal = Vector3.down;
    Vector3 groundContactPoint = Vector3.zero;
    Vector3 groundVelocity = Vector3.zero;

    //Raycast to get the object pointing
    Ray ray;
    RaycastHit hit;
    string objectName = "";



    //Initialize variables
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Movement Handling
    void FixedUpdate()
    {
        //Record the world-space walking movement
        Vector3 movement = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        //If we're currently contacting a wall/ground like object
        if (groundCollider != null && Vector3.Dot(Vector3.up, groundNormal) > -0.3f)
        {
            //Subtract the ground's velocity
            if (groundRigidbody != null && groundRigidbody.isKinematic)
            {
                body.velocity -= groundVelocity;
            }

            //Walking along the ground movement
            if (Vector3.Dot(Vector3.up, groundNormal) > 0.5f)
            {
                if (movement != Vector3.zero)
                {
                    Vector2 XYVel = new Vector2(body.velocity.x, body.velocity.z);
                    XYVel = Mathf.Clamp(XYVel.magnitude, 0f, 3f) * XYVel.normalized;
                    body.velocity = new Vector3(XYVel.x, body.velocity.y, XYVel.y);
                }
                else
                {
                    body.velocity = new Vector3(body.velocity.x * 0.8f, body.velocity.y, body.velocity.z * 0.8f);
                }
                body.velocity += movement * 10f; // ***** 1f *****
            }

            //Handle jumping
            //若角色在地面上，且按下跳躍鍵，則給予角色一個向上的力
            //velocity.y <= 0.2f 代表角色在地面上(沒有跳躍的情況下）
            if (jumpPressed && body.velocity.y <= 0.2f) { body.velocity += Vector3.Slerp(Vector3.up, groundNormal, 0.2f) * 6f; } 

            //Draw some debug info
            Debug.DrawLine(groundContactPoint, groundContactPoint + groundNormal, Color.blue, 2f);

            //Add back the ground's velocity
            if (groundRigidbody != null && groundRigidbody.isKinematic)
            {
                groundVelocity = groundRigidbody.GetPointVelocity(groundContactPoint);
                body.velocity += groundVelocity;
            }
        }
        else
        {
            body.velocity += movement * 0.1f;// ***** 0.1f ***** //1->su
            groundVelocity = Vector3.zero;
        }

        groundNormal = Vector3.down;
        groundCollider = null;
        groundRigidbody = null;
        groundContactPoint = (transform.position - Vector3.down * -0.5f);
        jumpPressed = false;
    }

    //Per-Frame Updates
    void Update()
    {
        //Record whether the jump key was hit this frame
        //NOTE: Must be done in Update, not FixedUpdate
        jumpPressed = jumpPressed ? jumpPressed : Input.GetButtonDown("Jump");

        //Rotate the player
        transform.Rotate(0, (Input.GetAxis("Mouse X")) * 2f, 0);

        //Rotate the camera rig and prevent it from penetrating the environment
        cam.transform.parent.localRotation *= Quaternion.Euler(-Input.GetAxis("Mouse Y") * 2f, 0, 0);

        //以下代碼會在角色相機角度太低時，將相機距離拉遠，變成第三人稱視角
        //cam.transform.localPosition = (Physics.SphereCast(cam.transform.parent.position, cam.nearClipPlane * 0.5f, -cam.transform.parent.forward, out hit, 3f) ? (Vector3.back * hit.distance) : Vector3.back * 3f);

        //Get the object pointing
        RaycastObject();
    }

    //Get the object pointing
    void RaycastObject(){
        ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        
        if( Physics.Raycast( ray, out hit, 100 ) )
        {
            if(objectName != hit.transform.gameObject.name){
                objectName = hit.transform.gameObject.name;
                Debug.Log( objectName );
            }
        }
    }

    //Ground Collision Handling
    void OnCollisionEnter(Collision collision)
    {
        colliding.Add(collision.collider);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.impulse.magnitude > float.Epsilon)
        {
            if (!colliding.Contains(collision.collider))
            {
                colliding.Add(collision.collider);
            }

            //Record ground telemetry
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Vector3.Dot(Vector3.up, collision.contacts[i].normal) > Vector3.Dot(Vector3.up, groundNormal))
                {
                    groundNormal = collision.contacts[i].normal;
                    groundCollider = collision.collider;
                    groundContactPoint = collision.contacts[i].point;
                    groundRigidbody = collision.rigidbody;
                    if (groundRigidbody != null && groundVelocity == Vector3.zero)
                    {
                        groundVelocity = groundRigidbody.GetPointVelocity(groundContactPoint);
                    }
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        colliding.Remove(collision.collider);
        if (colliding.Count == 0)
        {
            groundVelocity = Vector3.zero;
        }
    }
}