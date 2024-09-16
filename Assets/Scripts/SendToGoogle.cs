using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class SendToGoogle : MonoBehaviour
{
    // Start is called before the first frame update
    public string URL;
    private DateTime startTime;
    private AbilityManager abilityManager;
    private float nextActionTime = 0.0f;
    private float period = 1.0f; // ��
    private PlayerController playcontroller;
    private Dictionary<float, Vector2> positionData = new Dictionary<float, Vector2>();
    private Dictionary<float, String> abilityData = new Dictionary<float, String>();
    private Dictionary<float, String> healthyData = new Dictionary<float, String>();
    void Start()
    {
        this.startTime = DateTime.Now;
        abilityManager = GetComponent<AbilityManager>();
        playcontroller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            string timeability = abilityManager.getSelectedAbility().ToUpper();
            string healthy = playcontroller.getHealthy().ToString();
            positionData.Add(nextActionTime, new Vector2(transform.position.x, transform.position.y));
            abilityData.Add(nextActionTime, timeability);
            healthyData.Add(nextActionTime, healthy);
        }
    }

    public void Send(DateTime deadtime, Vector2 deadposition, string enemy)
    {
        string sceneName = SceneManager.GetActiveScene().name; ;
        Debug.Log("try to send to google form");
        TimeSpan timePlayed = DateTime.Now - this.startTime;
        string ability = abilityManager.getSelectedAbility().ToUpper();

        // Combine all position data and timestamps into a single string
        StringBuilder positionTimestampData = new StringBuilder();
        StringBuilder abilityTimestampData = new StringBuilder();
        StringBuilder healthyTimestampData = new StringBuilder();
        foreach (var entry in positionData)
        {
            positionTimestampData.AppendFormat("{0}:{1};", entry.Key.ToString(), entry.Value.ToString());
        }
        foreach (var entry in abilityData)
        {
            abilityTimestampData.AppendFormat("{0}:{1};", entry.Key.ToString(), entry.Value.ToString());
        }
        foreach (var entry in healthyData)
        {
            healthyTimestampData.AppendFormat("{0}:{1};", entry.Key.ToString(), entry.Value.ToString());
        }

        StartCoroutine(Post(deadtime.ToString(), deadposition.ToString(), enemy, timePlayed.ToString(), ability, positionTimestampData.ToString(), abilityTimestampData.ToString(), healthyTimestampData.ToString(),sceneName));
    }

    private IEnumerator Post(string deadtime, string deadposition, string enemy, string timePlayed, string ability, string positionTimestampData,string abilityTimestampData, string healthyTimestampData, string sceneName)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.2004368566", deadtime);
        form.AddField("entry.1825431539", deadposition);
        form.AddField("entry.1728689000", enemy);
        form.AddField("entry.370185319", timePlayed);
        form.AddField("entry.1648837221", ability);
        form.AddField("entry.1775864670", positionTimestampData);
        form.AddField("entry.713905336", abilityTimestampData);
        form.AddField("entry.296926494", healthyTimestampData);
        form.AddField("entry.1939754744", sceneName);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("google form failed");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}