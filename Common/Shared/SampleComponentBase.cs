using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LogicCircuit.Shared
{
    public class SampleBaseComponent : ComponentBase
    {
        [Inject]
        protected IJSRuntime? jsRuntime { get; set; }

        [Inject]
        protected SampleService? Service { get; set; }

       
        protected async override void OnAfterRender(bool FirstRender)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            if (Service != null)
            {
                Service.Update(new NotifyProperties() { HideSpinner = true, RestricMouseEvents = true });
            }
        }
    }
}
