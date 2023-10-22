using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DayCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Day: " + (GameManager.instance.CurrentDay + 1);
    }
}
