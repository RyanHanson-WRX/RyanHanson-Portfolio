from job import Job
from preemptive_shortestjobfirst import SJF
from nonaggressive_scheduler import Nonaggressive
from aggressive_scheduler import Aggressive
import argparse
import random

class CoordStats:
    def __init__(self):
        self._number_of_jobs = 0
        self._total_elapsed_time = 0
        self._total_elapsed_time_per_job = {}
        self._wait_time_per_job = []
        self._longest_job_time = 0
        self._shortest_job_time = 1
        self._runs = []
    @property
    def GetAvgWaitTime(self):
        avg_wait = 0
        num_of_jobs = len(self._wait_time_per_job)
        for i in range(0, len(self._wait_time_per_job)):
            avg_wait += self._wait_time_per_job[i][1]
        total_avg_wait = avg_wait / num_of_jobs
        return total_avg_wait
    @property
    def GetAvgRunningTime(self):
        avg_run = 0
        num_of_jobs = len(self._runs)
        for i in range(0, len(self._runs)):
            avg_run += self._runs[i][1]
        total_avg_run = avg_run / num_of_jobs
        return total_avg_run            

    @property
    def GetNumberOfJobs(self):
        return self._number_of_jobs
    @property
    def GetTotalElapsedTime(self):
        return self._total_elapsed_time
    @property
    def GetTotalElapsedTimePerJob(self):
        return self._total_elapsed_time_per_job
    @property
    def GetWaitTimePerJob(self):
        return self._wait_time_per_job
    @property
    def GetLongetsJobTime(self):
        return self._longest_job_time
    @property
    def GetShortestJobTime(self):
        return self._shortest_job_time
    def UpdateNumberOfJobs(self, new_number):
        self._number_of_jobs += new_number
    def UpdateTotalElapsedTime(self, new_time):
        self._total_elapsed_time += new_time
    def UpdateTotalElapsedTimePerJob(self, job_pid, job_time):
        self._total_elapsed_time_per_job[job_pid] = job_time
    def UpdateRunTimePerJob(self,job_pid, run_time):
        curr_arr = [job_pid, run_time]
        self._runs.append(curr_arr)
    def UpdateWaitTimePerJob(self, job_pid, wait_time):
        curr_arr = [job_pid, wait_time]
        self._wait_time_per_job.append(curr_arr)
    def SetLongestJobTime(self, new_time):
        self._longest_job_time = new_time
    def SetShortestJobTime(self, new_time):
        self._shortest_job_time = new_time


def IO_Request():
    if random.randint(1, args.iorequest) % args.iorequest == 0:
        return 1
    else:
        return 0

