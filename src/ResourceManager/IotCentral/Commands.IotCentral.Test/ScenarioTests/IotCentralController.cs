// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Management.Internal.Resources;

namespace Microsoft.Azure.Commands.IotCentral.Test.ScenarioTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.Azure.Commands.Common.Authentication;
    using Microsoft.Azure.Gallery;
    using Microsoft.Azure.Management.Authorization;
    using Microsoft.Azure.Subscriptions;
    using Microsoft.Azure.Test;
    using Microsoft.Azure.Test.HttpRecorder;
    using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
    using Microsoft.WindowsAzure.Commands.ScenarioTest;
    using TestBase = Microsoft.Azure.Test.TestBase;
    using TestUtilities = Microsoft.Azure.Test.TestUtilities;
    using ServiceManagemenet.Common.Models;
    using Microsoft.Azure.Management.IotCentral;
    using TestEnvironmentFactory = Rest.ClientRuntime.Azure.TestFramework.TestEnvironmentFactory;

    public sealed class IotCentralController
    {
        private EnvironmentSetupHelper _helper;

        public ResourceManagementClient ResourceManagementClient { get; private set; }

        public SubscriptionClient SubscriptionClient { get; private set; }

        public GalleryClient GalleryClient { get; private set; }

        public AuthorizationManagementClient AuthorizationManagementClient { get; private set; }

        public IotCentralClient IotCentralClient { get; private set; }

        public string UserDomain { get; private set; }

        public static IotCentralController NewInstance
        {
            get
            {
                return new IotCentralController();
            }
        }

        public IotCentralController()
        {
            _helper = new EnvironmentSetupHelper();
        }

        public void RunPsTest(XunitTracingInterceptor logger, params string[] scripts)
        {
            var callingClassType = TestUtilities.GetCallingClass(2);
            var mockName = TestUtilities.GetCurrentMethodName(2);

            RunPsTestWorkflow(
                logger,
                () => scripts,
                // no custom initializer
                null,
                // no custom cleanup
                null,
                callingClassType,
                mockName);
        }


        public void RunPsTestWorkflow(
            XunitTracingInterceptor logger,
            Func<string[]> scriptBuilder,
            Action<CSMTestEnvironmentFactory> initialize,
            Action cleanup,
            string callingClassType,
            string mockName)
        {
            _helper.TracingInterceptor = logger;
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("Microsoft.Resources", null);
            d.Add("Microsoft.Features", null);
            d.Add("Microsoft.Authorization", null);
            var providersToIgnore = new Dictionary<string, string>();
            providersToIgnore.Add("Microsoft.Azure.Management.Resources.ResourceManagementClient", "2016-02-01");
            HttpMockServer.Matcher = new PermissiveRecordMatcherWithApiExclusion(true, d, providersToIgnore);
            HttpMockServer.RecordsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SessionRecords");

            using (MockContext context = MockContext.Start(callingClassType, mockName))
            {
                this._helper.SetupEnvironment(AzureModule.AzureResourceManager);

                SetupManagementClients(context);
                var callingClassName = callingClassType
                                        .Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                                        .Last();
                this._helper.SetupModules(AzureModule.AzureResourceManager,
                    "ScenarioTests\\Common.ps1",
                    "ScenarioTests\\" + callingClassName + ".ps1",
                    this._helper.RMProfileModule,
                    this._helper.RMResourceModule,
                    this._helper.RMInsightsModule);
                try
                {
                    if (scriptBuilder != null)
                    {
                        var psScripts = scriptBuilder();

                        if (psScripts != null)
                        {
                            this._helper.RunPowerShellTest(psScripts);
                        }
                    }
                }
                finally
                {
                    cleanup?.Invoke();
                }
            }
        }

        private void SetupManagementClients(MockContext context)
        {
            this.ResourceManagementClient = GetResourceManagementClient(context);
            this.SubscriptionClient = GetSubscriptionClient(context);
            this.IotCentralClient = GetIotCentralClient(context);
            this.AuthorizationManagementClient = GetAuthorizationManagementClient(context);

            this._helper.SetupManagementClients(this.ResourceManagementClient,
                this.SubscriptionClient,
                this.IotCentralClient,
                this.AuthorizationManagementClient
                );
        }

        private AuthorizationManagementClient GetAuthorizationManagementClient(MockContext context)
        {
            return context.GetServiceClient<AuthorizationManagementClient>(TestEnvironmentFactory.GetTestEnvironment());
        }

        private ResourceManagementClient GetResourceManagementClient(MockContext context)
        {
            return context.GetServiceClient<ResourceManagementClient>(TestEnvironmentFactory.GetTestEnvironment());
        }

        private SubscriptionClient GetSubscriptionClient(MockContext context)
        {
            return context.GetServiceClient<SubscriptionClient>(TestEnvironmentFactory.GetTestEnvironment());
        }

        private IotCentralClient GetIotCentralClient(MockContext context)
        {
            return context.GetServiceClient<IotCentralClient>(TestEnvironmentFactory.GetTestEnvironment());
        }
    }
}
