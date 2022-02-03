using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fix_eps
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("The file does not exist.");
                return;
            }

            // Introduce a reader and a writer to open the EPS and write the corrected EPS
            StreamReader rdr = new StreamReader(args[0]);
            StreamWriter wri = new StreamWriter(args[0] + "Fixed");

            // Keep track of whether we're currently drawing in grayscale or not
            bool grayscale = true;
            // Count of the number of f -> fo substitutions made
            long substitutions = 0;


            // Start reading
            string line = rdr.ReadLine();

            while (line != null)
            {
                // Replace the fill command with an alias, the "legacy fill command" fo
                if (line == "/f/fill ld")
                    line = "/fo/fill ld";

                // Update whether we are drawing in grayscale
                if (line.Contains("GC"))
                    grayscale = true;
                else if (line.Contains("RC"))
                    grayscale = false;

                // Further (MATLAB-specific) grayscale commands
                if (line.StartsWith("f1") || line.StartsWith("f2") || line.StartsWith("f3") || line.StartsWith("f4") || line.StartsWith("f5") || line.StartsWith("f6") || line.StartsWith("f7") || line.StartsWith("f8") || line.StartsWith("f9") || line.StartsWith("f-"))
                    grayscale = true;

                // If we are drawing in grayscale, switch out f -> fo - no need to draw a line round the fill (or we get bold text, for example)
                if (line == "f" && grayscale)
                {
                    line = "fo";
                    substitutions++; // Increment counter
                }

                // Write the line (potentially modified) to the new file
                wri.WriteLine(line);

                // If we've got to a point where all the macros have been introduced, create a new fill command to replace f, which lines each region
                if (line == "/LW/setlinewidth ld")
                    wri.WriteLine("/f{GS 0 LW S GR fill}bd");

                // Next line
                line = rdr.ReadLine();
            }

            rdr.Close();
            rdr.Dispose();

            wri.Close();
            wri.Dispose();

            // Copy the new image and delete the old one
            try
            {
                System.IO.File.Copy(args[0] + "Fixed", args[0], true);
                System.IO.File.Delete(args[0] + "Fixed");
            }
            catch
            {
                Console.WriteLine("I/O exception when deleting the temporary file - you can find the fixed EPS at {0}", args[0] + "Fixed");
                return;
            }

            Console.WriteLine("EPS clean complete with {0} substitutions.", substitutions.ToString());
        }
    }
}
