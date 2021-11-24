using System;

namespace ModLoader
{
    public interface IHelper
    {
        Action<KeyboardEvent> OnKeyPress { get; set; }
    }
}