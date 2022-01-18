using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RetrieveNotes : MonoBehaviour
{

    private bool notesReady = false; 
    private string[] bKeys = {"22", "30", "32", "34", "37", "39", "42", "44", "46", "49", "51", "54", "56", "58", "61", "63", "66", "68", "70", "73", "75", "78", "80", "82", "85", "87", "90", "92", "94", "99", "102", "104", "106"};
    public List<Dictionary<string, float>> notesList = new List<Dictionary<string, float>>();
    private ColorBlock originalColorBlock;
    private ColorBlock pressedColorBlock;
    private GameObject piano;
    public AudioSource note21;
    public AudioSource note22;
    public AudioSource note23;
    public AudioSource note24;
    public AudioSource note25;
    public AudioSource note26;
    public AudioSource note27;
    public AudioSource note28;
    public AudioSource note29;
    public AudioSource note30;
    public AudioSource note31;
    public AudioSource note32;
    public AudioSource note33;
    public AudioSource note34;
    public AudioSource note35;
    public AudioSource note36;
    public AudioSource note37;
    public AudioSource note38;
    public AudioSource note39;
    public AudioSource note40;
    public AudioSource note41;
    public AudioSource note42;
    public AudioSource note43;
    public AudioSource note44;
    public AudioSource note45;
    public AudioSource note46;
    public AudioSource note47;
    public AudioSource note48;
    public AudioSource note49;
    public AudioSource note50;
    public AudioSource note51;
    public AudioSource note52;
    public AudioSource note53;
    public AudioSource note54;
    public AudioSource note55;
    public AudioSource note56;
    public AudioSource note57;
    public AudioSource note58;
    public AudioSource note59;
    public AudioSource note60;
    public AudioSource note61;
    public AudioSource note62;
    public AudioSource note63;
    public AudioSource note64;
    public AudioSource note65;
    public AudioSource note66;
    public AudioSource note67;
    public AudioSource note68;
    public AudioSource note69;
    public AudioSource note70;
    public AudioSource note71;
    public AudioSource note72;
    public AudioSource note73;
    public AudioSource note74;
    public AudioSource note75;
    public AudioSource note76;
    public AudioSource note77;
    public AudioSource note78;
    public AudioSource note79;
    public AudioSource note80;
    public AudioSource note81;
    public AudioSource note82;
    public AudioSource note83;
    public AudioSource note84;
    public AudioSource note85;
    public AudioSource note86;
    public AudioSource note87;
    public AudioSource note88;
    public AudioSource note89;
    public AudioSource note90;
    public AudioSource note91;
    public AudioSource note92;
    public AudioSource note93;
    public AudioSource note94;
    public AudioSource note95;
    public AudioSource note96;
    public AudioSource note97;
    public AudioSource note98;
    public AudioSource note99;
    public AudioSource note100;
    public AudioSource note101;
    public AudioSource note102;
    public AudioSource note103;
    public AudioSource note104;
    public AudioSource note105;
    public AudioSource note106;
    public AudioSource note107;
    public AudioSource note108;

    public Text debugText;
    public bool debug = true;

    private string songInfoURL = "https://piano-27614-default-rtdb.firebaseio.com/song%20info.json";

    // Start is called before the first frame update
    void Start()
    {

        Screen.orientation = ScreenOrientation.Portrait;
        originalColorBlock = ColorBlock.defaultColorBlock;
        pressedColorBlock = ColorBlock.defaultColorBlock;
        pressedColorBlock.normalColor = Color.gray;

        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available) {
        //        // Create and hold a reference to your FirebaseApp,
        //        // where app is a Firebase.FirebaseApp property of your application class.
        //        Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        //        // Set a flag here to indicate whether Firebase is ready to use by your app.
        //    } 

        //    else {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});

        debugText.enabled = debug;

    }

    void Update() {

        if (notesReady == true)
        {
            if (debug) { debugText.text = "StartCoroutine"; }
            StartCoroutine(playNotes());
        }

        notesReady = false;
    }

    public void downloadNotes()
    {
        if (debug) { debugText.text = "downloadNotes"; }
        notesList.Clear();
        StartCoroutine(GetMidiJSON(songInfoURL));
    }


    private IEnumerator GetMidiJSON(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Get(songInfoURL);
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            if (debug) { debugText.text = www.error; }

        }
        else
        {
            // Show results as text
            string jsonText = www.downloadHandler.text;

            jsonText = jsonText.Substring(1, jsonText.Length - 2);

            Debug.Log(jsonText);


            //jsonText = "{\"list\":" + jsonText + "}";

            //Debug.Log(jsonText);


            //SongData songdata = JsonUtility.FromJson<SongData>(jsonText);
            //SongData songData = JsonUtility.FromJson<SongData>(jsonText);

            //print(songData.list.Count);
            //print(songData.ToString());

            if (debug) { debugText.text = "donereading"; }

            getNotes(jsonText);
        }
    }

    //public void downloadNotes() {
    //    if (debug) { debugText.text = "downloadNotes"; }

    //    FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false); 

    //    //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

    //    FirebaseDatabase.DefaultInstance.RootReference.Child("song info").GetValueAsync().ContinueWith(task => {

    //        if (task.IsFaulted) {
    //            if (debug) { debugText.text = "No notes downloaded"; }

    //            print("No notes downloaded");
    //        }

    //        else if (task.IsCompleted) {
    //            DataSnapshot snapshot = task.Result;
    //            string notes = snapshot.GetRawJsonValue();

    //            notes = notes.Remove(0, 1);
    //            notes = notes.Remove(notes.Length-1);

    //            print(notes);

    //            getNotes(notes);

    //        }
    //    });
    //}

    public void deleteNotes() {

        FirebaseDatabase.DefaultInstance.GetReference("song info").RemoveValueAsync().ContinueWith(task => {
            
            if (task.IsFaulted) {

            }
            
            else if (task.IsCompleted) {

            }
        });
    }

    void getNotes(string notes) {


        while (true) {

            int index = notes.IndexOf('}');
            String singleNote = notes.Substring(0, index+1);
            
            singleNote = singleNote.Replace('\"', '\'');
            
            Dictionary<string, float> singleNoteDict = JsonConvert.DeserializeObject<Dictionary<string, float>>(singleNote);
            notesList.Add(singleNoteDict);

            if((notes.Length  - 1) == index)
                break;
            
            notes = notes.Remove(0, index+2);

        }

        notesReady = true;
  
    }

    IEnumerator playNotes() {
        yield return new WaitForSecondsRealtime(2);

        for (int i = 0; i < notesList.Count; i++) {

            AudioSource noteVolume = GameObject.Find(notesList[i]["note"].ToString()).GetComponent<AudioSource>();
            noteVolume.volume = notesList[i]["velocity"] * .01f;

            string funcName = "playNote" + notesList[i]["note"].ToString();
            //print(funcName);
            Invoke(funcName, 0);
            
            string buttonName = "Button" + notesList[i]["note"].ToString();
            Button noteButton = GameObject.Find(buttonName).GetComponent<Button>();
            noteButton.colors = pressedColorBlock;

            if (i+1 != notesList.Count) {
                /*if(notesList[i]["time_start"] == notesList[i+1]["time_start"]) {
                    print("Time starts == time starts + 1");
                    yield return new WaitForSecondsRealtime(0); 
                }

                else if(notesList[i]["time_end"] == notesList[i+1]["time_start"]) {
                    float noteTime = notesList[i+1]["time_start"] - notesList[i]["time_start"];
                    print("Time end == time start + 1");
                    yield return new WaitForSecondsRealtime(noteTime);
                }

                else if(notesList[i]["time_end"] > notesList[i+1]["time_start"]) {
                    float noteTime = notesList[i+1]["time_start"] - notesList[i]["time_start"];
                    print("Time end > time start + 1");
                    yield return new WaitForSecondsRealtime(noteTime);
                }

                /*else {
                    float noteTime = notesList[i]["time_end"] - notesList[i]["time_start"];
                    print(noteTime);
                    yield return new WaitForSecondsRealtime(noteTime);
                }*/

                float noteTime = notesList[i+1]["time_start"] - notesList[i]["time_start"];
                yield return new WaitForSecondsRealtime(noteTime);

                if (i-1 >= 0) {
                    
                    buttonName = "Button" + notesList[i-1]["note"].ToString();
                    noteButton = GameObject.Find(buttonName).GetComponent<Button>();

                    int pos = Array.IndexOf(bKeys, notesList[i-1]["note"].ToString());
                    if (pos > -1) {
                        originalColorBlock.normalColor = Color.black;
                        noteButton.colors = originalColorBlock;
                    }

                    else {
                        originalColorBlock.normalColor = Color.white;
                        noteButton.colors = originalColorBlock;
                    }

                    //print(i.ToString() + ": Note Time - " + noteTime.ToString() + " Note - " + notesList[i-1]["note"].ToString());
                }
            }
        }

        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioClip = Resources.Load("mySong") as AudioClip;
        //audioSource.clip = audioClip;
    }

    public void QuitApp() {
        Application.Quit();
        print("Quitting Application");
    }


    public void playNote21() {
        note21.Play();
    }

    public void playNote22() {
        note22.Play();
    }

    public void playNote23() {
        note23.Play();
    }

    public void playNote24() {
        note24.Play();
    }

    public void playNote25() {
        note25.Play();
    }

    public void playNote26() {
        note26.Play();
    }

    public void playNote27() {
        note27.Play();
    }

    public void playNote28() {
        note28.Play();
    }

    public void playNote29() {
        note29.Play();
    }

    public void playNote30() {
        note30.Play();
    }

    public void playNote31() {
        note31.Play();
    }

    public void playNote32() {
        note32.Play();
    }

    public void playNote33() {
        note33.Play();
    }

    public void playNote34() {
        note34.Play();
    }

    public void playNote35() {
        note35.Play();
    }

    public void playNote36() {
        note36.Play();
    }

    public void playNote37() {
        note37.Play();
    }

    public void playNote38() {
        note38.Play();
    }

    public void playNote39() {
        note39.Play();
    }

    public void playNote40() {
        note40.Play();
    }

    public void playNote41() {
        note23.Play();
    }

    public void playNote42() {
        note42.Play();
    }

    public void playNote43() {
        note43.Play();
    }

    public void playNote44() {
        note44.Play();
    }

    public void playNote45() {
        note45.Play();
    }

    public void playNote46() {
        note46.Play();
    }

    public void playNote47() {
        note47.Play();
    }

    public void playNote48() {
        note48.Play();
    }

    public void playNote49() {
        note49.Play();
    }

    public void playNote50() {
        note50.Play();
    }

    public void playNote51() {
        note51.Play();
    }

    public void playNote52() {
        note52.Play();
    }

    public void playNote53() {
        note53.Play();
    }

    public void playNote54() {
        note54.Play();
    }

    public void playNote55() {
        note55.Play();
    }

    public void playNote56() {
        note56.Play();
    }

    public void playNote57() {
        note57.Play();
    }

    public void playNote58() {
        note58.Play();
    }

    public void playNote59() {
        note59.Play();
    }

    public void playNote60() {
        note60.Play();
    }

    public void playNote61() {
        note61.Play();
    }

    public void playNote62() {
        note62.Play();
    }

    public void playNote63() {
        note63.Play();
    }

    public void playNote64() {
        note64.Play();
    }

    public void playNote65() {
        note65.Play();
    }

    public void playNote66() {
        note66.Play();
    }

    public void playNote67() {
        note67.Play();
    }

    public void playNote68() {
        note68.Play();
    }

    public void playNote69() {
        note69.Play();
    }

    public void playNote70() {
        note70.Play();
    }

    public void playNote71() {
        note71.Play();
    }

    public void playNote72() {
        note72.Play();
    }

    public void playNote73() {
        note73.Play();
    }

    public void playNote74() {
        note74.Play();
    }

    public void playNote75() {
        note75.Play();
    }

    public void playNote76() {
        note76.Play();
    }

    public void playNote77() {
        note77.Play();
    }

    public void playNote78() {
        note78.Play();
    }

    public void playNote79() {
        note79.Play();
    }

    public void playNote80() {
        note80.Play();
    }

    public void playNote81() {
        note81.Play();
    }

    public void playNote82() {
        note82.Play();
    }

    public void playNote83() {
        note83.Play();
    }

    public void playNote84() {
        note84.Play();
    }

    public void playNote85() {
        note85.Play();
    }

    public void playNote86() {
        note86.Play();
    }

    public void playNote87() {
        note87.Play();
    }

    public void playNote88() {
        note88.Play();
    }

    public void playNote89() {
        note89.Play();
    }

    public void playNote90() {
        note90.Play();
    }

    public void playNote91() {
        note91.Play();
    }

    public void playNote92() {
        note92.Play();
    }

    public void playNote93() {
        note93.Play();
    }

    public void playNote94() {
        note94.Play();
    }

    public void playNote95() {
        note95.Play();
    }

    public void playNote96() {
        note96.Play();
    }

    public void playNote97() {
        note97.Play();
    }

    public void playNote98() {
        note98.Play();
    }

    public void playNote99() {
        note99.Play();
    }

    public void playNote100() {
        note100.Play();
    }

    public void playNote101() {
        note101.Play();
    }

    public void playNote102() {
        note102.Play();
    }

    public void playNote103() {
        note103.Play();
    }

    public void playNote104() {
        note104.Play();
    }

    public void playNote105() {
        note105.Play();
    }

    public void playNote106() {
        note106.Play();
    }

    public void playNote107() {
        note107.Play();
    }

    public void playNote108() {
        note108.Play();
    }
}