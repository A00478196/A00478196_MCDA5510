using CsvHelper;
using Serilog;
using System.Diagnostics;

namespace ProgAssign1
{
    public class Home
     
    {

        public static void Main (String[] args)
        {

            // Initial configuration to write log to a log.txt file
            
                Log.Logger = new LoggerConfiguration()
            .WriteTo.File("D:\\MCDA\\5510\\A00478196_MCDA5510\\ProgAssign1\\ProgAssign1\\logs\\log.txt") // Specify the file path here
            .CreateLogger();

            

            Log.Information("---- The Program started ----");

            // Start the timer when the program is run
            var timer = new Stopwatch();
            timer.Start();

          
            List<string> fileEntries = new List<string>();
            

            // Initialize the classes to read thru the directories and handle the csv files
            DirWalker dirWalk = new DirWalker();
            CSVHandler csvHandler = new CSVHandler();


            //store the read directories in a list called fileEntries
            fileEntries = dirWalk.Walker();

            //pass the list of files to csvhandler class to write to the output along with the timer
            csvHandler.readWrite(fileEntries, timer);



            Log.Information("---- The program has ended... ----");
            Log.Information("");
            Log.CloseAndFlush();

        }
}
}