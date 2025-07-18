using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Entities.FacturaEletronica
{
    public class FacturaRequest
    {
        public int NumberingRangeId { get; set; }
        public string ReferenceCode { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public string PaymentForm { get; set; } = string.Empty;
        public string PaymentDueDate { get; set; } = string.Empty;
        public string PaymentMethodCode { get; set; } = string.Empty;
        public BillingPeriod BillingPeriod { get; set; } = new();
        public Customer Customer { get; set; } = new();
        public List<Item> Items { get; set; } = new();
    }

    public class BillingPeriod
    {
        public string StartDate { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }

    public class Customer
    {
        public string Identification { get; set; } = string.Empty;
        public string Dv { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LegalOrganizationId { get; set; } = string.Empty;
        public string TributeId { get; set; } = string.Empty;
        public string IdentificationDocumentId { get; set; } = string.Empty;
        public string MunicipalityId { get; set; } = string.Empty;
    }

    public class Item
    {
        public string CodeReference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public int UnitMeasureId { get; set; }
        public int StandardCodeId { get; set; }
        public int IsExcluded { get; set; }
        public int TributeId { get; set; }
        public List<WithholdingTax> WithholdingTaxes { get; set; } = new();
    }

    public class WithholdingTax
    {
        public string Code { get; set; } = string.Empty;
        public string WithholdingTaxRate { get; set; } = string.Empty;
    }
}
