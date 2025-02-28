﻿using Generator3.Generation.Framework;
using Generator3.Publication;

namespace Generator3
{
    public static class Framework
    {
        public static void GenerateFramework(this GirModel.Namespace ns)
        {
            var internalExtensionsGenerator = new InternalExtensionsGenerator(
                template: new InternalExtensionsTemplate(),
                publisher: new InternalFrameworkFilePublisher()
            );

            var moduleTypeRegistration = new PublicModuleTypeRegistrationGenerator(
                template: new PublicModuleTypeRegistrationTemplate(),
                publisher: new PublicFrameworkFilePublisher()
            );

            internalExtensionsGenerator.Generate(ns);
            moduleTypeRegistration.Generate(ns);
        }
    }
}
