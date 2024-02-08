using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if(_instance == null){
                GameObject go = new GameObject();
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [SerializeField] public int MaxScore;

    ObservableCollection<int> Score = new ObservableCollection<int>();
  
    void Awake(){
        if(_instance != null){
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        Score.CollectionChanged += new NotifyCollectionChangedEventHandler(
            delegate(object sender, NotifyCollectionChangedEventArgs e)                    
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                Debug.Log($"NewItems is {e.NewItems[0]}");
            }
        });
    }
    void Start(){
        Score.Add(2);
    }

    void EndGame(){
        Debug.Log("Travail terminééééééééé");
    }
}
