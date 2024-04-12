using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text accuracyText;
    public TMP_Text comboText;
    public TMP_Text hitTypeText;
    public TMP_Text noteScoreText;
    
    private Color missColor = new Color32(255,26,0,255);
    private Color earlyColor = new Color32(60,220,30,255);
    private Color lateColor = new Color32(220,135,30,255);

    private const float TEXTFADEFLOAT = 1;
    private float textFadeTime;
    public float hits;
    uint score;
    uint combo; 
    void Start()
    {
        score = 0;
        hits = 0;
        combo = 0;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
        hitTypeText.text = "";
        noteScoreText.text = "";
        textFadeTime = TEXTFADEFLOAT;
    }

    void Update()
    {
        if(textFadeTime > 0)
        {
            textFadeTime -= Time.deltaTime;
            hitTypeText.color = new Color(hitTypeText.color.r, hitTypeText.color.g, hitTypeText.color.b,textFadeTime);
            noteScoreText.color = new Color(noteScoreText.color.r, noteScoreText.color.g, noteScoreText.color.b,textFadeTime);
        }   
    }

    // How scoring will add and combos
    public void HoldNote()
    {
        combo++;
        hitTypeText.text = "";
        noteScoreText.text = "";
        score += 100 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }
    public void Perfect()
    {
        combo++;
        hitTypeText.text = "";
        noteScoreText.text = "";
        score += 300 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
    }
    public void Early()
    {
        combo++;
        hitTypeText.color = earlyColor;
        noteScoreText.color = earlyColor;
        hitTypeText.text = "Early";
        noteScoreText.text = "100";
        score += 100 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
        textFadeTime = TEXTFADEFLOAT;
    }
    public void Late()
    {
        combo++;
        hitTypeText.color = lateColor;
        noteScoreText.color = lateColor;
        hitTypeText.text = "Late";
        noteScoreText.text = "100";
        score += 100 * combo;
        accuracyText.text = score.ToString();
        comboText.text = "x" + combo.ToString();
        textFadeTime = TEXTFADEFLOAT;
        
    }
    public void ResetCombo()
    {
        combo = 0;
        hitTypeText.color = missColor;
        noteScoreText.color = missColor;
        hitTypeText.text = "Missed";
        noteScoreText.text = "";
        comboText.text = "x" + combo.ToString();
        textFadeTime = TEXTFADEFLOAT;
    }

    public uint getScore()
    {
        return score;
    }

}
