using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [SerializeField] public int MaxScore = 5;

    public ObservableCollection<int> Score = new ObservableCollection<int>() { 0, 0, 0, 0 };
    public List<bool> Connected = new List<bool> { false, false, false, false };
    public List<Color> Color = new List<Color>
    {
           new Color(0.51f, 0.84f, 0.45f),
           new Color(0.95f, 0.56f, 0.56f),
           new Color(1.00f, 0.66f, 0.36f),
           new Color(0.55f, 0.65f, 0.97f)
    };

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        Score.CollectionChanged += new NotifyCollectionChangedEventHandler(
            delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    // Should never be superior, but just in case.
                    if ((int)e.NewItems[0] >= MaxScore)
                    {
                        EndGame(e.NewStartingIndex);
                    }
                }
            });
        DontDestroyOnLoad(this.gameObject);
    }

    void EndGame(int winner)
    {
        SceneManager.LoadScene("Fin", LoadSceneMode.Additive);
        var ui = FindObjectOfType<FinUI>();
        ui.ShowTrophy(winner);
        ui.ScoreFinal();

    }
}
