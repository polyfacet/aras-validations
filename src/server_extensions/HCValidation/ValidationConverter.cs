using Aras.IOM;

namespace HCValidation;
public class ValidationConverter
{

    private const string HC_VALIDATION_TYPE = "HC_Validation";

    public static Item ConvertValidationToItemResult(Innovator inn, List<Validation> validations, string sourceId)
    {
        Item res = inn.newItem("Dummy");
        Item validationItems = default;
        int i = 0;
        foreach (Validation validation in validations)
        {
            i += 1;
            Item validationItem = ConvertValidationToItem(inn, validation);
            if (validationItems is null)
            {
                validationItems = validationItem;
            }
            else
            {
                validationItems.appendItem(validationItem);
            }

            Item rel = inn.newItem(HC_VALIDATION_TYPE);
            rel.setID(inn.getNewID());
            rel.setAttribute("typeId", "dummy");
            rel.setPropertyItem("related_id", validationItem);
            rel.removeAttribute("isNew");
            rel.removeAttribute("isTemp");

            rel.setProperty("source_id", sourceId);
            res.appendItem(rel);
        }

        if (res.getItemCount() > 1)
        {
            res.removeItem(res.getItemByIndex(0));
            Item res1 = inn.newResult("");
            res1.dom.SelectSingleNode("//Result").InnerXml = res.dom.SelectSingleNode("//AML").InnerXml;
            return res1;
        }
        else
        {
            return res;
        }
    }

    private static Item ConvertValidationToItem(Innovator inn, Validation validation)
    {
        Item validationItem = inn.newItem(HC_VALIDATION_TYPE, "add");
        validationItem.setProperty("validation_type", validation.Type.ToString());
        validationItem.setProperty("image", validation.ImagePath);
        validationItem.setProperty("description", validation.Description);
        validationItem.setProperty("long_description", validation.LongDescription);
        validationItem.setProperty("item_number", validation.ItemNumber);
        if (validation.Item is not null)
        {
            validationItem.setProperty("item_type", validation.Item.getType());
            validationItem.setProperty("item", validation.Item.getID());
            validationItem.setPropertyAttribute("item", "keyed_name", validation.ItemNumber);
        }

        validationItem.removeAttribute("isNew");
        validationItem.removeAttribute("isTemp");
        validationItem.apply();
        return validationItem;
    }


}
