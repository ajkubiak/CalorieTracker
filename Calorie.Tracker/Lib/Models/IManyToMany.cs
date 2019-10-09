using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public interface IManyToMany
    {
        void SetIdFirst(Guid id);
        void SetIdSecond(Guid id);
    }
}
