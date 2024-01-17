#include <sys/types.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <stdlib.h>
#include "pipe.h"

#define READ  0
#define WRITE 1

int ExecPipe(char*** cmds) 
{
  pid_t pid;
  int fd[2];
  int nextfd = 0;

  while(*cmds != NULL)
  {

    if (pipe(fd) == -1) 
    {
      perror("Creating pipe");
      exit(EXIT_FAILURE);
    }

    switch(pid = fork()) 
    {
      case 0:
        dup2(nextfd, STDIN_FILENO);
        if (*(cmds + 1) != NULL)
        {
          dup2(fd[WRITE], STDOUT_FILENO);
        }
        close(fd[READ]);
        execvp(cmds[0][0], cmds[0]);

      case -1:
        perror("fork() failed)");
        exit(EXIT_FAILURE);

      default:
        wait(NULL);
        close(fd[WRITE]);
        nextfd = fd[READ];
        cmds++;
    }
  }
}
