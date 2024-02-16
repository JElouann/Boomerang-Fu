using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    int winner = -1;

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
            return;
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
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        this.winner = winner;
        foreach(var obj in GameObject.FindObjectsOfType<AudioSource>())
        {
            obj.enabled = false;
        }
        Destroy(FindObjectOfType<Canvas>().gameObject);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var ui = FindObjectOfType<FinUI>();
        ui.ShowTrophy(winner);
        ui.ScoreFinal();

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        Connected = new List<bool> { false, false, false, false };
        Score[0] = 0;
        Score[1] = 0;
        Score[2] = 0;
        Score[3] = 0;
        winner = -1;

        FindObjectOfType<Button>().Select();
    }
}
