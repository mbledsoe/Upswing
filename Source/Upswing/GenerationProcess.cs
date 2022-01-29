using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class GenerationProcess
    {
        public void Run(ITableDefinitionSource tableDefinitionSource, ITableSpec tableSpec, IGenerator generator, string outputNamespace)
        {
            var tables = tableDefinitionSource.GetTableDefinitions();

            foreach (var table in tables)
            {
                if (tableSpec.IsMatch(table))
                {
                    generator.Generate(table, outputNamespace);
                }
            }
        }
    }
}
