using Aras.IOM;

namespace HCValidation;

public class Validation {

    public enum ValidationType {
        Error,
        Warning,
        Info,
        Ok
    }

    private const string BASE_PATH = @"..\Solutions\hccore\images\";

    private string imagePathField = string.Empty;

    public Validation(ValidationType validationType, Item item, string description) {
        Type = validationType;
        Item = item;
        Description = description;
        if (item is not null) {
            ItemNumber = item.getProperty("item_number", "");
        }
    }

    public Validation(ValidationType validationType, string description) {
        Type = validationType;
        Description = description;
    }

    public ValidationType Type { get; set; }
    public string Description { get; set; }
    public string? ItemNumber { get; set; }
    public string? LongDescription { get; set; }
    public Item? Item { get; set; }

    public string ImagePath {
        get {
            imagePathField = BASE_PATH + Type.ToString() + ".svg";
            return imagePathField;
        }
    }
}
