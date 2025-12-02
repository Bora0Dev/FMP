using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI Text;   // assign in prefab

   
    public void SetText(string message)
    {
        if (Text != null)
        {
            Text.text = message;
        }
    }
}