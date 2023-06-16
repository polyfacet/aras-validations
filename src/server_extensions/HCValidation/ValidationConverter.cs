using Aras.IOM;

namespace HCValidation;
public class ValidationConverter
{

    private const string HC_VALIDATION_TYPE = "HC_Validation";

    public static Item ConvertValidationToItemResult(Innovator inn, List<Validation> validations, string sourceId)
    {
        Item result = inn.newItem("Dummy");
        
        int i = 0;
        foreach (Validation validation in validations) {
            i += 1;
            Item validationItem = ConvertValidationToItem(inn, validation);
            Item relation = CreateValidationRelationship(inn, sourceId, validationItem);
            result.appendItem(relation);
        }

        if (result.getItemCount() > 1) {
            result = WrapColletionResult(inn, result);
        }
        return result;
    }

    private static Item WrapColletionResult(Innovator inn, Item result) {
        result.removeItem(result.getItemByIndex(0));
        Item res = inn.newResult("");
        res.dom.SelectSingleNode("//Result").InnerXml = result.dom.SelectSingleNode("//AML").InnerXml;
        return res;
    }

    private static Item CreateValidationRelationship(Innovator inn, string sourceId, Item validationItem) {
        Item rel = inn.newItem(HC_VALIDATION_TYPE);
        rel.setID(inn.getNewID());
        rel.setAttribute("typeId", "dummy");
        rel.setPropertyItem("related_id", validationItem);
        rel.removeAttribute("isNew");
        rel.removeAttribute("isTemp");

        rel.setProperty("source_id", sourceId);
        return rel;
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
