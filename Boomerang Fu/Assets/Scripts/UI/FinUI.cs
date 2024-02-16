using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinUI : MonoBehaviour
{
    public void ShowTrophy(int winner)
    {
        // We get all the trophies parent object
        var trophees = GameObject.Find("Trophées");

        // and activate only the winner's one
        trophees.transform.GetChild(winner).gameObject.SetActive(true);
    }

    public void ScoreFinal()
    {
        // We get all the scores parent object
        var scores = GameObject.Find("Scores");

        // For each one, we associate the score to the text mesh pro itself
        for (int i = 0; i < GameManager.Instance.Score.Count; i++)
        {
            scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                GameManager.Instance.Score[i].ToString();
        }
    }

    private void Awake()
    {
        // We unlock the cursor, we are now in UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // We get all the Score's text border
        var cadres = GameObject.Find("Cadres");
        for (int i = 0; i < GameManager.Instance.Color.Count; i++)
        {
            // And make them the color of the corresponding player
            cadres.transform.GetChild(i).GetComponent<Image>().color =
                GameManager.Instance.Color[i] * (GameManager.Instance.Connected[i] ? 1 : 0.5f);
        }

        // We freeze time
        Time.timeScale = 0;
    }
}
