using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        Test();
        AudioTask();
    }

    private void AudioTask()
    {
        if (CSVUtils.TryReadVolume(_audioSource.volume))
        {
            _audioSource.volume = CSVUtils.GetVolume();
        }
    }

    private void OnDestroy() => CSVUtils.TryReadVolume(_audioSource.volume);

    private void Test()
    {
        if (CSVUtils.TryReadJson(out DataBase dataBase))
        {
            foreach (Data data in dataBase.DataInstances)
            {
                Debug.Log($"{data.Name}: {data.Description}");
            }
        }
        else
        {
            Debug.LogError("Can't read Json");
        }
    }
}
