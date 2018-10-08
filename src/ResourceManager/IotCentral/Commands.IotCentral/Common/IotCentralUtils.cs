﻿using Microsoft.Azure.Commands.IotCentral.Models;
using Microsoft.Azure.Management.IotCentral.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commands.IotCentral.Common
{
    public static class IotCentralUtils
    {
        public static T2 ConvertObject<T1, T2>(T1 iotCentralObject)
        {
            var serialized = JsonConvert.SerializeObject(iotCentralObject);
            Console.WriteLine(serialized);
            return JsonConvert.DeserializeObject<T2>(serialized);
        }

        public static PSIotCentralApp ToPSIotCentralApp(App iotCentralApp)
        {
            return ConvertObject<App, PSIotCentralApp>(iotCentralApp);
        }

        public static IEnumerable<PSIotCentralApp> ToPSIotCentralApps(IEnumerable<App> iotCentralApps)
        {
            return ConvertObject<IEnumerable<App>, IEnumerable<PSIotCentralApp>>(iotCentralApps.ToList());
        }

        public static AppPatch CreateAppPatch(App iotCentralApp)
        {
            var copiedIotCenralApp = new AppPatch()
            {
                DisplayName = iotCentralApp.DisplayName,
                Tags = iotCentralApp.Tags
            };
            return copiedIotCenralApp;
        }
    }
}
