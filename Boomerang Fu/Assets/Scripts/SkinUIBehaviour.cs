using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUIBehaviour : MonoBehaviour
{
    public void ChangeColor(Color color)
    {
        var panel = GameObject.FindGameObjectWithTag("SkinPanel");
        int index = int.Parse(panel.name.Substring(7));

        GameManager.Instance.Color[index - 1] = color;
    }
}
