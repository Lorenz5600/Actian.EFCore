
using Microsoft.EntityFrameworkCore.Update;

namespace Actian.EFCore.Update.Internal;

public class ActianModificationCommand : ModificationCommand
{
    public ActianModificationCommand(in ModificationCommandParameters modificationCommandParameters)
        : base(modificationCommandParameters)
    {
    }

    public ActianModificationCommand(in NonTrackedModificationCommandParameters modificationCommandParameters)
        : base(modificationCommandParameters)
    {
    }
}
