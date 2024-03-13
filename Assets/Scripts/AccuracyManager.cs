using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text accuracyText;

    public int score; // Will be changed to a double later
    void Start()
    {
        score = 0;
        accuracyText.text = score.ToString();
    }

    // Will find out the accuracy of the player for the whole level. 
    // For right now, just increases score when note is hit
    public void AccuracyInc()
    {
        score++;
        accuracyText.text = score.ToString();
    }
}
