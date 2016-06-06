using System;
using System.Collections.Generic;
using CsvHelper.Configuration;
using PhoneNumbers;

namespace UploadPhotographersMyWeed
{
    public class PhotographerCSV
    {
        private string _phone;
        public string FIO { get; set; }

        public string Phone
        {
            get
            {
                var inst = PhoneNumberUtil.GetInstance();

                var list = new HashSet<string>();
                if (!string.IsNullOrEmpty(_phone))
                    try
                    {
                        var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(_phone, "RU"), PhoneNumberFormat.E164);
                        list.Add(str);
                    }
                    catch (Exception)
                    {

                    }

                if (Json != null && !string.IsNullOrEmpty(Json.Телефон))
                    try
                    {
                        var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(Json.Телефон, "RU"), PhoneNumberFormat.E164);
                        if (list.Contains(str) == false)
                            list.Add(str);
                    }
                    catch (Exception)
                    {


                    }

                if (Json != null && !string.IsNullOrEmpty(Json.Сотовый))
                    try
                    {
                        var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(Json.Сотовый, "RU"), PhoneNumberFormat.E164);
                        if (list.Contains(str) == false)
                            list.Add(str);
                    }
                    catch (Exception)
                    {


                    }

                return string.Join(",", list);
            }
            set { _phone = value; }
        }

        public string City { get; set; }
        public string TeaserImage { get; set; }
        public string Site { get; set; }
        public string Description { get; set; }
        public string Table { get; set; }

        public JObjectPhotographer Json { get; set; }

        public virtual IEnumerable<string> GetPhones()
        {
            var inst = PhoneNumberUtil.GetInstance();

            var list = new HashSet<string>();
            if (!string.IsNullOrEmpty(_phone))
                try
                {
                    var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(_phone, "RU"), PhoneNumberFormat.E164);
                    list.Add(str);
                }
                catch (Exception)
                {

                }

            if (Json != null && !string.IsNullOrEmpty(Json.Телефон))
                try
                {
                    var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(Json.Телефон, "RU"), PhoneNumberFormat.E164);
                    if (list.Contains(str) == false)
                        list.Add(str);
                }
                catch (Exception)
                {


                }

            if (Json == null || string.IsNullOrEmpty(Json.Сотовый)) 
                return list;
         
            try
            {
                var str = inst.Format(PhoneNumberUtil.GetInstance().Parse(Json.Сотовый, "RU"), PhoneNumberFormat.E164);
                if (list.Contains(str) == false)
                    list.Add(str);
            }
            catch (Exception)
            {


            }

            return list;
        }
    }

    public class PhotographerCSVMap : CsvClassMap<PhotographerCSV>
    {
        public PhotographerCSVMap()
        {
            base.Map(m => m.FIO).Index(0);
            base.Map(x => x.Phone).Index(1);
            base.Map(x => x.City).Index(2);
            base.Map(x => x.TeaserImage).Index(3);
            base.Map(x => x.Site).Index(4);
            base.Map(x => x.Description).Index(5);
            base.Map(x => x.Table).Index(6);
            base.Map(m => m.Json).Ignore();
        }
    }
}
