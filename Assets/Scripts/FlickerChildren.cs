using System.Collections;
using UnityEngine;

public class FlickerChildren : MonoBehaviour
{
    public static FlickerChildren Instance;
    public GameObject[] childObjects;
    public float flickerDuration = 0.5f;
    public float flickerInterval = 0.1f;
    public float totalTime = 10f; // Adjust this to control the total flicker time

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // Disable all child objects at the beginning
        foreach (GameObject child in childObjects)
        {
            child.SetActive(false);
        }
        

        // Start the flickering coroutine
        
    }

    public void Flicker()
    {
        StartCoroutine(FlickerBullets());
    }

    private IEnumerator FlickerBullets()
    {
        float flickerTimer = 0f;

        while (flickerTimer < totalTime)
        {
            // Choose a random child object
            int randomIndex = Random.Range(0, childObjects.Length);

            // Activate the chosen child object
            childObjects[randomIndex].SetActive(true);

            // Wait for the flicker duration
            yield return new WaitForSeconds(flickerDuration);

            // Deactivate the chosen child object
            childObjects[randomIndex].SetActive(false);

            // Wait for a brief interval before flickering the next one
            yield return new WaitForSeconds(flickerInterval);

            // Update the flicker timer
            flickerTimer += flickerDuration + flickerInterval;
        }

        // Turn off all child objects at the end
        foreach (GameObject child in childObjects)
        {
            child.SetActive(false);
        }
    }
}
