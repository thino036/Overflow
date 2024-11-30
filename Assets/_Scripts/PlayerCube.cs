using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI Slider support

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
    public float airIncreaseRate = 5f;
    public float currentAir;
    private bool isUnderwater = false;
    public Slider oxygenSlider; // Slider for oxygen display

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

        // Initialize oxygen slider
        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = maxAir;
            oxygenSlider.value = currentAir;
        }
    }

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

        // Handle oxygen decrease or increase
        if (isUnderwater)
        {
            currentAir -= airDecreaseRate * Time.deltaTime;
            currentAir = Mathf.Max(currentAir, 0); // Clamp to 0

            if (oxygenSlider != null)
            {
                oxygenSlider.value = currentAir; // Update slider
            }

            if (currentAir <= 0)
            {
                Debug.Log("Player is out of air!");
                // Add code for when the player is out of air (e.g., decrease health)
            }
        }
        else
        {
            // Replenish oxygen when out of water
            currentAir += airIncreaseRate * Time.deltaTime;
            currentAir = Mathf.Min(currentAir, maxAir); // Clamp to maxAir

            if (oxygenSlider != null)
            {
                oxygenSlider.value = currentAir; // Update slider
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
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Entered Trigger: {collision.name}");

        if (collision.CompareTag("Water"))
        {
            isUnderwater = true;
            Debug.Log("Player is underwater.");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log($"Exited Trigger: {collision.name}");

        if (collision.CompareTag("Water"))
        {
            isUnderwater = false;
            Debug.Log("Player exited water.");
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
