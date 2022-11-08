using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; } = null;
    //Index in list is the task ID / index, test progresses from 0 - n
    [SerializeField, Tooltip("Tasks to be performed in order of the list.")]
    private List<Task> tasks = new List<Task>();
    [SerializeField, Tooltip("Overrides the task list of this manager.")]
    private Scenario scenarioOverride = null;

    [SerializeField, Range(0, 10)]
    private int currentTask = 0;

    public UnityEvent<int> OnTaskCompleted { get; private set; } = new UnityEvent<int>();
    public UnityEvent<int> OnTaskFailed { get; private set; } = new UnityEvent<int>();

    [SerializeField]
    private List<TaskReactor> taskReactors = new List<TaskReactor>();

    //Used to track metrics for each task
    private Dictionary<Task, TaskMetrics> TaskTracking = new Dictionary<Task, TaskMetrics>();
    private Task CurrentTask => tasks[currentTask];
    private float timeTaskStarted = 0;

    private bool TasksComplete => currentTask == tasks.Count;

    /*
     * Attempted: player interacted close to the task but did not complete it
     * Completed: player interacted with a task and completed it
     */

    private void Awake()
    {
        Instance = Instance == null ? this : Instance;

        //Get any missing child reactors
        foreach (Transform child in transform)
        {
            TaskReactor[] reactors = child.GetComponentsInChildren<TaskReactor>();
            if (reactors.Length != 0)
            {
                for (int i = 0; i < reactors.Length; i++)
                {
                    if (taskReactors.Contains(reactors[i]) == false)
                    {
                        taskReactors.Add(reactors[i]); 
                    }
                }
            }
        }
    }

    private void Start()
    {
        if (Instance == this)
        {
            if (scenarioOverride != null)
            {
                tasks = scenarioOverride.Tasks;
            }

            foreach (Task task in tasks)
            {
                TaskTracking.TryAdd(task, new TaskMetrics());
            }
        }
    }


    /// <summary>
    /// Tells the manager that the task was attempted.
    /// </summary>
    public static void SubmitTaskAttempted(int taskIndex)
    {
        if (Instance == null || Instance.TasksComplete)
            return;

        if (Instance.currentTask == taskIndex) //Correct task attempted
        {
            Debug.Log("Correct Task Attempted!");
            Instance.TaskTracking[Instance.CurrentTask].Attempts += 1;
            Instance.taskReactors.ForEach(reactor => reactor.OnCorrectTaskAttempted(Instance.currentTask));
        }
        else //Incorrect task attempted
        {
            //Uncomment line below if attempts should be added for the incorrect task
            //Instance.TaskTracking[GetTask(taskIndex)].Attempts += 1;
            Instance.TaskTracking[Instance.CurrentTask].IncorrectAttempts += 1;
            Instance.taskReactors.ForEach(reactor => reactor.OnIncorrectTaskAttempted(Instance.currentTask));
            Debug.LogFormat("Incorrect Task ({0}) Attempted!", taskIndex);
        }
    }

    /// <summary>
    /// Tells the manager that the task was completed, and returns if its appropriate to consider complete.
    /// </summary>
    public static bool SubmitTaskCompleted(int taskIndex)
    {
        if (Instance == null || Instance.TasksComplete)
            return false;

        //Successful
        if (Instance.currentTask == taskIndex)
        {
            Instance.CompleteCurrentTask();
            Instance.taskReactors.ForEach(reactor => reactor.OnCorrectTaskCompleted(Instance.currentTask - 1));
            return true;
        }
        else //Failed
        {
            Instance.IncorrectTaskAttempted(taskIndex);
            Instance.taskReactors.ForEach(reactor => reactor.OnIncorrectTaskCompleted(Instance.currentTask));
            return false;
        }
    }

    private void CompleteCurrentTask()
    {
        Debug.Log(string.Format("({0}) {1} has been completed!", currentTask, tasks[currentTask].TaskName));
        OnTaskCompleted?.Invoke(currentTask);
        TaskTracking[CurrentTask].TimeToComplete = Time.time - timeTaskStarted;
        currentTask = Mathf.Clamp(currentTask + 1, 0, tasks.Count);
        timeTaskStarted = Time.time;

        if (TasksComplete)
            CompleteScenario();
    }

    private void IncorrectTaskAttempted(int taskIndex)
    {
        Debug.LogError(string.Format("Incorrect Task ({0}) was attempted instead of {1}!", taskIndex, currentTask));
        OnTaskFailed?.Invoke(currentTask);
    }

    private void CompleteScenario()
    {
        Debug.Log("Scenario is Complete!");

        //Save
        ScenarioMetricSaver.SaveData(Instance.TaskTracking, scenarioOverride);

        taskReactors.ForEach(reactor => reactor.OnScenarioComplete());
    }
}
