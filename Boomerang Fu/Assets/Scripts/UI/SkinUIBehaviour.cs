using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUIBehaviour : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<FlexibleColorPicker>().SetColor (GameManager.Instance.Color[i]);
        }
    }

    public void ChangeColor(Color color)
    {
        var panel = GameObject.FindGameObjectWithTag("SkinPanel");
        int index = int.Parse(panel.name.Substring(7));

        GameManager.Instance.Color[index - 1] = color;
    }
}
