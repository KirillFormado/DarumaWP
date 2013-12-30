using System;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common.Abstractions
{
    public interface IDarumaImageUriResolver
    {
        Uri ResolveImageUri(DarumaStatus status);
    }
}