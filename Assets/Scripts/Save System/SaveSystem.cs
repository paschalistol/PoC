using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public List<InteractablesData> interactablesData = new List<InteractablesData>();

    }


    [System.Serializable]
    public class InteractablesData
    {

        public float[] position;

        public InteractablesData(GameObject interactables)
        {
            position = new float[3];
            position[0] = interactables.transform.position.x;
            position[1] = interactables.transform.position.y;
            position[2] = interactables.transform.position.z;
        }
    }


    /*#region Player
    public void SavePlayer(CharacterStateMachine player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.oof";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);

        stream.Close();
        Debug.Log("saved player");
    }

    public PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.oof";
        Debug.Log("load playuer");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    #endregion*/

    #region Interactables
    public void SaveInteractables()
    {
        SaveData saveData = new SaveData();
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Interactables.oof";

        FileStream stream = new FileStream(path, FileMode.Create);
        for (int i = 0; i < GameManager.gameManager.interactables.Count; i++)
        {
            GameObject interactables = GameManager.gameManager.interactables[i];
            InteractablesData interactablesData = new InteractablesData(interactables);
            saveData.interactablesData.Add(interactablesData);
        }
        formatter.Serialize(stream, saveData);
        Debug.Log("saved inter");
        stream.Close();

    }

    public void LoadInteractables()
    {
        string path = Application.persistentDataPath + "/Interactables.oof";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            for(int i = 0; i < saveData.interactablesData.Count; i++)
            {
                float x = saveData.interactablesData[i].position[0];
                float y = saveData.interactablesData[i].position[1];
                float z = saveData.interactablesData[i].position[2];
                Vector3 savedPosition = new Vector3(x, y, z);
                GameManager.gameManager.interactables[i].transform.position = savedPosition;
            }
            stream.Close();
            Debug.Log("loaded inter");
            
        }
        else
        {
            Debug.Log("no path lol");
        }
    }
    #endregion
    

}
