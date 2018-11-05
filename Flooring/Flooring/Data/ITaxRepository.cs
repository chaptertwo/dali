using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public interface ITaxRepository
    {
        List<Tax> ReadTaxInfo();
        Tax GetTaxesByAbbr(string abbr);
        Tax GetTaxesByState(string state);
    }
}
