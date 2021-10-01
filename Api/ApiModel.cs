using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class CasesModel
    {
        public DataModel data { get; set; }
        public TopBlockModel topBlock { get; set; }
        public RegionalModel[] getBeneficiariesGroupBy { get; set;}
    }
    public class TopBlockModel
    {
        public VaccinationModel vaccination { get; set; }
    }
    public class VaccinationModel
    {
        public long total_doses { get; set; }
    }

    public class DataModel
    {
         public SummaryModel summary { get; set; }
         public RegionalModel[] regional { get; set; }
    }

    public class SummaryModel
    {
        public long total { get; set; }
        public long discharged { get; set; }
        public long deaths { get; set; }
        public long totalBeds { get; set; }
    }

    public class RegionalModel
    {
        public string loc { get; set; }
        public string state { get; set; }
        public string state_name { get; set; }
        public long totalConfirmed { get; set; }
        public long deaths { get; set; }
        public long discharged { get; set; }
        public long totalBeds { get; set; }
        public long partial_vaccinated { get; set; }
        public long fully_vaccinated { get; set; }

    }

}
