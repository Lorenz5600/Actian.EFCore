using Microsoft.EntityFrameworkCore.Update;

namespace Actian.EFCore.Update.Internal;

public class ActianModificationCommandFactory : IModificationCommandFactory
{
    public virtual IModificationCommand CreateModificationCommand(
        in ModificationCommandParameters modificationCommandParameters)
        => new ActianModificationCommand(modificationCommandParameters);

    public virtual INonTrackedModificationCommand CreateNonTrackedModificationCommand(
        in NonTrackedModificationCommandParameters modificationCommandParameters)
        => new ActianModificationCommand(modificationCommandParameters);
}
