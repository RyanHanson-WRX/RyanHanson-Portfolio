class Nonaggressive:
    def __init__(self, job_list, zero_priority, one_priority, two_priority, three_priority, four_priority, five_priority, six_priority, seven_priority):
        self._queue0 = []
        self._queue1 = []
        self._queue2 = []
        self._queue3 = []
        self._queue4 = []
        self._queue5 = []
        self._queue6 = []
        self._queue7 = []
        self._jobs = job_list
        self._zero = zero_priority
        self._one = one_priority
        self._two = two_priority
        self._three = three_priority
        self._four = four_priority
        self._five = five_priority
        self._six = six_priority
        self._seven = seven_priority

    def AddIncomingJobs(self, clock):
        for i in range(0, len(self._jobs)):
            if self._jobs[i].GetArrival == clock:
                self._jobs[i].SetState("Waiting - Ready To Run")
                curr_priority = self._jobs[i].GetPriority
                match(curr_priority):
                    case 0:
                        self._queue0.append(self._jobs[i])
                    case 1:
                        self._queue1.append(self._jobs[i])
                    case 2:
                        self._queue2.append(self._jobs[i])
                    case 3:
                        self._queue3.append(self._jobs[i])
                    case 4:
                        self._queue4.append(self._jobs[i])
                    case 5:
                        self._queue5.append(self._jobs[i])
                    case 6:
                        self._queue6.append(self._jobs[i])
                    case 7:
                        self._queue7.append(self._jobs[i])
                    case _:
                        raise Exception("Unknown Priority. Priorities must be an integer within the range of 0 to 7")
    
    def RequeueJob(self, job):
        curr_priority = job.GetPriority
        match(curr_priority):
            case 0:
                self._queue0.append(job)
            case 1:
                self._queue1.append(job)
            case 2:
                self._queue2.append(job)
            case 3:
                self._queue3.append(job)
            case 4:
                self._queue4.append(job)
            case 5:
                self._queue5.append(job)
            case 6:
                self._queue6.append(job)
            case 7:
                self._queue7.append(job)
            case _:
                raise Exception("Unknown Priority. Priorities must be an integer within the range of 0 to 7")

    def RequeueJobAfterTimeSlice(self, job):
        curr_priority = job.GetPriority
        if curr_priority != 7:
            job.SetPriority(curr_priority + 1)
            curr_priority += 1
        match(curr_priority):
            case 0:
                job.SetTimeSlice(self._zero)
                self._queue0.append(job)
            case 1:
                job.SetTimeSlice(self._one)
                self._queue1.append(job)
            case 2:
                job.SetTimeSlice(self._two)
                self._queue2.append(job)
            case 3:
                job.SetTimeSlice(self._three)
                self._queue3.append(job)
            case 4:
                job.SetTimeSlice(self._four)
                self._queue4.append(job)
            case 5:
                job.SetTimeSlice(self._five)
                self._queue5.append(job)
            case 6:
                job.SetTimeSlice(self._six)
                self._queue6.append(job)
            case 7:
                job.SetTimeSlice(self._seven)
                self._queue7.append(job)
            case _:
                raise Exception("Unknown Priority. Priorities must be an integer within the range of 0 to 7")

    def NextJob(self):
        if self._queue0:
            return self._queue0[0]
        else:
            if self._queue1:
                return self._queue1[0]
            else:
                if self._queue2:
                    return self._queue2[0]
                else:
                    if self._queue3:
                        return self._queue3[0]
                    else:
                        if self._queue4:
                            return self._queue4[0]
                        else:
                            if self._queue5:
                                return self._queue5[0]
                            else:
                                if self._queue6:
                                    return self._queue6[0]
                                else:
                                    if self._queue7:
                                        return self._queue7[0]
                                    else:
                                        return "No jobs ready to run"
    def GetCurrentJob(self):
        if self._queue0:
            current_job = self._queue0.pop(0)
        else:
            if self._queue1:
                current_job = self._queue1.pop(0)
            else:
                if self._queue2:
                    current_job = self._queue2.pop(0)
                else:
                    if self._queue3:
                        current_job = self._queue3.pop(0)
                    else:
                        if self._queue4:
                            current_job = self._queue4.pop(0)
                        else:
                            if self._queue5:
                                current_job = self._queue5.pop(0)
                            else:
                                if self._queue6:
                                    current_job = self._queue6.pop(0)
                                else:
                                    if self._queue7:
                                        current_job = self._queue7.pop(0)
                                    else:
                                        current_job = "IDLE PROCESS"
        return current_job
        
    def UpdateQueue(self):
        for i in range(0, len(self._queue0)):
            curr_state = self._queue0[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue0[i].UpdateWaitingReadyToRunTime(1)
                    self._queue0[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue1)):
            curr_state = self._queue1[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue1[i].UpdateWaitingReadyToRunTime(1)
                    self._queue1[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue2)):
            curr_state = self._queue2[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue2[i].UpdateWaitingReadyToRunTime(1)
                    self._queue2[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue3)):
            curr_state = self._queue3[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue3[i].UpdateWaitingReadyToRunTime(1)
                    self._queue3[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue4)):
            curr_state = self._queue4[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue4[i].UpdateWaitingReadyToRunTime(1)
                    self._queue4[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue5)):
            curr_state = self._queue5[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue5[i].UpdateWaitingReadyToRunTime(1)
                    self._queue5[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue6)):
            curr_state = self._queue6[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue6[i].UpdateWaitingReadyToRunTime(1)
                    self._queue6[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")
        for i in range(0, len(self._queue7)):
            curr_state = self._queue7[i].GetState
            match (curr_state):
                case "Waiting - Ready To Run":
                    self._queue7[i].UpdateWaitingReadyToRunTime(1)
                    self._queue7[i].UpdateElapsedTime()
                case _:
                    raise Exception("Something Went Wrong")