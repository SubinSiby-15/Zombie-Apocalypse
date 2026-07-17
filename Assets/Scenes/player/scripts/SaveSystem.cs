using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SaveData(int health, int score)
    {
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Score", score);

        PlayerPrefs.Save();

        Debug.Log("Saved");
    }

    public static int LoadHealth()
    {
        return PlayerPrefs.GetInt("Health", 100);
    }

    public static int LoadScore()
    {
        return PlayerPrefs.GetInt("Score", 0);
    }

    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared");
    }
}
