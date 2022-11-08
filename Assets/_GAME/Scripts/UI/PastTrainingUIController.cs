using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastTrainingUIController : MonoBehaviour
{
    [SerializeField]
    private Transform uiParent = null;
    [SerializeField]
    private Transform summaryParent = null;
    [SerializeField]
    private List<TrainingSummaryUI> summaryUI = new List<TrainingSummaryUI>();

    // Start is called before the first frame update
    void Start()
    {
        if (summaryParent != null)
        {
            foreach (Transform child in summaryParent)
            {
                if (child.TryGetComponent<TrainingSummaryUI>(out TrainingSummaryUI summary))
                {
                    summaryUI.Add(summary);
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
        //Load Save Data
        List<ScenarioSaveData> saveData = ScenarioMetricSaver.LoadMultipleSaveData(3);

        for (int i = 0; i < summaryUI.Count; i++)
        {
            if (i >= saveData.Count)
            {
                summaryUI[i].gameObject.SetActive(false);
                continue;
            }

            summaryUI[i].scenarioName.SetText(saveData[i].ScenarioName);
            summaryUI[i].durationValue.SetText(saveData[i].TotalDuration.ToString() + "s");
            summaryUI[i].attemptCount.SetText(saveData[i].TotalIncorrectAttempts.ToString());
            summaryUI[i].gameObject.SetActive(true);
        }
    }
}
