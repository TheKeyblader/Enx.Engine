using System;

namespace Enx.WebGPU.SourceGenerator
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    sealed class WrapperAttribute : Attribute
    {
        public WrapperAttribute(Type nativeType)
        {
            NativeType = nativeType;
        }
        public Type NativeType { get; set; }
    }
}
