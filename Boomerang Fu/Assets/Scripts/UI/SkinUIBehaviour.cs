using UnityEngine;

public class SkinUIBehaviour : MonoBehaviour
{
    [SerializeField]
    private FlexibleColorPicker[] pickers;

    public void ChangeColor(Color color)
    {
        var panel = GameObject.FindGameObjectWithTag("SkinPanel");
        int index = int.Parse(panel.name.Substring(7));

        GameManager.Instance.Color[index - 1] = color;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < 4; i++)
        {
            pickers[i].SetColor(GameManager.Instance.Color[i]);
        }
    }
}
