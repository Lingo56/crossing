using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class Fishing : MonoBehaviour
{
    public GameObject fishingRod;
    public GameObject fishIcon;
    public TextMeshProUGUI scoreText;

    private bool nearWater = false;
    private int score = 0;
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
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

        // Show fishing rod and icon
        if (fishingRod != null)
        {
            audioSources[0].Play();
            fishingRod.SetActive(true);
        }

        // Stop player movement
        MoveCharacter moveChar = GetComponent<MoveCharacter>();
        if (moveChar != null)
            moveChar.isFishing = true;

        // Start coroutine for fishing sequence
        StartCoroutine(FishingSequence());
    }

    private System.Collections.IEnumerator FishingSequence()
    {
        yield return new WaitForSeconds(1.5f);

        // Increase score and update text
        score++;
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // Hide fishing rod and icon
        if (fishingRod != null)
            fishingRod.SetActive(false);

        // Show fish icon
        if (fishIcon != null)
        { 
            audioSources[1].Play();
            fishIcon.SetActive(true);
        }

        // Allow player movement again
        MoveCharacter moveChar = GetComponent<MoveCharacter>();
        if (moveChar != null)
            moveChar.isFishing = false;

        // Hide fish after 1 second
        yield return new WaitForSeconds(0.75f);
        if (fishIcon != null)
            fishIcon.SetActive(false);
    }
}