def IO_Complete():
    if random.randint(1, args.iocomplete) % args.iocomplete == 0:
        return 1
    else:
        return 0    

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("-v", "--verbose", action="store_true", default=False, help="Verbose Output")
    parser.add_argument("-A", "--aggressive", action="store_true", default=False, help="Aggressive Scheduler")
    parser.add_argument("-N", "--nonaggressive", action="store_true", default=True, help="Nonaggressive Scheduler")
    parser.add_argument("-S", "--shortestjobfirst", action="store_true", default=False, help="Preemptive Shortest Job First Scheduler")
    parser.add_argument("-0", "--zero", type=int, default=1, help="Priority Queue 0")
    parser.add_argument("-1", "--one", type=int, default=2, help="Priority Queue 1")
    parser.add_argument("-2", "--two", type=int, default=4, help="Priority Queue 2")
    parser.add_argument("-3", "--three", type=int, default=8, help="Priority Queue 3")
    parser.add_argument("-4", "--four", type=int, default=16, help="Priority Queue 4")
    parser.add_argument("-5", "--five", type=int, default=32, help="Priority Queue 5")
    parser.add_argument("-6", "--six", type=int, default=64, help="Priority Queue 6")
    parser.add_argument("-7", "--seven", type=int, default=128, help="Priority Queue 7")
    parser.add_argument("-r", "--iorequest", type=int, default=10, help="I/O Request Chance")
    parser.add_argument("-c", "--iocomplete", type=int, default=4, help="I/O Completion Chance")
    parser.add_argument("-s", "--seed", type=int, default=1, help="Random Seed")
    parser.add_argument("inputfile", type=str)

    args = parser.parse_args()
    if args.aggressive == True or args.shortestjobfirst == True:
        args.nonaggressive = False
    if args.iorequest <= 1:
        raise Exception("I/O Request Chance must be a value larger than 1.")
    print("_"*100)
    print("Verbose Output: {}".format(args.verbose))
    print("Aggressive Scheduler: {}".format(args.aggressive))
    print("Nonaggressive Scheduler: {}".format(args.nonaggressive))
    print("PreemptiveSJF Scheduler: {}".format(args.shortestjobfirst))
    print("0 Priority Queue Time Slice: {}".format(args.zero))
    print("1 Priority Queue Time Slice: {}".format(args.one))
    print("2 Priority Queue Time Slice: {}".format(args.two))
    print("3 Priority Queue Time Slice: {}".format(args.three))
    print("4 Priority Queue Time Slice: {}".format(args.four))
    print("5 Priority Queue Time Slice: {}".format(args.five))
    print("6 Priority Queue Time Slice: {}".format(args.six))
    print("7 Priority Queue Time Slice: {}".format(args.seven))
    print("Seed Value: {}".format(args.seed))
    print("I/O Request Chance: {}".format(args.iorequest))
    print("I/O Completion Chance: {}".format(args.iocomplete))
    print("Input File Name: {}".format(args.inputfile))
    print("_"*100)
    print()
    data = []
    with open(args.inputfile, "r") as file:
        for line in file.readlines():
            row = line.strip().split(':')
            job = [0, 0, 0, 0]
            for i in range(0, len(row)):
                job[i] = row[i].strip()
            data.append(job)
    jobs = []
    for i in range(0, len(data)):
        curr_priority = int(data[i][3])
        match(curr_priority):
            case 0:
                time_slice = args.zero
            case 1:
                time_slice = args.one
            case 2:
                time_slice = args.two
            case 3:
                time_slice = args.three
            case 4:
                time_slice = args.four
            case 5:
                time_slice = args.five
            case 6:
                time_slice = args.six
            case 7:
                time_slice = args.seven
             
        new_job = Job(data[i][0], data[i][1], data[i][2], data[i][3], time_slice)
        jobs.append(new_job)

if args.aggressive:
    scheduler = Aggressive(jobs, args.zero, args.one, args.two, args.three, args.four, args.five, args.six, args.seven)
elif args.shortestjobfirst:
    scheduler = SJF(jobs)
else:
    scheduler = Nonaggressive(jobs, args.zero, args.one, args.two, args.three, args.four, args.five, args.six, args.seven)

jobs_waiting_io = []

num_jobs = len(jobs)
             
