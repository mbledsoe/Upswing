namespace Upswing
{
    public interface IDataAccessFileModelBuilder
    {
        DataAccessObjectFileModel BuildModel(TableDefinition tableDef, string outputPath);
    }
}