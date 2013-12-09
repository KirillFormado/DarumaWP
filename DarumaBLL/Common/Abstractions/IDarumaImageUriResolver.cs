using System;
using DarumaBLL.Domain;

namespace DarumaBLL.Common.Abstractions
{
    public interface IDarumaImageUriResolver
    {
        Uri ResolveImageUri(DarumaStatus status);
    }
}