using UnityEngine;

public class Fishing : MonoBehaviour
{
    private bool nearWater = false;
    public GameObject fishingIconPrefab;
    private GameObject currentFishingIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nearWater && Input.GetKeyDown(KeyCode.E))
        {
            StartFishing();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GrassEdge"))
        {
            nearWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GrassEdge"))
        {
            nearWater = false;
        }
    }

    void StartFishing()
    {
        Debug.Log("Fishing started!");
        // Remove previous icon if it exists
        if (currentFishingIcon != null)
            Destroy(currentFishingIcon);
        // Instantiate above the player
        Vector3 iconPosition = transform.position + Vector3.up * 1.5f; // Adjust as needed
        currentFishingIcon = Instantiate(fishingIconPrefab, iconPosition, Quaternion.identity);
        // Parent to player so it follows
        currentFishingIcon.transform.SetParent(transform);
        // Add your fishing logic here
    }
}
