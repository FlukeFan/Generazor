// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System;

namespace ConsoleApp
{
    public class InvoiceItem
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public Decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}