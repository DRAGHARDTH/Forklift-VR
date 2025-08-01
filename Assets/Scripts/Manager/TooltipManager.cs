using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    public VoiceLines_DataModel voicelineDatamodel;

    private Dictionary<int, VoiceLine> voiceLookup = new();
    public string audioFolderName = "VoiceLines";

    public string xmlFileName = "voice_lines.xml";


    private AudioSource audioSource;
    private string audioFolderPath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        audioSource = GetComponent<AudioSource>();
        audioFolderPath = Path.Combine(Application.streamingAssetsPath, audioFolderName);

        StartCoroutine(LoadVoiceLines());
    }

    private IEnumerator LoadVoiceLines()
    {
        string xmlPath = Path.Combine(Application.streamingAssetsPath, xmlFileName);

        using (UnityWebRequest www = UnityWebRequest.Get(xmlPath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load voice_lines.xml: " + www.error);
                yield break;
            }

            try
            {
                string xmlContent = www.downloadHandler.text;
                XmlSerializer serializer = new(typeof(VoiceLines_DataModel));

                using (var reader = new StringReader(xmlContent))
                {
                    voicelineDatamodel = serializer.Deserialize(reader) as VoiceLines_DataModel;
                    if (voicelineDatamodel != null)
                    {
                        foreach (var line in voicelineDatamodel.allVoiceLines)
                        {
                            voiceLookup[line.id] = line;
                        }
                        Debug.Log($"Loaded {voiceLookup.Count} voice lines.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing XML: " + ex.Message);
            }
        }
    }

    public string PlayVoice(int id)
    {
        if (!voiceLookup.TryGetValue(id, out var line))
        {
            Debug.LogWarning("Voice line not found: " + id);
            return string.Empty;
        }

        string audioPath = Path.Combine(audioFolderPath, line.file);

#if UNITY_ANDROID && !UNITY_EDITOR
        string uri = audioPath; // Android uses jar streaming path
#else
        string uri = "file://" + audioPath;
#endif

        StartCoroutine(LoadAndPlay(uri, line.subtitle));
        return line.subtitle ?? string.Empty;
    }

    private IEnumerator LoadAndPlay(string uri, string subtitle)
    {
        using (var www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load audio: " + uri);
                yield break;
            }

            audioSource.Stop();
            var clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    [XmlRoot("VoiceLines_DataModel"), Serializable]
    public class VoiceLines_DataModel
    {
        [XmlArray("VoiceLines"), XmlArrayItem("VoiceLine")]
        public List<VoiceLine> allVoiceLines;
    }

    [Serializable]
    public class VoiceLine
    {
        [XmlElement("Id")]
        public int id;

        [XmlElement("FileName")]
        public string file;

        [XmlElement("Subtitle")]
        public string subtitle;
    }
}
