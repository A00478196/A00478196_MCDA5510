# A00478196_MCDA5510

The aim of this program is to loop through multiple csv files, validate them, and write the valid rows in a single file.

<b>The input path to read the directories containing the CSV files is D:\\Sample_Data\\ </b>

It contains 3 classes.

Home.cs contains the main function to run the program
DirWalker.cs reads all the directories and files
CsvHandler loops through the files passed by the DirWalker.css, validates them and writes them in a single file called output.csv.

The validation included are empty/null fields, valid email address, int phone number. 

The exceptions are added as the code is written on the class itself.

The logs of the possible exceptions, details of invalid rows, count of valid/invalid rows, total time taken to execute the program is all stored in a log file called log.txt inside the logs folder.

