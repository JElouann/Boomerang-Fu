using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    // Get all scores texts of the main game UI
    [SerializeField]
    private List<TextMeshProUGUI> texts;

    private void Awake()
    {
        // Add collection changed event listener
        GameManager.Instance.Score.CollectionChanged += Score_CollectionChanged;

        // Change background image of each panel
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = GameManager.Instance.Color[i];
        }

        // Lock cursor, we are in game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Score_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        // If the score has changed, replace the text
        if (e.Action == NotifyCollectionChangedAction.Replace)
        {
            texts[e.NewStartingIndex].text = e.NewItems[0].ToString();
        }
    }

    private void FixedUpdate()
    {
        // For each panels, activate the one that are onlines
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(GameManager.Instance.Connected[i]);
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when destroyed
        GameManager.Instance.Score.CollectionChanged -= Score_CollectionChanged;
    }
}
