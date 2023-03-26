#!/bin/sh
#Ryan Hanson, PowerfulShellScript1, IS301

#Argument Syntax: arg1: name pattern, arg2: source text filename

#Save occurrences of pattern to variable for further processing
count=$(grep -Ec $1 $2)

#Output occurrences found to stdout
echo "Occurrences of $1 found in $2 is: $count"

#Output lines that match pattern to stdout
grep -En $1 $2
