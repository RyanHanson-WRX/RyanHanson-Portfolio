#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <stdio.h>
#include <fcntl.h>
#include <string.h>
#include <errno.h>
#include <stdlib.h>
#include "redirect.h"

#define READ  0
#define WRITE 1

void PipeToFile(char*** cmds) 
{
  pid_t pid;
  int fd[2];
  int nextfd = 0;

  while (*cmds != NULL)
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
        if (*(cmds + 2) == NULL)
        {
          const char* fileName = *(cmds)[1];
          FILE* file = fopen(fileName, "w+");
          fclose(file);
          int toFile = open(fileName, O_WRONLY | O_CREAT);
          dup2(toFile, STDOUT_FILENO);
          close(fd[READ]);
          execvp(cmds[0][0], cmds[0]);
          close(toFile);
          cmds = NULL;
          exit(EXIT_SUCCESS);
        }
        else if (*(cmds + 1) == NULL)
        {
          exit(EXIT_SUCCESS);
        }
        else
        {
          dup2(fd[WRITE], STDOUT_FILENO);
        }
        close(fd[READ]);
        execvp(cmds[0][0], cmds[0]);

      case -1:
        perror("fork() failed");
        exit(EXIT_FAILURE);

      default:
        wait(NULL);
        close(fd[WRITE]);
        nextfd = fd[READ];
        cmds++;
    }
  }
}
