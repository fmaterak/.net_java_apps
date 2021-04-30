#pragma checksum "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8437c2be70f75004f79f1328efe142ed0e160dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebApp.Pages.Pages_Currency), @"mvc.1.0.razor-page", @"/Pages/Currency.cshtml")]
namespace WebApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8437c2be70f75004f79f1328efe142ed0e160dd", @"/Pages/Currency.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09d1db1c25558096e535f8fd24d0fe24129c968a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Currency : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
   ViewData["Title"] = "Przelicznik walut do dolara (PLN, EUR, GBP, CHF)";
    if (HttpMethods.IsPost(Request.Method))
    {
        string Data = Request.Form["Data"];
        DownloadRates currencygetter = new DownloadRates();
        DownloadRates deserializedproduct = JsonConvert.DeserializeObject<DownloadRates>(currencygetter.getRates(Data));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    USD / PLN:  ");
#nullable restore
#line 13 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
           Write(deserializedproduct.Rates.PLN);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <br />\n    USD / EUR:  ");
#nullable restore
#line 15 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
           Write(deserializedproduct.Rates.EUR);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <br />\n    USD / GBP:  ");
#nullable restore
#line 17 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
           Write(deserializedproduct.Rates.GBP);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <br />\n    USD / CHF:  ");
#nullable restore
#line 19 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
           Write(deserializedproduct.Rates.CHF);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <br />\n");
#nullable restore
#line 21 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
        } 

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 22 "C:\AIR\6SEM\.net_java\web_app\Platf.-program.-.Net-i-Java-master\WebApp\Pages\Currency.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8437c2be70f75004f79f1328efe142ed0e160dd5884", async() => {
                WriteLiteral("\n        <fieldset>\n            <legend>Podaj datę dla której chcesz wyświetlić kursy walut</legend>\n            <div>\n                <label for=\"Data\">Data:</label>\n                <input type=\"text\" name=\"Data\"");
                BeginWriteAttribute("value", " value=\"", 989, "\"", 997, 0);
                EndWriteAttribute();
                WriteLiteral(" />\n            </div>\n            <label>&nbsp;</label>\n            <input type=\"submit\" value=\"Submit\" class=\"submit\" />\n            </div>\n        </fieldset>\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</p>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication1.Pages.Index1Model> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication1.Pages.Index1Model> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication1.Pages.Index1Model>)PageContext?.ViewData;
        public WebApplication1.Pages.Index1Model Model => ViewData.Model;
    }
}
#pragma warning restore 1591