clock = 0
scheduler.AddIncomingJobs(clock)
random.seed(args.seed)
stats = CoordStats()
while num_jobs > 0:
    current_job = scheduler.GetCurrentJob()
    if current_job != "IDLE PROCESS":
        current_job.ResetCurrentTime()
        current_job.UpdateCurrentTime(1)
        if args.nonaggressive or args.aggressive:
            current_job.SetPriority(current_job.GetInitPriority)
            match (current_job.GetPriority):
                                case 0:
                                    current_job.SetTimeSlice(args.zero)
                                case 1:
                                    current_job.SetTimeSlice(args.one)   
                                case 2:
                                    current_job.SetTimeSlice(args.two)
                                case 3:
                                    current_job.SetTimeSlice(args.three)
                                case 4:
                                    current_job.SetTimeSlice(args.four)
                                case 5:
                                    current_job.SetTimeSlice(args.five)
                                case 6:
                                    current_job.SetTimeSlice(args.six)
                                case 7:
                                    current_job.SetTimeSlice(args.seven)
        while(1):
            current_job.SetState("Running")
            if clock != 0:
                scheduler.AddIncomingJobs(clock)
            next_job = scheduler.NextJob()
            marked_swapped = False
            io_request_bool = False
            new_job_ready = False
            for i in range(0, len(jobs_waiting_io)):
                curr_job = jobs_waiting_io.pop(0)
                status = IO_Complete()
                if status == 1:
                    curr_job.SetState("Waiting - Ready To Run")
                    curr_job.UpdateWaitingIOTime(1)
                    if args.nonaggressive:
                        curr_job.SetPriority(0)
                        curr_job.SetTimeSlice(args.zero)
                        scheduler.RequeueJob(curr_job)
                    elif args.aggressive:
                        new_job_ready = True
                        scheduler.RequeueJobAfterIOBlock(curr_job)
                    else:
                        scheduler.RequeueJob(curr_job)
                else:
                    curr_job.UpdateWaitingIOTime(1)
                    jobs_waiting_io.append(curr_job)

            if args.aggressive and new_job_ready:
                    marked_swapped = True
                    current_job_loop_state = "Preempted"
                    current_job.SetState("Waiting - Ready To Run")
                    current_job.UpdateRunningTime(1)
                    current_job.UpdateElapsedTime()
                    if current_job.GetService == 1:
                        current_job.SetState("Terminated")
                        current_job_loop_state = "* Exited"
                        num_jobs = num_jobs - 1
                        stats.UpdateWaitTimePerJob(current_job.GetPid, current_job.GetTotalWaitTime)
                        stats.UpdateNumberOfJobs(1)
                        stats.UpdateTotalElapsedTimePerJob(current_job.GetPid, current_job.GetElapsedTime)
            else:
                if current_job.GetService == 1:
                    marked_swapped = True
                    io_request_bool = False
                    current_job.SetState("Terminated")
                    current_job_loop_state = "* Exited"
                    current_job.UpdateRunningTime(1)
                    current_job.UpdateElapsedTime()
                    num_jobs = num_jobs - 1
                    stats.UpdateWaitTimePerJob(current_job.GetPid, current_job.GetTotalWaitTime)
                    stats.UpdateNumberOfJobs(1)
                    stats.UpdateTotalElapsedTimePerJob(current_job.GetPid, current_job.GetElapsedTime)
                
                # For Preemptive SJF, GetPriority here reflects the expected burst time

                elif args.nonaggressive == False and next_job != "No jobs ready to run" and next_job.GetPriority < current_job.GetPriority:
                    marked_swapped = True
                    current_job_loop_state = "Preempted"
                    io_request_bool = False
                    current_job.UpdateRunningTime(1)
                    current_job.SetState("Waiting - Ready To Run")
                else:
                    if current_job.GetService > 1:
                        status = IO_Request()
                    else:
                        status = 0
                    if status == 1:
                        marked_swapped = True
                        io_request_bool = True
                        current_job_loop_state = "Sleeping"
                        current_job.SetState("Waiting - On I/O")
                        current_job.UpdateRunningTime(1)
                        jobs_waiting_io.append(current_job)
                    elif current_job.GetCurrentTime == current_job.GetTimeSlice:
                        marked_swapped = True
                        io_request_bool = False
                        current_job_loop_state = "Preempted"
                        current_job.SetState("Waiting - Ready To Run")
                        current_job.UpdateRunningTime(1)
                    else:
                        io_request_bool = False
                        current_job_loop_state = "Still Running"

            if args.verbose:
                if jobs_waiting_io:
                    io_jobs = []
                    for i in range(0, len(jobs_waiting_io)):
                        io_jobs.append(jobs_waiting_io[i].GetPid)
                    print("{}:{}:{}:{}:{}:{}\n".format(clock, current_job.GetPid, current_job.GetService - 1, io_request_bool, io_jobs, current_job_loop_state))
                else:
                    print("{}:{}:{}:{}:None:{}\n".format(clock, current_job.GetPid, current_job.GetService - 1, io_request_bool, current_job_loop_state))
            stats.UpdateTotalElapsedTime(1)
            if current_job.GetCurrentTime > stats.GetLongetsJobTime:
                stats.SetLongestJobTime(current_job.GetCurrentTime)
            elif current_job.GetCurrentTime < stats.GetShortestJobTime:
                stats.SetShortestJobTime(current_job.GetCurrentTime)

            scheduler.UpdateQueue()

            if marked_swapped:
                if io_request_bool:
                    clock += 1
                    stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                    break
                elif current_job.GetState == "Waiting - Ready To Run":
                    if args.aggressive:
                        if new_job_ready:
                            if current_job_loop_state == "* Exited":
                                stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                                clock += 1
                                break
                            else:
                                current_job.SetService((current_job.GetService - 1))
                                scheduler.RequeueJobAfterIOBlock(current_job)
                                stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                                clock += 1
                                break
                        else:
                            current_job.SetService((current_job.GetService - 1))
                            scheduler.RequeueJob(current_job)
                            stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                            clock += 1
                            break
                    elif (args.nonaggressive) and current_job.GetCurrentTime == current_job.GetTimeSlice:
                        current_job.SetService((current_job.GetService - 1))
                        scheduler.RequeueJobAfterTimeSlice(current_job)
                        stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                        clock += 1
                        break
                    else:
                        current_job.SetService((current_job.GetService - 1))
                        scheduler.RequeueJob(current_job)
                        stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                        clock += 1
                        break
                elif current_job_loop_state == "* Exited":
                    clock += 1
                    break
                else:
                    current_job.SetService((current_job.GetService - 1))
                    clock += 1
                    stats.UpdateRunTimePerJob(current_job.GetPid, current_job.GetCurrentTime)
                    break
            else:
                current_job.UpdateCurrentTime(1)
                current_job.UpdateRunningTime(1)
                current_job.SetService((current_job.GetService - 1))
            clock += 1

    else:
        scheduler.AddIncomingJobs(clock)
        for i in range(0, len(jobs_waiting_io)):
                curr_job = jobs_waiting_io.pop(0)
                status = IO_Complete()
                if status == 1:
                    curr_job.SetState("Waiting - Ready To Run")
                    curr_job.UpdateWaitingIOTime(1)
                    scheduler.RequeueJob(curr_job)
                else:
                    curr_job.UpdateWaitingIOTime(1)
                    jobs_waiting_io.append(curr_job)

        if args.verbose:
                if jobs_waiting_io:
                    io_jobs = []
                    for i in range(0, len(jobs_waiting_io)):
                        io_jobs.append(jobs_waiting_io[i].GetPid)
                    print("{}:*:x:False:{}:Idling\n".format(clock, io_jobs))
                else:
                    print("{}:*:x:False:None:Idling\n".format(clock))
        stats.UpdateTotalElapsedTime(1)
        clock += 1
