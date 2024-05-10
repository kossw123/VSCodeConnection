
File document = new File("document.txt");

Console.WriteLine($"Initial permissions: {document.Permissions}");

// Applying permissions dynamically using decorators
document.ApplyPermission(new ReadOnlyPermissionDecorator());
document.ApplyPermission(new WriteOnlyPermissionDecorator());
document.ApplyPermission(new ExecutePermissionDecorator());

Console.WriteLine($"Final permissions: {document.Permissions}");


[Flags]
public enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Execute = 4
}

public interface IPermissionDecorator
{
    void ApplyPermissions(ref Permissions p);
}
public class ReadOnlyPermissionDecorator : IPermissionDecorator
{
    public void ApplyPermissions(ref Permissions p) => p |= Permissions.Read;
}
public class WriteOnlyPermissionDecorator : IPermissionDecorator
{
    public void ApplyPermissions(ref Permissions p) => p |= Permissions.Write;
}
public class ExecutePermissionDecorator : IPermissionDecorator
{
    public void ApplyPermissions(ref Permissions p) => p |= Permissions.Execute;
}

public class File
{
    public string Name { get; set; }
    public Permissions Permissions;
    public File(string name)
    {
        Name = name;
        Permissions = Permissions.None;
    }

    public void ApplyPermission(IPermissionDecorator decorator)
    {
        decorator.ApplyPermissions(ref Permissions);
    }
}