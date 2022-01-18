using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Storage;
using System.Threading.Tasks;
using System;

using UnityEngine.Networking;
using System.IO;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class Recording : MonoBehaviour
{

    private string fireBaseURL = "https://piano-27614-default-rtdb.firebaseio.com/Address.json";
    public string serverURL = "";

    AudioSource audioSource;
    private float recordingTime;

    void Start() {
        GetCurrentServer();

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        #endif
    }
    
    // Start recording with built-in Microphone and play the recorded audio right away
    public void startRecording()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, false, 300, 44100); //"Microphone (High Definition Audio Device)"
        recordingTime = Time.time;
        //audioSource.Play();
    }

    public void stopRecording()
    {  
        AudioClip recordingNew = AudioClip.Create(audioSource.clip.name, (int)((Time.time - recordingTime) * audioSource.clip.frequency), audioSource.clip.channels, audioSource.clip.frequency, false);
        float[] data = new float[(int)((Time.time - recordingTime) * audioSource.clip.frequency)];
        audioSource.clip.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.audioSource.clip = recordingNew;


        SavWav.Save("mySong", audioSource.clip);
        print("Recording Saved");
        string local_file = Application.persistentDataPath + "/mySong.wav";

        StartCoroutine(UploadFile(local_file));

        FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference storageRef = storage.GetReferenceFromUrl("gs://piano-27614.appspot.com");
        Firebase.Storage.StorageReference storage_ref = storageRef.Child("song.wav");

        storage_ref.PutFileAsync(local_file).ContinueWith((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            } 
            else {
                // Metadata contains file metadata such as size, content-type, and download URL.
                string download_url = task.Result.ToString();
                Debug.Log("Finished uploading...");
                Debug.Log("download url = " + download_url);
            }
        });
    }


    void GetCurrentServer()
    {
        StartCoroutine(GetCurrentServerJSON());
    }

    private IEnumerator GetCurrentServerJSON()
    {
        UnityWebRequest www = UnityWebRequest.Get(fireBaseURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonText = www.downloadHandler.text;
            jsonText = jsonText.Replace("\"", "");
            serverURL = jsonText + "/midi";
            Debug.Log(jsonText);
        }
    }

    IEnumerator UploadFile(string path)
    {
        WWWForm form = new WWWForm();
        byte[] bytes = File.ReadAllBytes(path);

        form.AddBinaryData("song", bytes, "song.wav", "audio/wav");

        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