print()
print("Summary for each Job's Scheduling State:")
print("_" * 101)
print("|___________________________________________________________________________________________________|")
print("|    PID:    |   Time Waiting    |   Time Waiting   |   Time Running:    |   Total Elapsed Time:    |")
print("|            |   Ready to Run:   |      On I/O:     |                    |                          |")
print("|____________|___________________|__________________|____________________|__________________________|")
for i in range(0, len(jobs)):
    print("|    {:<8}|         {:<10}|         {:<9}|          {:<10}|             {:<13}|".format(jobs[i].GetPid, jobs[i].GetWaitingReadyToRunTime, jobs[i].GetWaitingIOTime, jobs[i].GetRunningTime, stats.GetTotalElapsedTimePerJob[jobs[i].GetPid]))
print("|____________|___________________|__________________|____________________|__________________________|")
print("|___________________________________________________________________________________________________|")
print()
print("Stats:")
print("_" * 116)
print("|__________________________________________________________________________________________________________________|")
print("|   Number   |   Total Elapsed   |   Average Wait   |     Average Run    |     Longest Job    |    Shortest Job    |")
print("|   of Jobs: |       Time:       |       Time:      |        Time:       |        Time:       |       Time:        |")
print("|____________|___________________|__________________|____________________|____________________|____________________|")
print("|    {:<8}|        {:<11}|      {:<12}|         {:<10.2f} |         {:<11}|         {:<11}|".format(stats.GetNumberOfJobs, stats.GetTotalElapsedTime, stats.GetAvgWaitTime, stats.GetAvgRunningTime,stats.GetLongetsJobTime, stats.GetShortestJobTime))
print("|____________|___________________|__________________|____________________|____________________|____________________|")
print("|__________________________________________________________________________________________________________________|")
print()