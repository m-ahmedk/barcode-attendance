using System;

namespace SchoolAttendance.Utilities.Helper
{
    public static class ConstantStudentList
    {
        //public static IEnumerable<StudentDetails> StudentDetailList { get; set; }=new List<StudentDetails>() 
        //{
        //    new StudentDetails()
        //    {
        //    Barcode = "bd2c169f-b2ae-49ec-8a40-299fe8006ccf",
        //    Name = "Muhammad Farhan Bin Irfan",
        //    Class = "A-Level",
        //    FatherName = "Muhammad Irfan Raja",
        //    Address="K-12, North Nazimabad",
        //    ImageUrl = "https://media.licdn.com/dms/image/C5603AQGzq4M5odld6Q/profile-displayphoto-shrink_800_800/0/1649526812912?e=1727913600&v=beta&t=6hj0QSs9VkEFTez5KhN8ov6F07HOxiaKHUf0NFK4_cQ" // Replace with actual image URL or path
        //    },
        //     new StudentDetails()
        //    {
        //    Barcode = "112fb8e2-2b0f-4bca-838f-39b4661ab285",
        //    Name = "Muhammad Ahmed",
        //    Class = "A-Level",
        //    FatherName = "Father",
        //    Address="Defence Karachi",
        //    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/schoolware-8f35e.appspot.com/o/NewStudent_Images%2F007ed033-80fe-4006-81c6-cdd44c9024e8_employee-3.png?alt=media&token=f3d37ddc-b0dc-4fbf-b500-03bdc6617375" // Replace with actual image URL or path
        //    },
        //      new StudentDetails()
        //    {
        //    Barcode = "d519aeef-520c-48de-bc56-2f028efb6caa",
        //    Name = "Arham Abeer",
        //    Class = "A-Level",
        //    FatherName = "Abeer",
        //    Address="Gulshan",
        //    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/schoolware-8f35e.appspot.com/o/NewStudent_Images%2F007ed033-80fe-4006-81c6-cdd44c9024e8_employee-3.png?alt=media&token=f3d37ddc-b0dc-4fbf-b500-03bdc6617375" // Replace with actual image URL or path
        //    },
        //     new StudentDetails()
        //    {
        //    Barcode = "5bc47439-7928-46f3-a8e4-db36743286c2",
        //    Name = "Wasif Aleem",
        //    Class = "O-Level",
        //    FatherName = "Aleem",
        //    Address="Model Colony",
        //    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/schoolware-8f35e.appspot.com/o/NewStudent_Images%2F007ed033-80fe-4006-81c6-cdd44c9024e8_employee-3.png?alt=media&token=f3d37ddc-b0dc-4fbf-b500-03bdc6617375" // Replace with actual image URL or path
        //    }
        //};
    }
    public class StudentDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public string RegNo { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime TimeIn { get; set; } = DateTime.Now;
        public DateTime? TimeOut { get; set; } = null;
        public DateTime Date { get; set; }
        public string? studentImageUrl { get; set; }
    }
}