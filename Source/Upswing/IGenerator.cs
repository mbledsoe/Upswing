namespace Upswing
{
    public interface IGenerator
    {
        void Generate(TableDefinition tableDef, string outputNamespace);
    }
}