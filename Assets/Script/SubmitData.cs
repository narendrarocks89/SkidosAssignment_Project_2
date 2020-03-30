using UnityEngine;
using TMPro;
using System.IO;
public class SubmitData : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI ValueText;

    public class PlayerData
    {
        public string Key;
        public string Value;
    }

    public void Fetch()
    {
        string saveString = SaveSystem.Load();

        if (saveString != null)
        {
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(saveString);

            Debug.Log("Key : " + playerData.Key);
            Debug.Log("Value : " + playerData.Value);

            keyText.text = "KEY: " + playerData.Key;
            ValueText.text = "VALUE: " + playerData.Value;
        }
        else
        {
            Debug.Log("File is missing.");
        }
    }
}