using TMPro;
using UnityEngine;

public class FinUI : MonoBehaviour
{
    private void ShowTrophy(int winner)
    {
        // on récupère le parent qui contient les trophées
       var trophees = GameObject.Find("Trophées");

        // on affiche le trophée du gagnant
        trophees.transform.GetChild(winner).gameObject.SetActive(true);
    }

    private void ScoreFinal()
    {
        // on récupère le parent qui contient les scores
        var scores = GameObject.Find("Scores");

        // on associe chaque texte de score à un score
        for (int i = 0; i> GameManager.Instance.Score.Count; i++)
        {
            scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Score[i].ToString();
        };
    }
}
