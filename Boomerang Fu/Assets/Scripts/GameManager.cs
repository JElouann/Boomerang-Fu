using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObservableCollection<int> Score = new ObservableCollection<int>() { 0, 0, 0, 0 };
    public List<bool> Connected = new List<bool> { false, false, false, false };
    public List<Color> Color = new List<Color>
    {
           new (0.51f, 0.84f, 0.45f),
           new (0.95f, 0.56f, 0.56f),
           new (1.00f, 0.66f, 0.36f),
           new (0.55f, 0.65f, 0.97f),
    };

    private int _winner;

    [SerializeField]
    private int _maxScore = 5;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        Score.CollectionChanged += new NotifyCollectionChangedEventHandler(
            delegate(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    // Should never be superior, but just in case.
                    if ((int)e.NewItems[0] >= _maxScore)
                    {
                        EndGame(e.NewStartingIndex);
                    }
                }
            });
        DontDestroyOnLoad(this.gameObject);
    }

    private void EndGame(int winner)
    {
        SceneManager.LoadScene("Fin", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        this._winner = winner;

        Destroy(FindObjectOfType<Canvas>().gameObject);

        // Disable every audio source in the game -> We don't want any of the main game sound in the end menu
        foreach (var obj in GameObject.FindObjectsOfType<AudioSource>())
        {
            obj.enabled = false;
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var ui = FindObjectOfType<FinUI>();
        ui.ShowTrophy(_winner);
        ui.ScoreFinal();

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        for (int i = 0; i < 4; i++)
        {
            Score[i] = 0;
        }

        Connected = new List<bool> { false, false, false, false };

        FindObjectOfType<Button>().Select();
    }
}
