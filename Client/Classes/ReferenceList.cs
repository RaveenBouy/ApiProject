using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public static class ReferenceList
    {
        public static string IpAddr { get; set; } = "http://192.168.1.105:33333/";
        public static string Book { get; set; } = $"{IpAddr}/api/book/";
        public static string BookMember { get; set; } = $"{IpAddr}/api/book/member/";
        public static string BookStaff { get; set; } = $"{IpAddr}/api/book/staff/";
        public static string JournalMember { get; set; } = $"{IpAddr}/api/journal/member/";
        public static string JournalStaff { get; set; } = $"{IpAddr}/api/journal/staff/";
        public static string MagazineMember { get; set; } = $"{IpAddr}/api/magazine/member/";
        public static string MagazineStaff { get; set; } = $"{IpAddr}/api/magazine/staff/";
        public static string ManuscriptMember { get; set; } = $"{IpAddr}/api/manuscript/member/";
        public static string ManuscriptStaff { get; set; } = $"{IpAddr}/api/manuscript/staff/";
        public static string NewspaperMember { get; set; } = $"{IpAddr}/api/newspaper/member/";
        public static string NewspaperStaff { get; set; } = $"{IpAddr}/api/newspaper/staff/";
        public static string Login { get; set; } = $"{IpAddr}/api/login";
        public static string StaffUsers{ get; set; } = $"{IpAddr}/api/staff/users/";
        public static string Register { get; set; } = $"{IpAddr}/api/register";
        public static string StaffUpdateUser { get; set; } = $"{IpAddr}/api/staff/updateuser";
        public static string StaffDeleteUser { get; set; } = $"{IpAddr}/api/staff/deleteuser/";
        public static string Token { get; set; }
    }
}
