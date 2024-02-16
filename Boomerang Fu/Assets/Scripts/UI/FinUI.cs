using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinUI : MonoBehaviour
{
    public void ShowTrophy(int winner)
    {
        // on récupère le parent qui contient les trophées
        var trophees = GameObject.Find("Trophées");

        // on affiche le trophée du gagnant
        trophees.transform.GetChild(winner).gameObject.SetActive(true);
    }

    public void ScoreFinal()
    {
        // on récupère le parent qui contient les scores
        var scores = GameObject.Find("Scores");

        // on associe chaque texte de score à un score
        for (int i = 0; i < GameManager.Instance.Score.Count; i++)
        {
            scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Score[i].ToString();
        };
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        var cadres = GameObject.Find("Cadres");
        for (int i = 0; i < GameManager.Instance.Color.Count; i++)
        {
            cadres.transform.GetChild(i).GetComponent<Image>().color = 
                GameManager.Instance.Color[i] * (GameManager.Instance.Connected[i] ? 1 : 0.5f);
        };

        Time.timeScale = 0;
    }

}
