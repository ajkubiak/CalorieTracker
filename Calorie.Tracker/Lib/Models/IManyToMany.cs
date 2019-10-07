using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public interface IManyToMany<T>
    {
        ISet<T> ManyToMany { get; set; }
    }
}
