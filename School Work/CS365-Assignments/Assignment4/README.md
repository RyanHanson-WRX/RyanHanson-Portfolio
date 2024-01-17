### Created by Ryan Hanson
### How to Run:
- Clone repository/tag
- Open terminal/console and cd into `Assignment4` folder
- On the command line, run ` python3 my_coordinator.py [-v] [-A|-N|-S] [-r <int>] [-c <int>] [-s <int>] [-0 <int>] [-1 <int>] [-2 <int>] [-3 <int>] [-4 <int>] [-5 <int>] [-6 <int>] [-7 <int>] <input_file> `
- Example: `python3 my_coordinator.py -v -A -r 5 -c 10 scheduling_data.txt`
- Optional: Run ` python3 my_coordinator.py -h ` or ` python3 my_coordinator.py --help ` to show flags
    - Output of -h / --help flag:   
        usage: my_coordinator.py [-h] [-v] [-A] [-N] [-S] [-0 ZERO] [-1 ONE] [-2 TWO] [-3 THREE] [-4 FOUR] [-5 FIVE] [-6 SIX] [-7 SEVEN] [-r IOREQUEST]
                         [-c IOCOMPLETE] [-s SEED]
                         inputfile  

        positional arguments:  
        inputfile. 

        options:  
        -h, --help            show this help message and exit. 
        -v, --verbose         Verbose Output  
        -A, --aggressive      Aggressive Scheduler  
        -N, --nonaggressive   Nonaggressive Scheduler  
        -S, --shortestjobfirst  
                                Preemptive Shortest Job First Scheduler  
        -0 ZERO, --zero ZERO  Priority Queue 0  
        -1 ONE, --one ONE     Priority Queue 1  
        -2 TWO, --two TWO     Priority Queue 2  
        -3 THREE, --three THREE  
                                Priority Queue 3   
        -4 FOUR, --four FOUR  Priority Queue 4  
        -5 FIVE, --five FIVE  Priority Queue 5  
        -6 SIX, --six SIX     Priority Queue 6  
        -7 SEVEN, --seven SEVEN  
                                Priority Queue 7  
        -r IOREQUEST, --iorequest IOREQUEST  
                                I/O Request Chance  
        -c IOCOMPLETE, --iocomplete IOCOMPLETE  
                                I/O Completion Chance  
        -s SEED, --seed SEED  Random Seed    

- Note: changing I/O request value [-r flag] to a value such as 5 will show bigger differences in outputs (especially between aggressive and nonaggressive schedulers)
