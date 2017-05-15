// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Microsoft.VisualStudio.Web.CodeGenerators.Mvc.RazorPages
{
    public class RazorPageGeneratorTemplateModel : ViewGeneratorTemplateModel
    {
        public string PageModelNamespace { get; set; }
        public string ContextTypeName { get; set; }
        public IEnumerable<string> RequiredNamespaces { get; set; }
    }
}