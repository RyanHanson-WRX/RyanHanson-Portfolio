import argparse
import time
from timeit import default_timer as timer
import random
import threading

def foodtruck(start_time, cust_id, customers, avg_wait):
    data_save.acquire()
    customers += 1
    data_save.release()
    screen.acquire()
    start_wait = timer()
    print("At time  {}, customer    {} arrives in line".format((int(timer() - start_time) * 10),  cust_id))
    screen.release()
    workers.acquire()
    screen.acquire()
    end_wait = timer()
    print("At time  {}, customer    {} starts being served".format((int(timer() - start_time) * 10),  cust_id))
    screen.release()
    deviation = random.uniform(-1.0, 1.0)
    sleep_time = (((args.service_time) / 10) + (((args.service_time) / 10) * deviation))
    time.sleep(sleep_time)
    screen.acquire()
    print("At time  {}, customer    {} leaves the food cart".format((int(timer() - start_time) * 10),  cust_id))
    screen.release()
    workers.release()
    data_save.acquire()
    customers -= 1
    curr_wait = (end_wait - start_wait)
    avg_wait.append(curr_wait)
    data_save.release()


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("number_of_workers", type=int, help="Number of workers for the simulation")
    parser.add_argument("customer_arrival", type=int, help="Mean time between customer arrivals (multiples of 10)")
    parser.add_argument("service_time", type=int, help="Mean service time (multiples of 10)")
    parser.add_argument("simulation_time", type=int, help="Length of time to run the simulation (multiples of 10)")
    args = parser.parse_args()

    print("Number of workers: {}".format(args.number_of_workers))
    print("Mean time between customer arrivals: {}".format(args.customer_arrival))
    print("Mean service time: {}".format(args.service_time))
    print("Length of time to run the simulation: {}".format(args.simulation_time))
    print()

    workers = threading.Semaphore(args.number_of_workers)
    screen = threading.Semaphore(1)
    data_save = threading.Semaphore(1)
    customers = 0
    avg_wait = []
    all_customers = []
    cust_id = 0
    sum = 0
    sim_done = False
    start_time = timer()
    while sim_done == False:
        while ((timer() - start_time) * 10) < args.simulation_time:
            deviation = random.uniform(-1.0, 1.0)
            sleep_time = (((args.customer_arrival) / 10) + (((args.customer_arrival) / 10) * deviation))
            time.sleep(sleep_time)
            cust_id += 1
            curr_thread = threading.Thread(target=foodtruck, args=(start_time, cust_id, customers, avg_wait))
            all_customers.append(curr_thread)
            curr_thread.start()
        curr_time = ((timer() - start_time) * 10)
        if (curr_time >= args.simulation_time) and (customers == 0) and (len(avg_wait) == cust_id):
            sim_done = True
    if sim_done:
        for i in range(0, len(all_customers)):
            all_customers[i].join()
        for i in range(0, len(avg_wait)):
            sum = sum + avg_wait[i]
        avg = (sum / len(all_customers))
        print()
        print("Simulation terminated after {} customers were served".format(len(all_customers)))
        print("Average waiting time = {:.2f}".format(avg))