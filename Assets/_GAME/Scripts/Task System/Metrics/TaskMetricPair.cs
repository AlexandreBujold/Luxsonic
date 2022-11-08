[System.Serializable]
public class TaskMetricPair
{
    public Task Task = null;
    public TaskMetrics Metrics = null;

    public TaskMetricPair(Task task, TaskMetrics metrics)
    {
        Task = task;
        Metrics = metrics;
    }
}