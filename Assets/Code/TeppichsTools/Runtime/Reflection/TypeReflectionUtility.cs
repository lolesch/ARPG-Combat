using System;
using System.Linq;

namespace TeppichsTools.Runtime.Reflection
{
    public static class TypeReflectionUtility
    {
        public static Type[] GetSubtypesFromSameAssembly(Type superType) => superType.Assembly.GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(superType)).ToArray();

        public static Type[] GetSubtypesFromAllAssemblies(Type superType) =>
            (from assembly in AppDomain.CurrentDomain.GetAssemblies()
             from typeInAssembly in assembly.GetTypes()
             where typeInAssembly.IsSubclassOf(superType) && !typeInAssembly.IsAbstract
             select typeInAssembly).ToArray();
    }
}