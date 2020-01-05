using Kentico.Kontent.Management.Models.Items;
using RowlingApp.Constants;

namespace RowlingApp.Helpers
{
    public static class KontentManagementHelper
    {
        public static (ContentItemIdentifier, LanguageIdentifier, ContentItemVariantIdentifier) GetIdentifiers(string ContentItemCodeName)
        {
            //Retrieive an ItemIdentifier by codename of the content item we want to create a new version of
            ContentItemIdentifier itemIdentifier = ContentItemIdentifier.ByCodename(ContentItemCodeName);

            //Retreive the language identifier of the content item in Konent for the content item (even if we only have one)
            LanguageIdentifier langIdentifier = LanguageIdentifier.ByCodename(RowlingAppConstants.DefaultLanguageCode);

            //Retreive the content item language variant of the content item we want to update (the real unique identifier)
            ContentItemVariantIdentifier identifier = new ContentItemVariantIdentifier(itemIdentifier, langIdentifier);

            return (itemIdentifier, langIdentifier, identifier);
        }
    }
}
