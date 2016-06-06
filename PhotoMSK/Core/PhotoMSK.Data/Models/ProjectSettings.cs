using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models
{
    public class ProjectSettings : NamedEntity
    {
        public Guid ProjectID { get; set; }
        public virtual Project Project { get; set; }
        public int Discount { get; set; }  // Скидка
        public int Seats { get; set; }  // Количество мест
        public ICollection<UserInformation> Members { get; set; }  // Участники
        public int Time { get; set; }  // Время на проект, мин. (30 минут, 120 минут, 360 минут)
        public DateTime StartingDate { get; set; }  // Дата начала
        public string Location { get; set; }  // Место проведения
        public bool Image { get; set; }  // Наличие образа
        public int ImageQuantity { get; set; }  // Кол-во готовых образов (1, 3, 5)
        public int PhotoQuantity { get; set; }  // Кол-во отдаваемых фото (5, 10, 30)
    }
}
