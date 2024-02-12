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

    [SerializeField] public int MaxScore;

    public ObservableCollection<int> Score = new ObservableCollection<int>() { 0, 0, 0, 0 };
    public List<bool> Connected = new List<bool>{ false, false, false, false };
  
    void Awake(){
        if(_instance != null){
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
    void Start()
    {

    }

    void EndGame(int winner)
    {
        SceneManager.LoadScene("EndingScene", LoadSceneMode.Additive);

    }
}
