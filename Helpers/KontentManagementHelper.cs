using Kontent.Ai.Management.Models.Items;
using Kontent.Ai.Management.Models.LanguageVariants;
using Kontent.Ai.Management.Models.Shared;
using RowlingApp.Constants;
using System;

namespace RowlingApp.Helpers
{
    public static class KontentManagementHelper
    {
        public static LanguageVariantIdentifier GetIdentifiers(string ContentItemCodeName, string id)
        {
            //Retrieive an ItemIdentifier by codename of the content item we want to create a new version of
            Reference itemIdentifier = Reference.ById(Guid.Parse(id));

            //Retreive the language identifier of the content item in Konent for the content item (even if we only have one)
            Reference langIdentifier = Reference.ByCodename(RowlingAppConstants.DefaultLanguageCode);

            //Retreive the content item language variant of the content item we want to update (the real unique identifier)
            var identifier = new LanguageVariantIdentifier(itemIdentifier, langIdentifier);

            return identifier;
        }
    }
}
