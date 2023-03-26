#!/bin/sh
#Ryan Hanson, PowerfulShellScript2, IS301

#Must use '\' for character delimiters that are special: "/\.*[]^$; etc"
#Argument Syntax: arg1: data delimiter pattern, arg2: replacement
# delimiter pattern, arg3: source text filename

#Change all occurrences of delimiter pattern to new replacement delimiter
# pattern
sed -e 's/\'"$1"'/\'"$2"'/g' <$3 >IS301_outputText.txt

#Output all input lines with the new replacement delimiter pattern applied
cat IS301_outputText.txt | sed -n 's/\'"$2"'/&/p'
