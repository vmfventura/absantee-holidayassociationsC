using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public interface IPeriod
    {
        public bool IsStartDateIsValid(DateOnly startDate, DateOnly endDate);
        public void UpdateStartDate(DateOnly startDate);
        public void UpdateEndDate(DateOnly endDate);
    }
}