script1 := "IS301_RyanHanson_PowerfulShellScript1.sh is a solution that will return the occurrences of a pattern and the lines on which the pattern was found from an input file. To use the deployed solution, unzip the package, and run: './IS301_RyanHanson_PowerfulShellScript1 <pattern> <input_filename>' in a CLI."
script2 := "IS301_RyanHanson_PowerfulShellScript2.sh is a solution that will change all occurences of a data delimiter to a new specified pattern from an input file and output the results to an output file. To use the deployed solution, unzip the package and run: './IS301_RyanHanson_PowerfulShellScript2 <old_pattern> <new_pattern> <input_filename>' in a CLI. Note: for new patterns that contain a special character, you must escape them with a '\'."
solutionAssests := IS301_RyanHanson_PowerfulShellScript1.sh IS301_RyanHanson_PowerfulShellScript2.sh IS301_inputText.txt IS301_outputText.txt

all: help

help:
	@echo "Ryan Hanson"
	@echo "To build, run: 'make build'"
	@echo "To deploy, run: 'make deploy'"

build: $(solutionAssests)
	mkdir powerfulSolutions
	touch script1README.md
	touch script2README.md
	@echo $(script1) > script1README.md
	@echo $(script2) > script2README.md
	cp $(solutionAssests) powerfulSolutions
	cp script1README.md powerfulSolutions
	cp script2README.md powerfulSolutions
	zip -r powerfulSolutions.zip powerfulSolutions
	rm -r -f powerfulSolutions
	rm -f script1README.md
	rm -f script2README.md

deploy: build
	@echo $(shell git add powerfulSolutions.zip)
	@echo $(shell git commit -m "Created powerfulSolutions.zip")
	@echo $(shell git push)

.PHONY: clean
clean:
	rm -f powerfulSolutions.zip
