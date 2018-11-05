using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Flooring.Models;

namespace Flooring.Data
{
    public class TaxRepository : ITaxRepository
    {
        private string filePath = @"C:\Data\Flooring\Taxes.txt";

        public List<Tax> ReadTaxInfo()
        {
                List<Tax> taxes = new List<Tax>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine(); //skip header
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        //Product newProduct = new Product();
                        Tax tax = new Tax();

                        string[] columns = line.Split(',');

                        tax.Abbreviation = columns[0];
                        tax.StateName = columns[1];
                        tax.TaxRate = decimal.Parse(columns[2]);
                       
                        taxes.Add(tax);
                    }
                    return taxes;
            }
        }


        public Tax GetTaxesByAbbr(string abbr)
        {
            var specificTax = ReadTaxInfo().FirstOrDefault(t => t.Abbreviation == abbr);
            return specificTax;
        }

        public Tax GetTaxesByState(string state)
        {
            var specificTax = ReadTaxInfo().FirstOrDefault(t => t.StateName == state);
            return specificTax;
        }
    }
}
