using FubuCollectionBindingProblem.Handlers;
using FubuMVC.Core;
using FubuMVC.Spark;

namespace FubuCollectionBindingProblem
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            // Policies
            Routes
                .IgnoreControllerFolderName()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace()
                .ConstrainToHttpMethod(x => x.Method.Name == "Query", "GET")
                .ConstrainToHttpMethod(x => x.Method.Name == "Command", "POST")
                .HomeIs<SayHelloController>(x => x.Query());

            // Match views to action methods by matching
            // on model type, view name, and namespace
            Views.TryToAttachWithDefaultConventions();


            this.UseSpark();
        }
    }
}