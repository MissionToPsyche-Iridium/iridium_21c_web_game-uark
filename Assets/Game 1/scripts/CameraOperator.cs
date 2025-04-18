using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CameraOperator : MonoBehaviour
{
    public Transform asteroidImage;
    public float moveSpeed = 0.1f;   // Speed for panning
    public float scaleSpeed = 0.05f; // Speed for zooming
    public float positionMoveSpeed = 0.1f; // Speed for position correction
    public Vector3 defaultScale;     // original scale (8, 5.3, 1)
    public Vector3 defaultPosition;  // original position
    public bool canMove = false;

    // Base movement bounds
    public float baseBoundsX = 10f, baseBoundsY = 5f;
    
    // Dynamic movement bounds
    private float minX, maxX, minY, maxY;

    // Screenshot Dependencies
    public Renderer selectorRenderer; // Reference to the selector renderer
    private Color targetColor = Color.yellow; // The color indicating a feature is selected

    public GameObject flashPanel; // Reference to the UI panel for flash effect

    public GameObject CraterUI;
    public GameObject FrozenEjectaUI;
    public GameObject FaultScarpUI;
    public GameObject SulfurLavaUI;
    public AudioClip flashSFX;
    public AudioSource audioSource;

    void Start()
    {
        defaultPosition = asteroidImage.position; // Store initial position
        defaultScale = asteroidImage.localScale;  // Store correct original scale

        // Initialize bounds dynamically based on the default scale
        UpdateBounds();

        if (flashPanel != null)
            flashPanel.SetActive(false); // Ensure it's disabled at start
    }

    void Update()
    {
        // Move the image using arrow keys
        if (canMove)
        {
            float moveX = -Input.GetAxis("Horizontal") * moveSpeed;
            float moveY = -Input.GetAxis("Vertical") * moveSpeed;

            // Apply movement within dynamic bounds
            Vector3 newPosition = asteroidImage.position + new Vector3(moveX, moveY, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            asteroidImage.position = newPosition;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            // Zoom in (increase scale)
            canMove = true;
            Vector3 newScale = asteroidImage.localScale * (1 + scaleSpeed);
            asteroidImage.localScale = new Vector3(
                Mathf.Min(newScale.x, defaultScale.x * 3), // Cap at max scale
                Mathf.Min(newScale.y, defaultScale.y * 3),
                1
            );

            // Update movement bounds dynamically based on the new scale
            UpdateBounds();
        }

        if (Input.GetKey(KeyCode.X))
        {
            // Zoom out (decrease scale)
            Vector3 newScale = asteroidImage.localScale * (1 - scaleSpeed);

            // Compute zoom progress: 0 when fully zoomed in, 1 when fully zoomed out
            float zoomProgress = Mathf.InverseLerp(defaultScale.x * 2, defaultScale.x, asteroidImage.localScale.x);

            // Move gradually towards the original position, proportional to zoom progress
            asteroidImage.position = Vector3.MoveTowards(asteroidImage.position, defaultPosition, positionMoveSpeed * zoomProgress);

            if (newScale.x > defaultScale.x && newScale.y > defaultScale.y) // Prevent flipping
            {
                asteroidImage.localScale = new Vector3(newScale.x, newScale.y, 1);
            }
            else
            {
                // Ensure scale stays at original when fully zoomed out
                asteroidImage.localScale = defaultScale;
                asteroidImage.position = defaultPosition;
                canMove = false;
            }

            // Update movement bounds dynamically after scaling
            UpdateBounds();
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && selectorRenderer != null)
        {
            // Check if the selector is yellow
            if (selectorRenderer.material.color == targetColor)
            {
                //string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                //string filePath = Application.persistentDataPath + "/" + fileName;

                // Capture the screen and save it
                //ScreenCapture.CaptureScreenshot(filePath);
                //Debug.Log("Screenshot saved at: " + filePath);

                //camera flash sfx
                audioSource.PlayOneShot(flashSFX);

                int feature = GameManager1.SelectCurrentFeature();
                Debug.Log("Photographed feature " + feature);

                switch (feature)
                {
                    case 0:
                        SulfurLavaUI.SetActive(true);
                        break;
                    case 1:
                        CraterUI.SetActive(true);
                        break;
                    case 2:
                        FaultScarpUI.SetActive(true);
                        break;
                    case 3:
                        FrozenEjectaUI.SetActive(true);
                        break;
                    default:
                        Debug.Log("default statement");
                        break;
                }

                // Trigger the screen flash effect
                if (flashPanel != null)
                {
                    StartCoroutine(FlashEffect());
                }
            }
        }
    }

    IEnumerator FlashEffect()
    {
        flashPanel.SetActive(true);  // Make sure it's active
        Image flashImage = flashPanel.GetComponent<Image>();

        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // Start fully transparent

            // Flash effect (fade in)
            flashImage.color = new Color(1, 1, 1, 1); // Fully visible

            float fadeDuration = 0.5f;
            float fadeOutTime = 0f;

            while (fadeOutTime < fadeDuration)
            {
                fadeOutTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, fadeOutTime / fadeDuration);
                flashImage.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            flashPanel.SetActive(false); // Hide it after fade-out
        }
    }

    void UpdateBounds()
    {
        float scaleFactor = asteroidImage.localScale.x / defaultScale.x; // Determine zoom level

        float boundScaleFactor = 0.5f;

        // Adjust movement bounds based on scale
        minX = defaultPosition.x - (baseBoundsX * scaleFactor * boundScaleFactor);
        maxX = defaultPosition.x + (baseBoundsX * scaleFactor * boundScaleFactor);
        minY = defaultPosition.y - (baseBoundsY * scaleFactor * boundScaleFactor);
        maxY = defaultPosition.y + (baseBoundsY * scaleFactor * boundScaleFactor);
    }
}
