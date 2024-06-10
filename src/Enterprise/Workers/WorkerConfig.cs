namespace Enterprise.Workers;

public record WorkerConfig(string WorkerId, bool CommunityWorker = false);
