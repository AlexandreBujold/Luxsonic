using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioPanelController : MonoBehaviour
{
    //Summary + Title
    [SerializeField]
    private TextMeshProUGUI scenarioTitle = null;
    [SerializeField]
    private TextMeshProUGUI durationValue = null;
    [SerializeField]
    private TextMeshProUGUI totalAttemptsValue = null;

    [Space]
    [SerializeField]
    private Transform uiParent = null;
    [SerializeField]
    private Transform taskInfosParent = null;
    [SerializeField]
    private List<TaskInfoUI> taskInfos = new List<TaskInfoUI>();

    // Start is called before the first frame update
    void Start()
    {
        if (taskInfosParent != null)
        {
            foreach (Transform child in taskInfosParent)
            {
                if (child.TryGetComponent<TaskInfoUI>(out TaskInfoUI info))
                {
                    taskInfos.Add(info);
                }
            }
        }

        uiParent.gameObject.SetActive(false);
    }

    public void SetUIVisible()
    {
        ToggleUIVisible(true, true);
    }

    public void ToggleUIVisible(bool visible, bool updateFirst = false)
    {
        if (updateFirst)
            UpdateUI();

        uiParent.gameObject.SetActive(visible);
    }

    public void UpdateUI()
    {
        //Load Latest Run
        ScenarioSaveData save = ScenarioMetricSaver.LoadLatestSaveData();

        if (save != null)
        {
            scenarioTitle.SetText(save.ScenarioName);
            durationValue.SetText(save.TotalDuration.ToString() + "s");
            totalAttemptsValue.SetText(save.TotalIncorrectAttempts.ToString());

            for (int i = 0; i < taskInfos.Count; i++)
            {
                TaskMetricPair pair = save.TaskMetrics[i];
                taskInfos[i].taskName.SetText(pair.Task.TaskName);
                taskInfos[i].taskNumber.SetText((i + 1).ToString());
                taskInfos[i].completionTime.SetText(pair.Metrics.TimeToComplete.ToString() + "s");
                taskInfos[i].attemptCount.SetText(pair.Metrics.IncorrectAttempts.ToString());
            }
        }
    }
}