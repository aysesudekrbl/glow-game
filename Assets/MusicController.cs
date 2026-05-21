using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;

   void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject); 
    }
}}