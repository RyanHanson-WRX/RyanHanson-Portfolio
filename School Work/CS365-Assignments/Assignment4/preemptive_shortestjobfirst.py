class SJF:
    def __init__(self, job_list):
        self._queue = []
        self._job_list = job_list
        self._default_weight = 0.5
        self._weight = 5
        self._previous_burst_weight = 5
    @property
    def GetQueue(self):
        return self._queue
    def AddIncomingJobs(self, clock):
        for i in range(0, len(self._job_list)):
            if self._job_list[i].GetArrival == clock:
                self._job_list[i].SetState("Waiting - Ready To Run")
                #setting time slice to bypass timeslice logic in coordinator which is used for the other algorithms.
                self._job_list[i].SetTimeSlice(10000000)
                self._queue.append(self._job_list[i])
        self.CalcBursts()
    
    def RequeueJob(self, job):
        self._queue.append(job)
        self.CalcBursts()
    def CalcBursts(self):
        for i in range(0, len(self._queue) - 1):
            if i == 0:
                self._queue[i].SetPriority(self._weight + self._previous_burst_weight)
            self._weight = self._default_weight * self._queue[i].GetService
            self._previous_burst_weight = (1 - self._default_weight) * self._queue[i].GetExpectedBurst
            self._queue[i + 1].SetExpectedBurst((self._weight) + (self._previous_burst_weight))
            self._queue[i + 1].SetPriority(self._queue[i + 1].GetExpectedBurst)
        self._queue.sort(key=lambda job: job.GetExpectedBurst)

    def NextJob(self):
        if self._queue:
            return self._queue[0]
        else:
            return "No jobs ready to run"
        
    def GetCurrentJob(self):
        if self._queue:
            return self._queue.pop(0)
        else:
            return "IDLE PROCESS"
        
    def UpdateQueue(self):
        if len(self._queue) > 0:
            for i in range(0, len(self._queue)):
                curr_state = self._queue[i].GetState
                match (curr_state):
                    case "Waiting - Ready To Run":
                        self._queue[i].UpdateWaitingReadyToRunTime(1)
                        self._queue[i].UpdateElapsedTime()
                    case _:
                        raise Exception("Something Went Wrong")