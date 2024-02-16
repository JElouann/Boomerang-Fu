using UnityEngine;

public class ScriptTestMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(1, 0, 0) * 2 * Time.deltaTime;
        }
    }
}
