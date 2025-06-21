using UnityEngine;
using TMPro;

public class Fishing : MonoBehaviour
{
    private bool nearWater = false;
    public GameObject fishingIconPrefab;
    private GameObject currentFishingIcon;
    public GameObject fishingRodPrefab;
    private GameObject currentFishingRod;
    public TextMeshProUGUI scoreText;
    private int score = 0;

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

        // Remove previous icons if they exist
        if (currentFishingIcon != null)
            Destroy(currentFishingIcon);
        if (currentFishingRod != null)
            Destroy(currentFishingRod);

        // Stop player movement
        MoveCharacter moveChar = GetComponent<MoveCharacter>();
        if (moveChar != null)
            moveChar.isFishing = true;

        // Handle fishing rod instantiation and flipping
        HandleFishingRod();

        // Start coroutine for fishing sequence
        StartCoroutine(FishingSequence());
    }

        private System.Collections.IEnumerator FishingSequence()
    {
        yield return new WaitForSeconds(1.5f);

        // Remove fishing rod
        if (currentFishingRod != null)
            Destroy(currentFishingRod);

        // Instantiate fish icon above the player
        Vector3 iconPosition = transform.position + Vector3.up * 1.5f;
        currentFishingIcon = Instantiate(fishingIconPrefab, iconPosition, fishingIconPrefab.transform.rotation);
        currentFishingIcon.transform.SetParent(transform);
        Destroy(currentFishingIcon, 0.5f);

        // Increase score and update text
        score++;
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // Allow player movement again
        MoveCharacter moveChar = GetComponent<MoveCharacter>();
        if (moveChar != null)
            moveChar.isFishing = false;
    }

    // Handles fishing rod instantiation, positioning, and flipping
    private void HandleFishingRod()
    {
        // Instantiate fishing rod at the player's position, respecting prefab's local offset
        currentFishingRod = Instantiate(fishingRodPrefab, transform.position, fishingRodPrefab.transform.rotation);
        currentFishingRod.transform.SetParent(transform);

        // Mirror localPosition.x and flip rotation.y for the fishing rod when facing left. Keep scale positive. This ensures the rod appears on the correct side.
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        Vector3 rodLocalPos = fishingRodPrefab.transform.localPosition;
        if (playerSprite != null && playerSprite.flipX)
        {
            // Mirror the localPosition.x
            rodLocalPos.x = -Mathf.Abs(rodLocalPos.x);
            currentFishingRod.transform.localPosition = rodLocalPos;

            // Flip rotation about the Y axis
            Vector3 rodEuler = currentFishingRod.transform.localEulerAngles;
            rodEuler.y = 180f;
            currentFishingRod.transform.localEulerAngles = rodEuler;

            // Keep scale positive
            Vector3 rodScale = currentFishingRod.transform.localScale;
            rodScale.x = Mathf.Abs(rodScale.x);
            rodScale.y = Mathf.Abs(rodScale.y);
            currentFishingRod.transform.localScale = rodScale;
        }
        else
        {
            rodLocalPos.x = Mathf.Abs(rodLocalPos.x);
            currentFishingRod.transform.localPosition = rodLocalPos;

            Vector3 rodEuler = currentFishingRod.transform.localEulerAngles;
            rodEuler.y = 0f;
            currentFishingRod.transform.localEulerAngles = rodEuler;

            Vector3 rodScale = currentFishingRod.transform.localScale;
            rodScale.x = Mathf.Abs(rodScale.x);
            rodScale.y = Mathf.Abs(rodScale.y);
            currentFishingRod.transform.localScale = rodScale;
        }
    }


}
