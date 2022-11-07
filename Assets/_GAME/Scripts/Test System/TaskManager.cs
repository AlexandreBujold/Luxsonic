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

    private void Awake()
    {
        Instance = Instance == null ? this : Instance;

        //Get any missing child reactors
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<TaskReactor>(out TaskReactor reactor))
            {
                if (taskReactors.Contains(reactor) == false)
                    taskReactors.Add(reactor);
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

    /*
     * Attempted: means the task was attempted but was not completed for whatever reason
     * Incorrect: means a task was completed, but was the incorrect task so was not considered
     * Completed: means a task was completed, and was the correct one
     */

    /// <summary>
    /// Tells the manager that the task was attempted.
    /// </summary>
    public static void SubmitTaskAttempted(int taskIndex)
    {
        if (Instance == null)
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
            Debug.Log("Incorrect Task Attempted!");
        }
    }

    /// <summary>
    /// Tells the manager that the task was completed, and returns if its appropriate to consider complete.
    /// </summary>
    public static bool SubmitTaskCompleted(int taskIndex)
    {
        if (Instance == null)
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

    private static Task GetTask(int index)
    {
        if (index < Instance.tasks.Count)
            return Instance.tasks[index];
        return null;
    }

    private void CompleteCurrentTask()
    {
        Debug.Log(string.Format("({0}) {1} has been completed!", currentTask, tasks[currentTask].TaskName));
        OnTaskCompleted?.Invoke(currentTask);
        TaskTracking[CurrentTask].TimeToComplete = Time.time - timeTaskStarted;
        currentTask = Mathf.Clamp(currentTask + 1, 0, tasks.Count - 1);
        timeTaskStarted = Time.time;
    }

    private void IncorrectTaskAttempted(int taskIndex)
    {
        Debug.LogError(string.Format("Incorrect Task ({0}) was attempted instead of {1}!", taskIndex, currentTask));
        OnTaskFailed?.Invoke(currentTask);
    }
}
