using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Status.Core.Interfaces
{
    public interface IOutputConfig
    {
        bool Multiline { get; }
        string Seperator { get; }
    }
}
