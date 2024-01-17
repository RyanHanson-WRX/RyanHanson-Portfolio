#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <fcntl.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <ctype.h>
#include <stdlib.h>
#include "pipe.h"
#include "redirect.h"

void RMTrailingWS(char* string)
{
  char* end = string + strlen(string) - 1;
  while (end > string && isspace((unsigned char) * end))
  {
    end--;
  }
  *(end + 1) = 0;
}

char NextDelim(char* cmdString, char** delims, int* delimSize)
{
  if (strchr(cmdString, ';') != NULL || strchr(cmdString, '|') != NULL || strchr(cmdString, '>') != NULL || strchr(cmdString, '&') != NULL)
  {
    char pipe[2] = "|";
    char semi[2] = ";";
    char redirect[2] = ">";
    char amp[2] = "&";
    char* delimiter;
    char* semiChar, *pipeChar, *redirectChar, *ampChar;
    int semiIndex = -1;
    int pipeIndex = -1;
    int redirectIndex = -1;
    int ampIndex = -1;
    if (strchr(cmdString, ';') != NULL)
    {
      semiChar = strchr(cmdString, ';');
      semiIndex = (int)(semiChar - cmdString);
    }
    if (strchr(cmdString, '|') != NULL)
    {
      pipeChar = strchr(cmdString, '|');
      pipeIndex = (int)(pipeChar - cmdString);
    }
    if (strchr(cmdString, '>') != NULL)
    {
      redirectChar = strchr(cmdString, '>');
      redirectIndex = (int)(redirectChar - cmdString);
    }
    if (strchr(cmdString, '&') != NULL)
    {
      ampChar = strchr(cmdString, '&');
      ampIndex = (int)(ampChar - cmdString);
    }
    if (semiIndex != -1 && pipeIndex != -1 && redirectIndex != -1 && ampIndex != -1)
    {
      if (semiIndex < pipeIndex && semiIndex < redirectIndex && semiIndex < ampIndex)
      {
        delimiter = semi;
      }
      else if (redirectIndex < pipeIndex && redirectIndex < semiIndex && redirectIndex < ampIndex)
      {
        delimiter = redirect;
      }
      else if (pipeIndex < redirectIndex && pipeIndex < semiIndex && pipeIndex < ampIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (semiIndex == -1 && redirectIndex == -1 && ampIndex == -1)
    {
      delimiter = pipe;
    }
    else if (pipeIndex == -1 && redirectIndex == -1 && ampIndex == -1)
    {
      delimiter = semi;
    }
    else if (pipeIndex == -1 && semiIndex == -1 && ampIndex == -1)
    {
      delimiter = redirect;
    }
    else if (pipeIndex == -1 && semiIndex == -1 && redirectIndex == -1)
    {
      delimiter = amp;
    }
    
    else if (semiIndex == -1 && redirectIndex == -1)
    {
      if (pipeIndex < ampIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (pipeIndex == -1 && redirectIndex == -1)
    {
      if (semiIndex < ampIndex)
      {
        delimiter = semi;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (ampIndex == -1 && redirectIndex == -1)
    {
      if (pipeIndex < semiIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = semi;
      }
    }
    else if (pipeIndex == -1 && semiIndex == -1 )
    {
      if (redirectIndex < ampIndex)
      {
        delimiter = redirect;
      }
      else
      {
        delimiter = amp;
      }
    }
  else if (ampIndex == -1 && semiIndex == -1)
    {
      if (pipeIndex < redirectIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = redirect;
      }
    }
    else if (pipeIndex == -1 && ampIndex == -1)
    {
      if (semiIndex < redirectIndex)
      {
        delimiter = semi;
      }
      else
      {
        delimiter = redirect;
      }
    }
    else if (semiIndex == -1)
    {
      if ( pipeIndex < redirectIndex && pipeIndex < ampIndex)
      {
        delimiter = pipe;
      }
      else if (redirectIndex < pipeIndex && redirectIndex < ampIndex)
      {
        delimiter = redirect;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (pipeIndex == -1)
    {
      if (semiIndex < redirectIndex && semiIndex < ampIndex)
      {
        delimiter = semi;
      }
      else if (redirectIndex < semiIndex && redirectIndex < ampIndex)
      {
        delimiter = redirect;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (redirectIndex == -1)
    {
      if (semiIndex < pipeIndex && semiIndex < ampIndex)
      {
        delimiter = semi;
      }
      else if (pipeIndex < semiIndex && pipeIndex < ampIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = amp;
      }
    }
    else if (ampIndex == -1)
    {
      if (semiIndex < pipeIndex && semiIndex < redirectIndex)
      {
        delimiter = semi;
      }
      else if (pipeIndex < semiIndex && pipeIndex < redirectIndex)
      {
        delimiter = pipe;
      }
      else
      {
        delimiter = redirect;
      }
    }
    *delims[*delimSize] = *delimiter;
    *delimSize += 1;
    return *delimiter;
  }
}

char* CommandParser(char* command, char** commands, int* cmdSize, char** delims, int* delimSize)
{
  char* token;
  char* delim = malloc(sizeof(char));
  char* endOfCmd;
  int j = 0;
  *delim = NextDelim(command, delims, delimSize);
  if (strchr(command, '|') != NULL || strchr(command, ';') != NULL || strchr(command, '>') != NULL || strchr(command, '&') != NULL)
  {
    endOfCmd = strchr(command, *delim);
    if (endOfCmd != NULL)
    {
      endOfCmd = endOfCmd + 1;
    }
    token = strtok(command, delim);
    while (token != NULL)
    { 
      if (token[0] == ' ')
      {
        for (int i = 0; i < strlen(token); i++)
        {
          token[i] = token[i+1];
        }
      }
      RMTrailingWS(token);
      commands[j] = token;
      j += 1;
      if (delim != NULL)
        *delim = NextDelim(endOfCmd, delims, delimSize);
        if (delim != NULL)
        {
          endOfCmd = strchr(endOfCmd, *delim);
          if (endOfCmd != NULL)
          {
            endOfCmd = endOfCmd + 1;
          }
          token = strtok(NULL, delim);
        }
    }
    token = NULL;
    *cmdSize = j;
    free(delim);
  }
}

char* ExecCommandParser(char* command, char** cmdWithArgs, int* argCount, int argIndex)
{
  char *args[16];
  int args_num = 0;
  char *token;
  char* cmdCopy = strdup(command);
  for (int j = 0; ; j++, cmdCopy = NULL) {
            token = strtok(cmdCopy, " ");
            if (token == NULL)
                break;
            args[j] = token;
            args_num += 1;
            argCount[argIndex] = args_num;
            cmdWithArgs[j] = args[j];  
        }
}

int main() 
{
  char input[256];
  char* commands[16];
  char*** cmdWithArgs;
  char** delims;
  int* cmdSize;
  int* delimSize;
  int* argCount;
  int* argCountIndex;
  argCountIndex = malloc(sizeof(int));
  cmdWithArgs = malloc(17 * sizeof(char*));
  delimSize = malloc(sizeof(int));
  cmdSize = malloc(sizeof(int));
  delims = malloc(16 * sizeof(char*));
  argCount = malloc(17 * sizeof(int));
  for (int i = 0; i < 16; i++)
  {
    delims[i] = malloc(2 * sizeof(char));
  }
  for (int i = 0; i < 17; i++)
  {
    cmdWithArgs[i] = malloc(17 * sizeof(char));
  }
  *delimSize = 0;
  *cmdSize = 0;
  *argCount = 0;
  printf("ry_sh>> $ ");
  fgets(input, 256, stdin);
  if (strchr(input, ';') == NULL && strchr(input, '|') == NULL && strchr(input, '>') == NULL && strchr(input, '&') == NULL)
  {
    RMTrailingWS(input);
    ExecCommandParser(input, cmdWithArgs[0], argCount, 0);
    execvp(cmdWithArgs[0][0], cmdWithArgs[0]);
    return 0;
  }
  CommandParser(input, commands, cmdSize, delims, delimSize);
  for (int i = 0; i < *cmdSize; i++)
  {
    ExecCommandParser(commands[i], cmdWithArgs[i], argCount, i);
  }

  for (int i = 0; i < *cmdSize; i++)
  {
    if (*delims[i] == '|')
    {
      int cmdsIndex = 0;
      char** cmds[17];
      while (*delims[i] == '|')
      {
        cmds[cmdsIndex] = cmdWithArgs[i];
        cmds[cmdsIndex + 1]= cmdWithArgs[i + 1];
        cmdsIndex++;
        if (i < *delimSize)
        {
          i++;
        }
      }
      if (*delims[i] == '>')
      {
        cmds[cmdsIndex] = cmdWithArgs[i];
        cmds[cmdsIndex + 1] = cmdWithArgs[i + 1];
        cmdsIndex++;
        i++;
        pid_t pid;
        switch(pid = fork())
        {
          case 0:
            PipeToFile(cmds);
            exit(EXIT_SUCCESS);
          case -1:
            perror("fork() failed");
            exit(EXIT_FAILURE);
          default:
            wait(NULL);
            for (int x = 0; x < 17; x++)
            {
              cmds[x] = NULL;
            }
            continue;
        }
      }
      else
      {
        pid_t pid;
        switch(pid = fork())
        {
          case 0:
            ExecPipe(cmds);
            exit(EXIT_SUCCESS);
          case -1:
            perror("fork() failed");
            exit(EXIT_FAILURE);
          default:
            wait(NULL);
            for (int x = 0; x < 17; x++)
            {
              cmds[x] = NULL;
            }
            continue;
        }
      }
    }
    else if (i < *delimSize && *delims[i] == '>')
    {
      if (i != 0 && *delims[i - 1] == '>')
      {
        perror("Redirect failed: can't redirect a redirect");
        exit(EXIT_FAILURE);
      }
      else
      {
        pid_t pid;
        switch(pid = fork())
        {
          case 0:
            char* fileName = cmdWithArgs[i + 1][0];
            FILE* file = fopen(fileName, "w+");
            fclose(file);
            int toFile = open(fileName, O_WRONLY | O_CREAT);
            dup2(toFile, STDOUT_FILENO);
            execvp(cmdWithArgs[i][0], cmdWithArgs[i]);
            close(toFile);
          case -1:
            perror("fork() failed");
            exit(EXIT_FAILURE);
          default:
            wait(NULL);
            i++;
            continue;
        }
      }
    }
    else if (*delims[i] == '&')
    {
      if (i > 0)
      {
        if (*delims[i - 1] == '|' || *delims[i - 1] == '>')
        {
          //ignores sending piped and redirects to the background
          continue;
        }
        else
        {
          pid_t pid;
          switch(pid = fork())
          {
            case 0:
              pid_t pid2;
              switch(pid2 = fork())
              {
                case 0:
                  sleep(15 + i);
                  FILE* file = fopen("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt", "w+");
                  fclose(file);
                  int backgroundGhostFile = open("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt", O_RDWR | O_CREAT);
                  dup2(backgroundGhostFile, STDOUT_FILENO);
                  execvp(cmdWithArgs[i][0], cmdWithArgs[0]);
                  close(backgroundGhostFile);
                case -1:
                  perror("fork() failed");
                  exit(EXIT_FAILURE);
                default:
                  wait(NULL);
                  remove("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt");
                  exit(EXIT_SUCCESS);
              }
            case -1:
              perror("fork() failed");
              exit(EXIT_FAILURE);
            default:
              continue;
          }
        }
      }
      else
      {
        pid_t pid;
        switch(pid = fork())
        {
          case 0:
            pid_t pid2;
            switch(pid2 = fork())
            {
              case 0:
                sleep(10);
                FILE* file = fopen("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt", "w+");
                fclose(file);
                int backgroundGhostFile = open("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt", O_RDWR | O_CREAT);
                dup2(backgroundGhostFile, STDOUT_FILENO);
                execvp(cmdWithArgs[i][0], cmdWithArgs[0]);
                close(backgroundGhostFile);
              case -1:
                perror("fork() failed");
                exit(EXIT_FAILURE);
              default:
                wait(NULL);
                remove("b_a_c_k_g_r_o_u_n_d_G_h_o_s_t_F_i_l_e.txt");
                exit(EXIT_SUCCESS);
            }
          case -1:
            perror("fork() failed");
            exit(EXIT_FAILURE);
          default:
            continue;
        }
      }
    }
    else if (*delims[i] == ';')
    {
      if (i == 0)
      {
        pid_t pid;
        switch(pid = fork())
        {
          case 0:
            execvp(cmdWithArgs[i][0], cmdWithArgs[i]);
            exit(EXIT_SUCCESS);
          case -1:
            perror("fork() failed");
            exit(EXIT_FAILURE);
          default:
            wait(NULL);
            continue;
        }
      }
      else 
      {
        if (*delims[i - 1] == '|' || *delims[i - 1] == '>')
        {
          continue;
        }
        else
        {
          pid_t pid;
          switch(pid = fork())
          {
            case 0:
              execvp(cmdWithArgs[i][0], cmdWithArgs[i]);
              exit(EXIT_SUCCESS);
            case -1:
              perror("fork() failed");
              exit(EXIT_FAILURE);
            default:
              wait(NULL);
              continue;
          }
        }
      }
    }
    else
    {
      if (i == (*delimSize) && (*delims[i - 1] != '|' || *delims[i - 1] != '>'))
      {
          pid_t pid;
          switch(pid = fork())
          {
            case 0:
              execvp(cmdWithArgs[i][0], cmdWithArgs[i]);
              exit(EXIT_SUCCESS);
            case -1:
              perror("fork() failed");
              exit(EXIT_FAILURE);
            default:
              wait(NULL);
              continue;
          }
        continue;
      }
      else
      {
        continue;
      }
    }
  }

  free(delimSize);
  free(cmdSize);
  for (int i = 0; i < 16; i++)
  {
    free(delims[i]);
  }
  for (int i = 0; i < 17; i++)
  {
    free(cmdWithArgs[i]);
  }
  free(argCount);
  free(delims);
  free(cmdWithArgs);
  return 0;
}