using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClueManager : MonoBehaviour
{
    [System.Serializable]
    public class Clue
    {
        public string clueId;
        public GameObject letterObject;
        public string hintText;
    }

    public List<Clue> clues = new List<Clue>();
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI congratsText;
    public GameObject treasureChest;            // assign the chest prefab/instance
    public Vector3 chestLocalOffset = new Vector3(0f, 0.12f, 0f); // offset above marker
    public AudioSource audioSource;
    public AudioClip dingSound;
    public AudioClip treasureSound;

    private int currentClueIndex = 0;
    private int collectedCount = 0;

    // remember the transform of the last-scanned image target
    private Transform lastFoundTarget;

    void Start()
    {
        if (clues == null || clues.Count == 0)
        {
            Debug.LogError("ClueManager: No clues configured.");
            return;
        }

        // Hide letters and UI init
        foreach (var c in clues)
            if (c.letterObject != null)
                c.letterObject.SetActive(false);

        if (treasureChest != null)
            treasureChest.SetActive(false);

        if (congratsText != null)
            congratsText.gameObject.SetActive(false);

        ShowHint(clues[0].hintText);
        UpdateProgress();
    }

    // Called by TargetHandler when a marker is tracked; receives that target's transform
    public void OnTargetFound(string clueId, Transform targetTransform)
    {
        Clue clue = clues.Find(c => c.clueId == clueId);

        if (clue != null && clues.IndexOf(clue) == currentClueIndex)
        {
            // remember where this target is (used if this is the final clue)
            lastFoundTarget = targetTransform;

            // show the letter and auto-collect after delay
            StartCoroutine(HandleLetterSequence(clue));
        }
    }

    private IEnumerator HandleLetterSequence(Clue clue)
    {
        if (clue.letterObject != null)
            clue.letterObject.SetActive(true);

        // wait before playing ding (2s)
        yield return new WaitForSeconds(2f);

        if (audioSource != null && dingSound != null)
            audioSource.PlayOneShot(dingSound);

        // small extra delay so player hears ding then letter hides
        yield return new WaitForSeconds(0.5f);

        if (clue.letterObject != null)
            clue.letterObject.SetActive(false);

        // update counters
        collectedCount++;
        currentClueIndex++;
        UpdateProgress();

        if (currentClueIndex < clues.Count)
        {
            ShowHint(clues[currentClueIndex].hintText);
        }
        else
        {
            // final clue collected — show treasure at the last found target
            RevealTreasure();
        }
    }

    void ShowHint(string text)
    {
        if (hintText != null) hintText.text = text;
    }

    void UpdateProgress()
    {
        if (progressText != null) progressText.text = $"Letters Found: {collectedCount} / {clues.Count}";
    }

    void RevealTreasure()
    {
        // clear hint and show congrats
        if (hintText != null) hintText.text = "";
        if (congratsText != null)
        {
            congratsText.gameObject.SetActive(true);
            congratsText.text = "Congratulations! You found the treasure!";
        }

        if (treasureChest == null)
        {
            Debug.LogWarning("ClueManager: TreasureChest not assigned.");
            return;
        }

        // If we have a valid last-found target, place the chest on it
        if (lastFoundTarget != null)
        {
            // ensure the chest is active and positioned relative to the image target
            treasureChest.SetActive(true);

            // Parent the chest to the image target so it moves/tracks with it
            treasureChest.transform.SetParent(lastFoundTarget, worldPositionStays: false);

            // set local position with offset so it sits above the marker
            treasureChest.transform.localPosition = chestLocalOffset;
            treasureChest.transform.localRotation = Quaternion.identity;
            treasureChest.transform.localScale = Vector3.one; // adjust if needed
        }
        else
        {
            // fallback: spawn chest in front of camera if we somehow don't have target
            Camera cam = Camera.main;
            if (cam != null)
            {
                treasureChest.transform.SetParent(null);
                treasureChest.transform.position = cam.transform.position + cam.transform.forward * 1.2f;
                treasureChest.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
                treasureChest.SetActive(true);
            }
        }

        // play treasure sound if provided
        if (audioSource != null && treasureSound != null)
            audioSource.PlayOneShot(treasureSound);
    }
}
