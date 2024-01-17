
class Job:
    def __init__(self, pid, arrival_time, service_time, priority, time_slice):
        self._pid = int(pid)
        self._arrival_time = int(arrival_time)
        self._service_time = int(service_time)
        self._init_priority = int(priority)
        self._priority = int(priority)
        self._state = "New Job"
        self._total_elapsed_time = 0
        self._time_waiting_ready_to_run = 0
        self._time_waiting_io = 0
        self._time_running = 0
        self._current_time = 0
        self._time_slice = time_slice
        self._expected_burst = 10
    @property
    def GetTotalWaitTime(self):
        return self._time_waiting_io + self._time_waiting_ready_to_run
    @property
    def GetPid(self):
        return self._pid
    @property
    def GetArrival(self):
        return self._arrival_time
    @property
    def GetService(self):
        return self._service_time
    @property
    def GetPriority(self):
        return self._priority
    @property
    def GetInitPriority(self):
        return self._init_priority
    @property
    def GetState(self):
        return self._state
    @property
    def GetElapsedTime(self):
        return self._total_elapsed_time
    @property
    def GetWaitingReadyToRunTime(self):
        return self._time_waiting_ready_to_run
    @property
    def GetWaitingIOTime(self):
        return self._time_waiting_io
    @property
    def GetRunningTime(self):
        return self._time_running
    @property
    def GetCurrentTime(self):
        return self._current_time
    @property
    def GetTimeSlice(self):
        return self._time_slice
    @property
    def GetExpectedBurst(self):
        return self._expected_burst
    def SetService(self, new_service):
        self._service_time = new_service
    def SetPriority(self, new_priority):
        self._priority = new_priority
    def SetState(self, new_state):
        self._state = new_state
    def UpdateElapsedTime(self):
        self._total_elapsed_time = self._time_waiting_io + self._time_running + self._time_waiting_ready_to_run
    def UpdateWaitingReadyToRunTime(self, new_time):
        self._time_waiting_ready_to_run += new_time
    def UpdateWaitingIOTime(self, new_time):
        self._time_waiting_io += new_time
    def UpdateRunningTime(self, new_time):
        self._time_running += new_time
    def ResetCurrentTime(self):
        self._current_time = 0
    def UpdateCurrentTime(self, new_time):
        self._current_time += new_time
    def SetTimeSlice(self, new_slice):
        self._time_slice = new_slice
    def SetExpectedBurst(self, burst):
        self._expected_burst = burst
