using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text accuracyText;
    public TMP_Text comboText;

    public int hits;
    uint score;
    uint combo; 
    void Start()
    {
        score = 0;
        hits = 0;
        combo = 0;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }

    // How scoring will add and combos
    public void Perfect()
    {
        combo++;
        score += 300 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }
    public void Early()
    {
        combo++;
        score += 100 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }
    public void Late()
    {
        combo++;
        score += 100 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }
    public void ResetCombo()
    {
        combo = 0;
        comboText.text = "x" + combo.ToString();
    }

    public uint getScore()
    {
        return score;
    }

}
