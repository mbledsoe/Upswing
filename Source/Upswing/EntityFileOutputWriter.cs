using System;
using System.IO;

namespace Upswing
{
    public class EntityFileOutputWriter : IOutputWriter<EntityFileModel>
    {
        private readonly string basePath;
        private readonly IEntityFileNamingStrategy entityFileNamingStrategy;

        public EntityFileOutputWriter(string basePath, IEntityFileNamingStrategy entityFileNamingStrategy)
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

    public class DataAccessFileOutputWriter : IOutputWriter<DataAccessObjectFileModel>
    {
        private readonly string basePath;
        private readonly string format;

        public DataAccessFileOutputWriter(string basePath, string format = "{0}Dao.generated.cs")
        {
            this.basePath = basePath;
            this.format = format;
        }

        public void WriteOutput(DataAccessObjectFileModel entityFileModel, string output)
        {
            var filename = string.Format(format, entityFileModel.EntityName);
            var fullPath = Path.Combine(basePath, filename);

            Console.WriteLine($"Writing file {fullPath}");
            File.WriteAllText(fullPath, output);
        }
    }
}
