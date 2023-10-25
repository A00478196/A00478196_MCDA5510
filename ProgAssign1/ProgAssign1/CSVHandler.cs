using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Serilog;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Formats.Asn1;
using System.Security;

namespace ProgAssign1
{
    public class CSVHandler
    {
        int validRows = 0;
        int invalidRows = 0;
        int currentRow = 1;
        string emailPattern = @"^(?!\.)[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";


        public object readWrite(List<string> files, Stopwatch timer)
        {
            CSVHandler finalData = new CSVHandler();
           
           

                // configuration for csvHelper
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.HasHeaderRecord = false;
                csvConfig.MissingFieldFound = null;

              
                // do not show the header from the csv file, instead make a custom header 
                var customHeader = new
                {
                    FirstName = "First Name",
                    LastName = "Last Name",
                    StreetNumber = "Street Number",
                    Street = "Street",
                    City = "City",
                    Province = "Province",
                    Country = "Country",
                    PostalCode = "Postal Code",
                    PhoneNumber = "Phone Number",
                    Email = "Email Address",
                    Date = "Date"
                };
               

                // path to store the output after reading the files and validating them
               string outputFileName = "..\\..\\..\\output\\Output.csv";
                //Console.Write("Enter the full path for directory of sample data. For example: C://Assingment1/Sample data// ");
                //string outputFileName = Console.ReadLine();


                using (StreamWriter writer = new StreamWriter(outputFileName, true))
                    using (CsvWriter csv = new CsvWriter(writer, csvConfig))
                    {

                        // add the header only if the output.csv file is empty
                        if (new FileInfo(outputFileName).Length == 0)
                        {

                            csv.WriteRecord(customHeader);
                            csv.NextRecord();
                        }


                        // loop thru all the files, validate them and write it to a csv file.
                        foreach (var fileName in files)
                        {
                            // split the file name to extract the day/month/year of each file

                            string[] splitData = fileName.Split("\\");
                            string date = splitData[splitData.Length - 4] + "/" + splitData[splitData.Length - 3] + "/" + splitData[splitData.Length - 2];
                            string file = splitData[splitData.Length - 1];

                            using (StreamReader reader = new StreamReader(fileName))
                            using (CsvReader csvReader = new CsvReader(reader, csvConfig))
                            {

                                // Read and write the data (excluding headers) from each input file.
                                csvReader.Read(); // Skip the header in the input file
                                while (csvReader.Read())
                                {
                                    currentRow++;
                                    var record = csvReader.GetRecord<Customer>();
                                    if (validation(record))
                                    {
                                        record.Date = fileName.EndsWith(file) ? date : null;
                                        csv.WriteRecord(record);
                                        csv.NextRecord();
                                        validRows++;
                                    }
                                    else
                                    {

                                        Console.WriteLine("Invalid row on file " + file + " on line " + currentRow + " written on " + date);
                                        invalidRows++;
                                        Log.Information("Invalid row on file " + file + " on line " + currentRow + " written on " + date);

                                    }

                                }

                            }
                            currentRow = 1;

                        }

                    
               
                timer.Stop();
                TimeSpan elapsed = timer.Elapsed;

                Log.Information("Total Valid Rows: "+ validRows);
                Log.Information("Total Rows Skipped: "+ invalidRows);
                Log.Information("Total Time taken: " + elapsed.Seconds + " s");
               
            }
           
           
            return files;

        }
        bool validation(Customer record)
        {
            return !string.IsNullOrEmpty(record.FirstName)
                && !string.IsNullOrEmpty(record.LastName)
                && !string.IsNullOrEmpty(record.StreetNumber)
                && !string.IsNullOrEmpty(record.Street)
                && !string.IsNullOrEmpty(record.City)
                && !string.IsNullOrEmpty(record.Province)
                && !string.IsNullOrEmpty(record.Country)
                && !string.IsNullOrEmpty(record.PostalCode)
                && record.PhoneNumber!=null
                && record.PhoneNumber.GetType() == typeof(int)
                && Regex.IsMatch(record.Email.ToString(), emailPattern);
                 
        }

    }
    public class Customer
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Date { get; set; }
    }

}
