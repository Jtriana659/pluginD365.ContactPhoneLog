using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace ContactPhoneLogPlugin
{
    public class ContactPhoneLogPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            tracing.Trace("Inicio del plugin ContactPhoneLogPlugin");

            // Solo ejecuta en Update de contacto

            if (context.MessageName != "Update" || context.PrimaryEntityName != "contact")
                return;

            Entity target = (Entity)context.InputParameters["Target"];

            // Ejecutar solo si están actualizando telephone1
            if (!target.Contains("telephone1"))
                return;

            tracing.Trace("telephone1 está siendo modificado...");

            // ==== TOMAR EL OLD PHONE DEL PRE-IMAGE ====
            Entity preImage = null;
            Entity postImage = null;
            if (context.PreEntityImages.Contains("PreImage"))
            {
                preImage = context.PreEntityImages["PreImage"];
            }

            if (context.PostEntityImages.Contains("PostImage"))
            {
                postImage = context.PostEntityImages["PostImage"];
            }


            string oldPhone = preImage?.GetAttributeValue<string>("telephone1") ?? "";
            string newPhone = postImage?.GetAttributeValue<string>("telephone1") ?? "";
            // ==== NEW PHONE del Target ----
            //string newPhone = target.GetAttributeValue<string>("telephone1");
            tracing.Trace($"OLD: {oldPhone} | NEW: {newPhone}");

            Entity log = new Entity("cree4_new_contactlog");
            log["cree4_new_contact"] = new EntityReference("contact", target.Id);
            log["cree4_new_oldphone"] = oldPhone;
            log["cree4_new_newphone"] = newPhone;
            log["cree4_new_description"] = $"Teléfono cambiado de {oldPhone} a {newPhone}";

            service.Create(log);

        }
    }
}
