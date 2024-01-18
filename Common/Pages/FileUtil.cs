using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LogicCircuit
{
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class FileUtil
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        /// <summary>
        /// Asynchronously triggers a save operation using JavaScript interop.
        /// </summary>
        public async static Task SaveAs(IJSRuntime js, string data, string fileName)
        {
            await js.InvokeAsync<object>(
                "saveDiagram",
#pragma warning disable CA1305 // Specify IFormatProvider
                Convert.ToString(data), fileName).ConfigureAwait(true);
#pragma warning restore CA1305 // Specify IFormatProvider
        }
        /// <summary>
        /// Asynchronously triggers a click event using JavaScript interop.
        /// </summary>
        public async static Task Click(IJSRuntime js)
        {
            await js.InvokeAsync<object>(
                "click").ConfigureAwait(true);
        }
        /// <summary>
        /// Asynchronously loads a file using JavaScript interop.
        /// </summary>
        public async static Task<string> LoadFile(IJSRuntime js, object data)
        {
            return await js.InvokeAsync<string>(
                  "loadFile", data).ConfigureAwait(true);
        }
    }
}
