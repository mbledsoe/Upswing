using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Upswing
{
    public class UpswingOptions
    {
        [Option('c', "connectionString", Required = true, HelpText = "The connection string to use for querying SQL Server table metadata.")]
        public string ConnectionString { get; set; }

        [Option('t', "tables", Required = false, HelpText = "Comma separated list of tables to query for generation.", Separator = ',', Default = new string[0])]
        public IEnumerable<string> Tables { get; set; }

        [Option('o', "output", Required = true, HelpText = "Ouput location for generated files.")]
        public string Output { get; set; }

        [Option('n', "namespace", Required = true, HelpText = "Namespace for generated classes")]
        public string Namespace { get; set; }
        
        //[Option('g', "generator", Required = false, HelpText = "Generator type to use.  Only \"entity\" is currently supported.")]
        //public string Generator { get; set; }
    }
}
