using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player, HeroStats herostats)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(player, herostats);

        formater.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();          
            return data;
        }
        else
        {
            Debug.LogError("Save fiel not find");
            return null;
        }
    }
}
