namespace Upswing
{
    public interface IEntityFileModelBuilder
    {
        EntityFileModel BuildModel(TableDefinition tableDef, string entityNamespace);
    }
}