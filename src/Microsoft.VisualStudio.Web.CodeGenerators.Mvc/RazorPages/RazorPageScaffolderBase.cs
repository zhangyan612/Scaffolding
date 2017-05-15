// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Microsoft.VisualStudio.Web.CodeGenerators.Mvc.RazorPages
{
    public abstract class RazorPageScaffolderBase: ViewScaffolderBase
    {
        public RazorPageScaffolderBase(
            IProjectContext projectDependencyProvider,
            IApplicationInfo applicationInfo,
            ICodeGeneratorActionsService codeGeneratorActionsService,
            IServiceProvider serviceProvider,
            ILogger logger)
            : base(projectDependencyProvider, applicationInfo, codeGeneratorActionsService, serviceProvider, logger)
        {

        }

        public override IEnumerable<string> TemplateFolders
        {
            get
            {
                return TemplateFoldersUtilities.GetTemplateFolders(
                    containingProject: Constants.ThisAssemblyName,
                    applicationBasePath: ApplicationInfo.ApplicationBasePath,
                    baseFolders: new[] { "RazorPages" },
                    projectContext: ProjectContext);
            }
        }

        protected RazorPageGeneratorTemplateModel GetRazorPageGeneratorTemplateModel(ViewGeneratorModel viewGeneratorModel, ModelTypeAndContextModel modelTypeAndContextModel)
        {
            bool isLayoutSelected = !viewGeneratorModel.PartialView &&
                (viewGeneratorModel.UseDefaultLayout || !String.IsNullOrEmpty(viewGeneratorModel.LayoutPage));

            RazorPageGeneratorTemplateModel templateModel = new RazorPageGeneratorTemplateModel()
            {
                ViewDataTypeName = modelTypeAndContextModel?.ModelType?.FullName,
                ViewDataTypeShortName = modelTypeAndContextModel?.ModelType?.Name,
                ViewName = viewGeneratorModel.ViewName,
                LayoutPageFile = viewGeneratorModel.LayoutPage,
                IsLayoutPageSelected = isLayoutSelected,
                IsPartialView = viewGeneratorModel.PartialView,
                ReferenceScriptLibraries = viewGeneratorModel.ReferenceScriptLibraries,
                ModelMetadata = modelTypeAndContextModel?.ContextProcessingResult?.ModelMetadata,
                JQueryVersion = "1.10.2", //Todo,
                PageModelNamespace = "Dummy", //TODO Fix this.
                ContextTypeName = modelTypeAndContextModel?.DbContextFullName,
                RequiredNamespaces = new List<string>()
            };

            return templateModel;
        }
    }
}