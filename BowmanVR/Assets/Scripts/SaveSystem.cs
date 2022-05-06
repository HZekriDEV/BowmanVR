using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem
{
    public static bool fileExists;
    public static bool playerExists;
    public static bool topScoreExists;

    public static void SaveTopScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/topScore.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        TopScore data = new TopScore();

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Top Score Saved");
    }

    public static TopScore LoadTopScore()
    {
        string path = Application.persistentDataPath + "/topScore.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TopScore data = formatter.Deserialize(stream) as TopScore;
            stream.Close();
            topScoreExists = true;
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            topScoreExists = false;
            return null;
        }
    }

    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Player Stats Saved");
    }
    public static void PausedPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/TempPause.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        TempData data = new TempData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            playerExists = true;
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            playerExists = false;
            return null;
        }
    }
    public static TempData LoadResumeData()
    {
        string path = Application.persistentDataPath + "/TempPause.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TempData data = formatter.Deserialize(stream) as TempData;
            stream.Close();
            fileExists = true;
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            fileExists = false;
            return null;
        }
    }
}
