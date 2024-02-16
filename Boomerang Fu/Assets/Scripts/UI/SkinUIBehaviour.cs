using UnityEngine;

public class SkinUIBehaviour : MonoBehaviour
{
    // Populate the list of every color pickers
    [SerializeField]
    private FlexibleColorPicker[] pickers;

    public void ChangeColor(Color color)
    {
        // Get the panel name, and substring it to have the player id (the name is Joueur X, X being the index + 1)
        var panel = GameObject.FindGameObjectWithTag("SkinPanel");
        int index = int.Parse(panel.name.Substring(7));

        // Change the color to the one passed in parameter by the color picker
        GameManager.Instance.Color[index - 1] = color;
    }

    private void Start()
    {
        // Unlock mouse : In UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Change the color of the color picker to the GameManager's one
        for (int i = 0; i < 4; i++)
        {
            pickers[i].SetColor(GameManager.Instance.Color[i]);
        }
    }
}
