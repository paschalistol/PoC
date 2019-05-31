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
        public List<EnemyData> enemiesData = new List<EnemyData>();
        public PlayerData playerData;

    }

    #region Datatypes

    [System.Serializable]
    public abstract class Data
    {
        public float[] position;
        public float[] rotation;
    }

    [System.Serializable]
    public class PlayerData : Data
    {

        public PlayerData(GameObject player)
        {
            position = new float[3];
            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;

            rotation = new float[3];
            rotation[0] = player.transform.rotation.x;
            rotation[1] = player.transform.rotation.y;
            rotation[2] = player.transform.rotation.z;
        }
    }

    [System.Serializable]
    public class EnemyData : Data
    {

        public EnemyData(GameObject enemy)
        {
            position = new float[3];
            position[0] = enemy.transform.position.x;
            position[1] = enemy.transform.position.y;
            position[2] = enemy.transform.position.z;

            //rotation = enemy.transform.rotation;
        }
    }


    [System.Serializable]
    public class InteractablesData : Data
    {



        public InteractablesData(GameObject interactables)
        {
            position = new float[3];
            position[0] = interactables.transform.position.x;
            position[1] = interactables.transform.position.y;
            position[2] = interactables.transform.position.z;

            //rotation = interactables.transform.rotation;
        }
    }

    #endregion  


    #region Save

    public void Save()
    {
        SaveData saveData = new SaveData();

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Save.oof";
        FileStream stream = new FileStream(path, FileMode.Create);


        SavingPlayer(saveData);
        SavingInteractables(saveData);
        SavingEnemies(saveData);

        formatter.Serialize(stream, saveData);
        stream.Close();

        Debug.Log("saved");
    }

    private void SavingPlayer(SaveData saveData)
    {
        GameObject player = GameManager.gameManager.player;
        PlayerData playerData = new PlayerData(player);
        saveData.playerData = playerData;
    }

    private void SavingInteractables(SaveData saveData)
    {
        for (int i = 0; i < GameManager.gameManager.interactables.Count; i++)
        {
            GameObject interactable = GameManager.gameManager.interactables[i];
            InteractablesData interactableData = new InteractablesData(interactable);
            saveData.interactablesData.Add(interactableData);
        }
    }

    private void SavingEnemies(SaveData saveData)
    {
        for (int i = 0; i < GameManager.gameManager.enemies.Count; i++)
        {
            GameObject enemy = GameManager.gameManager.enemies[i];
            EnemyData enemyData = new EnemyData(enemy);
            saveData.enemiesData.Add(enemyData);
        }
    }

    #endregion


    #region Load

    public void Load()
    {
        string path = Application.persistentDataPath + "/Save.oof";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;

            LoadPlayer(saveData);
            LoadInteractables(saveData);
            LoadEnemies(saveData);

            stream.Close();
            Debug.Log("loaded");

        }
        else
        {
            Debug.Log("no path lol");
        }

    }

    #region Loading Player
    private void LoadPlayer(SaveData saveData)
    {
        
        GameManager.gameManager.player.transform.position = LoadPlayerPosition(saveData);     
        GameManager.gameManager.player.transform.rotation = LoadPlayerRotation(saveData);
    }

    private Vector3 LoadPlayerPosition(SaveData saveData)
    {
        float x = saveData.playerData.position[0];
        float y = saveData.playerData.position[1];
        float z = saveData.playerData.position[2];
        return new Vector3(x, y, z);
    }

    private Quaternion LoadPlayerRotation(SaveData saveData)
    {
        float x = saveData.playerData.position[0];
        float y = saveData.playerData.position[1];
        float z = saveData.playerData.position[2];
        return new Quaternion(0, x, y, z);
    }

    #endregion

    private void LoadInteractables(SaveData saveData)
    {
        for (int i = 0; i < saveData.interactablesData.Count; i++)
        {
            float x = saveData.interactablesData[i].position[0];
            float y = saveData.interactablesData[i].position[1];
            float z = saveData.interactablesData[i].position[2];
            Vector3 savedPosition = new Vector3(x, y, z);
            GameManager.gameManager.interactables[i].transform.position = savedPosition;
        }
    }

    private void LoadEnemies(SaveData saveData)
    {
        for (int i = 0; i < saveData.enemiesData.Count; i++)
        {
            float x = saveData.enemiesData[i].position[0];
            float y = saveData.enemiesData[i].position[1];
            float z = saveData.enemiesData[i].position[2];
            Vector3 savedPosition = new Vector3(x, y, z);
            GameManager.gameManager.enemies[i].transform.position = savedPosition;
        }
    }
    #endregion


}
