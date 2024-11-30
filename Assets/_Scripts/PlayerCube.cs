using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    public LayerMask obstacleMask;

    [Header("Movement Variables")]
    public float HoMoveSpeed = 5f;
    public float VeMoveSpeed = 100f;

    private Rigidbody rb;
    private Vector3 mvmt;

    [Header("Ice Platform Variables")]
    public GameObject icePlatformPrefab;
    public GameObject indicatorPrefab;
    public float spawnDistance = 10f;

    [Header("Dynamic")]
    public GameObject indicator;
    public Vector3 playerPos;

    private Camera mainCam;

    [Header("Air Variables")]
    public float maxAir = 100f;
    public float airDecreaseRate = 10f;
    public float currentAir;
    private bool isUnderwater = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Get main camera for converting mouse position to world position
        mainCam = Camera.main;

        playerPos = transform.position;

        indicator = Instantiate(indicatorPrefab) as GameObject;
        indicator.transform.position = playerPos;

        indicator.SetActive(true);

        this.GetComponent<SphereCollider>().enabled = false;

        currentAir = maxAir;
    }

    // Update is called once per frame
    void Update()
    {
        mvmt.x = Input.GetAxisRaw("Horizontal");
        bool canJump = Mathf.Abs(rb.velocity.y) < 0.01f;

        // Display indicator
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -mainCam.transform.position.z;
        Vector3 mousePos3D = mainCam.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - playerPos;
        // Limit mouseDelta to radius of SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 placementPos = playerPos + mouseDelta;
        indicator.transform.position = placementPos;
        
        if (Input.GetMouseButtonDown(0) && CanPlacePlatform(placementPos, GetSize(icePlatformPrefab)))
        {
            Instantiate(icePlatformPrefab, placementPos, Quaternion.identity);
        }

        if (isUnderwater)
        {
            currentAir -= airDecreaseRate * Time.deltaTime;
            currentAir = Mathf.Max(currentAir, 0);
            
            if(currentAir <= 0)
            {
                Debug.Log("Player is out of air!");
                // Add code for when player is out of air.
                // Decrease health I guess?
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(
            rb.position.x + mvmt.x * HoMoveSpeed * Time.fixedDeltaTime,
            rb.position.y,
            rb.position.z);
        rb.MovePosition(newPos);
        
        // Update player position so that indicator logic follows position
        playerPos = rb.position;
        FollowCam.POI = this.gameObject;

        //Player Rotations
        if (mvmt.x < 0) // Rotate Left
        {
            transform.localEulerAngles = new Vector3(0, 75, 0);
        }
        else if (mvmt.x > 0) // Rotate Right
        {
            transform.localEulerAngles = new Vector3(0, -75, 0);
        }
        else if(mvmt.x == 0) // Face Forward
        {
            //transform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Water"))
        {
            isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Water"))
        {
            isUnderwater = false;
            currentAir = maxAir;
            Debug.Log("Player can breathe again!");
        }
    }

    public bool CanPlacePlatform(Vector3 pos, Vector3 platformSize)
    {
        Collider[] colliders = Physics.OverlapBox(pos, platformSize / 2, Quaternion.identity, obstacleMask);
        return colliders.Length == 0;
    }

    Vector3 GetSize(GameObject prefab)
    {
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size;
        }
        else
        {
            return Vector3.zero;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * VeMoveSpeed, ForceMode.Impulse);
    }
}
