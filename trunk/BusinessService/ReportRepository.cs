using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BusinessService.Data;

namespace BusinessService
{
    public class ReportRepository
    {
        public Report[] GetAllReports()
        {
            List<Report> list = new List<Report>();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int x = 0; x < 29; x++)
            {
                Report rp = new Report() { Year = r.Next(2000, 2012).ToString(), Money = r.NextDouble().ToString() };
                list.Add(rp);
            }
            return list.ToArray();
        }
    }
}
