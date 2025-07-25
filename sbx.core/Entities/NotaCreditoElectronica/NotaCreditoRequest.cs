
namespace sbx.core.Entities.NotaCreditoElectronica
{
    public class BillingPeriod
    {
        public string start_date { get; set; } = string.Empty;
        public string start_time { get; set; } = string.Empty;
        public string end_date { get; set; } = string.Empty;
        public string end_time { get; set; } = string.Empty;
    }

    public class Customer
    {
        public string identification { get; set; } = string.Empty;
        public string dv { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public string trade_name { get; set; } = string.Empty;
        public string names { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string legal_organization_id { get; set; } = string.Empty;
        public string tribute_id { get; set; } = string.Empty;
        public string identification_document_id { get; set; } = string.Empty;
        public string municipality_id { get; set; } = string.Empty;
    }

    public class Item
    {
        public string note { get; set; } = string.Empty;
        public string code_reference { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public decimal quantity { get; set; } 
        public decimal discount_rate { get; set; }
        public decimal price { get; set; }
        public decimal tax_rate { get; set; }
        public int unit_measure_id { get; set; }
        public int standard_code_id { get; set; }
        public int is_excluded { get; set; }
        public int tribute_id { get; set; }
        public List<object> withholding_taxes { get; set; } = new();
    }

    public class NotaCreditoRequest
    {
        public int numbering_range_id { get; set; }
        public int correction_concept_code { get; set; }
        public int customization_id { get; set; }
        public int bill_id { get; set; }
        public string reference_code { get; set; } = string.Empty;
        public bool send_email { get; set; }
        public string observation { get; set; } = string.Empty;
        public string payment_method_code { get; set; } = string.Empty;
        public BillingPeriod? billing_period { get; set; } 
        public Customer customer { get; set; } = new();
        public List<Item> items { get; set; } = new();
    }
}
