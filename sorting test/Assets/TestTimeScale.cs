using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTimeScale : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    float i = 1000;
    private void Update()
    {


        i -= Time.deltaTime;
        timer.text = ((int)i).ToString();

        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 100;
            Debug.Log(Time.timeScale);
        }
    }
}
