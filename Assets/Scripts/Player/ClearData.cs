using UnityEngine;

public class ClearData : MonoBehaviour
{
    private void Awake()
    {
        ES3.DeleteDirectory(Application.persistentDataPath);
    }
}
