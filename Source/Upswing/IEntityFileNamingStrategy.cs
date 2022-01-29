namespace Upswing
{
    public interface IEntityFileNamingStrategy
    {
        string GetFileName(EntityFileModel entityFileModel);
    }
}