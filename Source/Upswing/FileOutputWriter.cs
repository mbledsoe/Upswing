using System;
using System.IO;

namespace Upswing
{
    public class FileOutputWriter : IOutputWriter
    {
        private readonly string basePath;
        private readonly IEntityFileNamingStrategy entityFileNamingStrategy;

        public FileOutputWriter(string basePath, IEntityFileNamingStrategy entityFileNamingStrategy)
        {
            this.basePath = basePath;
            this.entityFileNamingStrategy = entityFileNamingStrategy;
        }

        public void WriteOutput(EntityFileModel entityFileModel, string output)
        {
            var filename = entityFileNamingStrategy.GetFileName(entityFileModel);
            var fullPath = Path.Combine(basePath, filename);

            Console.WriteLine($"Writing file {fullPath}");

            File.WriteAllText(fullPath, output);
        }
    }
}
