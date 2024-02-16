using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    private List<TextMeshProUGUI> texts;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.Score.CollectionChanged += Score_CollectionChanged;

        texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Color[i];
        }
    }

    private void Score_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Replace)
        {
            texts[e.NewStartingIndex].text = e.NewItems[0].ToString();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(GameManager.Instance.Connected[i]);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Score.CollectionChanged -= Score_CollectionChanged; 
    }
}
