using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CSVReader : List<string[]>
{
    protected string csv = string.Empty;
    protected string separator = ",";

    public CSVReader(string csv, string separator = "\",\"")
    {
        this.csv = csv;
        this.separator = separator;

        foreach (string line in Regex.Split(csv, System.Environment.NewLine).ToList().Where(s => !string.IsNullOrEmpty(s)))
        {
            string[] values = Regex.Split(line, separator);

            for (int i = 0; i < values.Length; i++)
            {
                //Trim values
                values[i] = values[i].Trim('\"');
            }

            this.Add(values);
        }
    }
    
}